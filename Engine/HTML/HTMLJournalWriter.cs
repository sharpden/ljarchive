using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.IO;
using EF.ljArchive.Engine;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;

namespace EF.ljArchive.Engine.HTML
{
	/// <summary>
	/// Writes a <see cref="EF.ljArchive.Engine.Journal"/> to a <see cref="Stream"/> in HTML format.
	/// </summary>
	/// <remarks><para><see cref="HTMLJournalWriter"/> uses a template system to format the output it writes
	/// to the <see cref="Stream"/>.  For a complete description of this template system, please visit the
	/// <a href="http://ljarchive.sourceforge.net/" target="_blank">web site</a>.</para>
	/// </remarks>
	/// <example>Here's an example of writing to an html file using <see cref="HTMLJournalWriter"/>.
	/// <code>
	/// Journal j = Journal.Load(@"journal.lja");
	/// HTMLJournalWriter hjw = new HTMLJournalWriter();
	/// using (System.IO.StreamReader s = new System.IO.StreamReader("simple.ljt"))
	///		hjw.Transform = s.ReadToEnd();
	/// using (System.IO.FileStream fs = System.IO.File.Open("journal.htm", System.IO.FileMode.Create))
	///		hjw.WriteJournal(fs, j, null, null, true, true);</code>
	/// </example>
	public class HTMLJournalWriter : Common.IJournalWriter
	{
		#region Public Instance Constructors
		/// <summary>
		/// Creates a new instance of the <see cref="HTMLJournalWriter"/> class.
		/// </summary>
		public HTMLJournalWriter()
		{
			hjws = HTMLJournalWriterSettings.CreateDefault();
		}
		#endregion

		#region IJournalWriter Members
		object EF.ljArchive.Common.IJournalWriter.Settings
		{
			get
			{
				return hjws;
			}
			set
			{
				hjws = (HTMLJournalWriterSettings) value;
			}
		}

		string EF.ljArchive.Common.IJournalWriter.Description
		{
			get
			{
				return "Default ljArchive engine for displaying and exporting HTML.";
			}
		}

		string EF.ljArchive.Common.IJournalWriter.Author
		{
			get
			{
				return "Erik Frey";
			}
		}

		string EF.ljArchive.Common.IJournalWriter.Name
		{
			get
			{
				return "HTML Journal Writer";
			}
		}

		string EF.ljArchive.Common.IJournalWriter.URL
		{
			get
			{
				return "http://ljarchive.sourceforge.net/";
			}
		}

		Version EF.ljArchive.Common.IJournalWriter.Version
		{
			get
			{
				return new Version(0, 9, 8, 1);
			}
		}

		string EF.ljArchive.Common.IJournalWriter.Filter
		{
			get
			{
				return "HTML files (*.htm)|*.htm|All files (*.*)|*.*";
			}
		}
		#endregion

		#region Public Instance Properties
		/// <summary>
		/// Search string to highlight in journal entries.
		/// </summary>
		public string EntrySearchString
		{
			get { return this.entrySearchString; }
			set { this.entrySearchString = value; }
		}

		/// <summary>
		/// Search string to highlight in journal comments.
		/// </summary>
		public string CommentSearchString
		{
			get { return this.commentSearchString; }
			set { this.commentSearchString = value; }
		}

		/// <summary>
		/// Comment to highlight as the selected comment.
		/// </summary>
		public int SelectedCommentID
		{
			get { return this.selectedCommentID; }
			set { this.selectedCommentID = value; }
		}

		/// <summary>
		/// Transform string to use.
		/// </summary>
		/// <remarks>The transform string is based on a template system that tells <see cref="HTMLJournalWriter"/>
		/// how to render events and comments to an HTML stream.  For a complete description of the template system,
		/// please visit the <a href="http://ljarchive.sourceforge.net/" target="_blank">web site</a>.</remarks>
		public string Transform
		{
			get { return this.transform; }
			set { this.transform = value;}
		}

		/// <summary>
		/// Gets or sets the <see cref="HTMLJournalWriterSettings"/> for this object.
		/// </summary>
		public HTMLJournalWriterSettings Settings
		{
			get { return this.hjws; }
			set { this.hjws = value; }
		}
		#endregion

		#region Public Instance Methods
		/// <summary>
		/// Writes selected <see cref="EF.ljArchive.Common.Journal.EventsRow"/> objects and
		/// <see cref="EF.ljArchive.Common.Journal.CommentsRow"/> objects to an HTML-formatted stream.
		/// </summary>
		/// <param name="s">The <see cref="Stream"/> to be written to.</param>
		/// <param name="j">The <see cref="Journal"/> to use for writing.</param>
		/// <param name="eventIDs">An array of <see cref="EF.ljArchive.Common.Journal.EventsRow"/> ItemIDs that
		/// specify which <see cref="EF.ljArchive.Common.Journal.EventsRow"/> objects to write.</param>
		/// <param name="commentIDs">An array of <see cref="EF.ljArchive.Common.Journal.CommentsRow"/> IDs that
		/// specify which <see cref="EF.ljArchive.Common.Journal.CommentsRow"/> objects to write.</param>
		/// <param name="header">If <see langword="true"/>, write any required header for the stream format.</param>
		/// <param name="footer">If <see langword="true"/>, write any required footer for the stream format.</param>
		public void WriteJournal(System.IO.Stream s, EF.ljArchive.Common.Journal j, int[] eventIDs, int[] commentIDs, bool header, bool footer)
		{
			StreamWriter sw;
			IEnumerable ieEvents;
			options = j.Options[0];
			PreProcessTransform();
			sw = new StreamWriter(s);
			if (header)
				sw.Write(pageHeader);
			if (eventIDs != null)
				ieEvents = j.Events.Select("ID IN (" + IntJoin(eventIDs) + ")");
			else
				ieEvents = j.Events;
			foreach (Journal.EventsRow er in ieEvents)
			{
				WriteEvent(sw, er, j.UserPics, j.Moods);
				string rowFilter = "JItemID = " + er.ID.ToString();
				if (commentIDs != null)
					rowFilter += " AND ID IN(" + IntJoin(commentIDs) + ")";
				DataRow[] drs = j.Comments.Select(rowFilter);
				CommentNode cn = new CommentNode(null, null);
				if (drs.Length > 0 && !AllDeleted(drs))
				{
					sw.Write(commentsHeader);
					foreach (Journal.CommentsRow cr in drs)
					{
						if (cr.ParentID == 0)
						{
							cn.AddChildComment(cr);
						}
						else
						{
							CommentNode cnParent = cn.Find(cr.ParentID);
							if (cnParent != null)
								cnParent.AddChildComment(cr);
						}
					}
					foreach (CommentNode tcEnum in cn.GetInfixEnumerator())
					{
						if (tcEnum.Comment.State != "D")
							WriteComment(sw, tcEnum.Comment, j.Users, (tcEnum.Depth - 1) * 25);
					}
					sw.Write(commentsFooter);
				}
			}
			if (footer)
				sw.Write(pageFooter);
			sw.Flush();
		}
		#endregion

		#region Private Instance Methods
		/// <summary>
		/// A general transformation that can be applied anywhere.
		/// </summary>
		private void WriteGeneral(string transform, TextWriter tw)
		{
			ArrayList idstack = new ArrayList();
			foreach (string block in _rblock.Split(transform))
			{
				if (block.StartsWith("<%"))
				{
					string id = block.Substring(2, block.Length - 4).Trim().ToLower();

					// check the id stack for additions
					if (id.StartsWith("!"))
					{
						string idToBlock = id.Substring(1);
						switch (idToBlock)
						{
							case "defaultpicurl":
								if ((options.IsUseJournalNull() && options.IsDefaultPicURLNull())
								    || (!options.IsUseJournalNull() && options.IsCommunityPicURLNull()))
									idstack.Add(idToBlock);
								break;
							default:
								if (idToBlock.StartsWith("!"))
								{
									if (options.Table.Columns.Contains(idToBlock.Substring(1)))
									{
										if (!options.IsNull(idToBlock.Substring(1)))
											idstack.Add(idToBlock);
									}
									else
										tw.Write(block);
								}
								else
								{
									if (options.Table.Columns.Contains(idToBlock))
									{
										if (options.IsNull(idToBlock))
											idstack.Add(idToBlock);
									}
									else
										tw.Write(block);
								}
								break;
						}
						continue;
					}
					// check the id stack for removals
					else if (id.StartsWith("/!"))
					{
						if (idstack.Contains(id.Substring(2)))
							idstack.Remove(id.Substring(2));
						else
							tw.Write(block);
						continue;
					}
					else
					{
						// we don't output anything until the stack is empty
						if (idstack.Count > 0)
							continue;
						switch (id)
						{
							case "userinfoiconpath":
								tw.Write(hjws.UserInfoIconPath);
								break;
							case "communityinfoiconpath":
								tw.Write(hjws.CommunityInfoIconPath);
								break;
							case "spacerpath":
								tw.Write(hjws.SpacerPath);
								break;
							case "pagebackgroundcolor":
								tw.Write(GetHTMLColor(hjws.PageBackgroundColor));
								break;
							case "pagealternatebackgroundcolor":
								tw.Write(GetHTMLColor(hjws.PageAlternateBackgroundColor));
								break;
							case "pagetextcolor":
								tw.Write(GetHTMLColor(hjws.PageTextColor));
								break;
							case "pagelinkcolor":
								tw.Write(GetHTMLColor(hjws.PageLinkColor));
								break;
							case "pagevisitedlinkcolor":
								tw.Write(GetHTMLColor(hjws.PageVisitedLinkColor));
								break;
							case "pageactivelinkcolor":
								tw.Write(GetHTMLColor(hjws.PageActiveLinkColor));
								break;
							case "entrybackgroundcolor":
								tw.Write(GetHTMLColor(hjws.EntryBackgroundColor));
								break;
							case "entrytextcolor":
								tw.Write(GetHTMLColor(hjws.EntryTextColor));
								break;
							case "entryheaderbackgroundcolor":
								tw.Write(GetHTMLColor(hjws.EntryHeaderBackgroundColor));
								break;
							case "entryheadertextcolor":
								tw.Write(GetHTMLColor(hjws.EntryHeaderTextColor));
								break;
							case "entryfooterbackgroundcolor":
								tw.Write(GetHTMLColor(hjws.EntryFooterBackgroundColor));
								break;
							case "entryfootertextcolor":
								tw.Write(GetHTMLColor(hjws.EntryFooterTextColor));
								break;
							case "defaultpicurl":
								if (options.IsUseJournalNull())
									tw.Write(options.DefaultPicURL);
								else
									tw.Write(options.CommunityPicURL);
								break;
							default:
								if (id != "hpassword" && options.Table.Columns.Contains(id))
									tw.Write(options[id]);
								else
									// fall through
									tw.Write(block);
								break;
						}
					}
				}
				else
				{
					if (idstack.Count == 0)
						tw.Write(block);
				}
			}
		}

		/// <summary>
		/// Output a transformed entry to the HTML stream.
		/// </summary>
		private void WriteEvent(TextWriter tw, Journal.EventsRow er, Journal.UserPicsDataTable updt,
			Journal.MoodsDataTable mdt)
		{
			ArrayList idstack = new ArrayList();
			foreach (string block in _rblock.Split(entry))
			{
				if (block.StartsWith("<%"))
				{
					string id = block.Substring(2, block.Length - 4).Trim().ToLower();
					// check the id stack for additions
					if (id.StartsWith("!"))
					{
						string idToBlock = id.Substring(1);
						switch (idToBlock)
						{
							case "userpicurl":
								if ((!er.IsPosterNull() && er.Poster != options.UserName)
								    || ((er.IsPictureKeywordNull() || updt.FindByPicKeyword(er.PictureKeyword) == null)
								        && (options.IsDefaultPicURLNull())))
									idstack.Add(idToBlock);
								break;
							case "currentmood":
								if (er.IsCurrentMoodNull() && (er.IsCurrentMoodIDNull() ||
									mdt.FindByID(er.CurrentMoodID) == null))
									idstack.Add(idToBlock);
								break;
							case "securityiconpath":
								if (er.IsSecurityNull() || (er.Security != "usemask" && er.Security != "private"))
									idstack.Add(idToBlock);
								break;
							default:
								if (!er.Table.Columns.Contains(idToBlock) || er.IsNull(idToBlock))
									idstack.Add(idToBlock);
								break;
						}
						continue;
					}
					// check the id stack for removals
					else if (id.StartsWith("/!"))
					{
						idstack.Remove(id.Substring(2));
						continue;
					}
					else
					{
						// we don't output anything until the stack is empty
						if (idstack.Count > 0)
							continue;
						switch (id)
						{
							case "userpicurl":
								if (er.IsPosterNull() || er.Poster == options.UserName)
								{
									Journal.UserPicsRow ur = null;
									if (!er.IsPictureKeywordNull())
										ur = updt.FindByPicKeyword(er.PictureKeyword);
									if (ur != null)
										tw.Write(ur.PicURL);
									else if (!options.IsDefaultPicURLNull())
										tw.Write(options.DefaultPicURL);
								}
								break;
							case "currentmood":
								if (!er.IsCurrentMoodNull())
									WritePreparedText(tw, er.CurrentMood, entrySearchString);
								else if (!er.IsCurrentMoodIDNull() && mdt.FindByID(er.CurrentMoodID) != null)
									WritePreparedText(tw, mdt.FindByID(er.CurrentMoodID).Name, entrySearchString);
								break;
							case "securityiconpath":
								if (!er.IsSecurityNull() && er.Security == "usemask")
									tw.Write(hjws.ProtectedIconPath);
								else if (!er.IsSecurityNull() && er.Security == "private")
									tw.Write(hjws.PrivateIconPath);
								break;
							case "posterusername":
								if (!options.IsUseJournalNull() && !er.IsPosterNull())
									tw.Write(er.Poster);
								else
									tw.Write(options.UserName);
								break;
							default:
								if (id == "subject" || id == "currentmusic" || id == "body")
									WritePreparedText(tw, er[id].ToString(), entrySearchString);
								else if (er.Table.Columns.Contains(id))
									tw.Write(er[id]);
								break;
						}
					}
				}
				else
				{
					if (idstack.Count == 0)
						tw.Write(block);
				}
			}
		}

		/// <summary>
		/// Output a transformed comment to the HTML stream.
		/// </summary>
		private void WriteComment(TextWriter tw, Journal.CommentsRow cr, Journal.UsersDataTable udt, int spacerWidth)
		{
			// you'd think that using an actual System.Collections.Stack would be faster
			// but profiling shows that it's actually a tiny bit slower.  not sure why.
			ArrayList idstack = new ArrayList();
			foreach (string block in commentBlocks)
			{
				if (block.StartsWith("<%"))
				{
					string id = block.Substring(2, block.Length - 4).Trim().ToLower();
					// check the id stack for additions
					if (id.StartsWith("!"))
					{
						string idToBlock = id.Substring(1);
						switch (idToBlock)
						{
							case "nonanonymous":
								if (cr.IsPosterIDNull() || cr.PosterID == 0)
									idstack.Add(idToBlock);
								break;
							case "anonymous":
								if (!cr.IsPosterIDNull() && cr.PosterID != 0)
									idstack.Add(idToBlock);
								break;
							case "spacerwidth":
								if (spacerWidth == 0)
									idstack.Add(idToBlock);
								break;
							default:
								if (!cr.Table.Columns.Contains(idToBlock) || cr.IsNull(idToBlock))
									idstack.Add(idToBlock);
								break;
						}
						continue;
					}
						// check the id stack for removals
					else if (id.StartsWith("/!"))
					{
						idstack.Remove(id.Substring(2));
						continue;
					}
					else
					{
						// we don't output anything until the stack is empty
						if (idstack.Count > 0)
							continue;
						switch (id)
						{
							case "spacerwidth":
								tw.Write(spacerWidth);
								break;
							case "commentbackgroundcolor":
								if (cr.ID == selectedCommentID)
									tw.Write(GetHTMLColor(hjws.SelectedCommentBackgroundColor));
								else if (!cr.IsStateNull() && cr.State.ToLower() == "s")
									tw.Write(GetHTMLColor(hjws.ScreenedCommentBackgroundColor));
								else
									tw.Write(GetHTMLColor(hjws.CommentBackgroundColor));
								break;
							case "commenttextcolor":
								if (cr.ID == selectedCommentID)
									tw.Write(GetHTMLColor(hjws.SelectedCommentTextColor));
								else if (!cr.IsStateNull() && cr.State.ToLower() == "s")
									tw.Write(GetHTMLColor(hjws.ScreenedCommentTextColor));
								else
									tw.Write(GetHTMLColor(hjws.CommentTextColor));
								break;
							default:
								if (id == "subject" || id == "body")
									WritePreparedText(tw, cr[id].ToString(), commentSearchString);
								else if (cr.Table.Columns.Contains(id))
									tw.Write(cr[id]);
								break;
						}
					}
				}
				else
				{
					if (idstack.Count == 0)
						tw.Write(block);
				}
			}
		}

		/// <summary>
		/// Output prepared text to the HTML stream.
		/// </summary>
		///<remarks>This handles things such as converting newlines to br tags, and rendering lj user tags.</remarks>
		private void WritePreparedText(TextWriter tw, string text, string searchString)
		{
			foreach (string tag in _rtag.Split(text))
			{
				if (tag.StartsWith("<"))
				{
					string id = tag.Substring(1, tag.Length - 2).Trim().ToLower();
					if (id.StartsWith("lj"))
					{
						string mode = id.Substring(2).Trim();
						if (mode.StartsWith("user"))
						{
							string user = mode.Substring(4).Trim(new char[] {' ', '=', '"'});
							tw.Write(string.Format(_usertransform, options.ServerURL, user, hjws.UserInfoIconPath));
						}
						if (mode.StartsWith("comm"))
						{
							string comm = mode.Substring(4).Trim(new char[] {' ', '=', '"'});
							tw.Write(string.Format(_usertransform, options.ServerURL, comm, hjws.CommunityInfoIconPath));
						}
					}
					else if (hjws.BlockImages && id.StartsWith("img"))
					{
						// print nothing
					}
					else
					{
						tw.Write(tag);
					}
				}
				else
				{
					string s = tag;
					if (searchString != null && searchString.Length > 0)
					{
						Regex r = new Regex("(" + Regex.Escape(searchString) + ")", RegexOptions.IgnoreCase);
						s = string.Format(_searchstringtransform,
							GetHTMLColor(hjws.HighlightBackgroundColor),
							GetHTMLColor(hjws.HighlightTextColor), "{1}");
						s = r.Replace(tag, s);
					}
					s = s.Replace("\n", "<br />" + Environment.NewLine);
					tw.Write(s);
				}
			}
		}

		/// <summary>
		/// Returns a specified section from the transform string.
		/// </summary>
		/// <param name="tag">The section to search for.</param>
		/// <returns>A specified section from the transform.</returns>
		private string GetTransformSection(string tag)
		{
			string pat = string.Format(@"<%\s*{0}\s*%>(.*?)<%\s*/{0}\s*%>", tag);
			Regex r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			Match m = r.Match(transform);
			if (!m.Success || m.Groups.Count < 2)
				throw new ArgumentException("bad transform", "transform");
			return m.Groups[1].Captures[0].ToString();
		}

		/// <summary>
		/// Preprocesses all sections using the <see cref="WriteGeneral"/> transform.
		/// </summary>
		private void PreProcessTransform()
		{
			pageHeader = GetTransformSection(_pageheadertag);
			entry = GetTransformSection(_entrytag);
			commentsHeader = GetTransformSection(_commentsheadertag);
			comment = GetTransformSection(_commenttag);
			commentsFooter = GetTransformSection(_commentsfootertag);
			pageFooter = GetTransformSection(_pagefootertag);
			using (StringWriter sw = new StringWriter())
			{
				WriteGeneral(pageHeader, sw);
				pageHeader = sw.ToString();
			}
			using (StringWriter sw = new StringWriter())
			{
				WriteGeneral(entry, sw);
				entry = sw.ToString();
			}
			using (StringWriter sw = new StringWriter())
			{
				WriteGeneral(commentsHeader, sw);
				commentsHeader = sw.ToString();
			}
			using (StringWriter sw = new StringWriter())
			{
				WriteGeneral(comment, sw);
				comment = sw.ToString();
			}
			using (StringWriter sw = new StringWriter())
			{
				WriteGeneral(commentsFooter, sw);
				commentsFooter = sw.ToString();
			}
			using (StringWriter sw = new StringWriter())
			{
				WriteGeneral(pageFooter, sw);
				pageFooter = sw.ToString();
			}
			pageHeaderBlocks = _rblock.Split(pageHeader);
			entryBlocks = _rblock.Split(entry);
			commentsHeaderBlocks = _rblock.Split(commentsHeader);
			commentBlocks = _rblock.Split(comment);
			commentsFooterBlocks = _rblock.Split(commentsFooter);
			pageFooterBlocks = _rblock.Split(pageFooter);
		}

		/// <summary>
		/// Returns a string represantation of a color in 6-digit hex code.
		/// </summary>
		private static string GetHTMLColor(Color c)
		{
			return string.Format("{0:x}", c.R).PadLeft(2, '0') +
				   string.Format("{0:x}", c.G).PadLeft(2, '0') +
				   string.Format("{0:x}", c.B).PadLeft(2, '0');
		}

		/// <summary>
		/// Returns a comma-delimited string of concatenated integers.
		/// </summary>
		private string IntJoin(int[] values)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			foreach (int i in values)
			{
				sb.Append(",");
				sb.Append(i);
			}
			return sb.ToString().Substring(1);
		}
		
		/// <summary>
		/// Returns true if all rows have deleted state.
		/// </summary>
		/// <param name="drs">Datarows to check if all in delete state.</param>
		/// <returns>true if all rows have deleted state.</returns>
		private bool AllDeleted(DataRow[] drs)
		{
			foreach (DataRow dr in drs)
				if ((string) dr["State"] != "D")
					return false;
			return true;
		}
		#endregion

		#region Private Instance Fields
		private string pageHeader;
		private string entry;
		private string commentsHeader;
		private string comment;
		private string commentsFooter;
		private string pageFooter;
		private string[] pageHeaderBlocks;
		private string[] entryBlocks;
		private string[] commentsHeaderBlocks;
		private string[] commentBlocks;
		private string[] commentsFooterBlocks;
		private string[] pageFooterBlocks;
		private string entrySearchString;
		private string commentSearchString;
		private int selectedCommentID;
		private string transform;
		private HTMLJournalWriterSettings hjws = null;
		private Journal.OptionsRow options;
		#endregion

		#region Private Static Fields
		private const string _pageheadertag = "sectionpageheader";
		private const string _entrytag = "sectionentry";
		private const string _commentsheadertag = "sectioncommentsheader";
		private const string _commenttag = "sectioncomment";
		private const string _commentsfootertag = "sectioncommentsfooter";
		private const string _pagefootertag = "sectionpagefooter";
		static private readonly Regex _rblock = new Regex(@"(<%.*?%>)");
		static private readonly Regex _rtag = new Regex(@"(<.*?>)");
		static private readonly string _usertransform = ConstReader.GetString("_usertransform");
		static private readonly string _searchstringtransform = ConstReader.GetString("_searchstringtransform");
		#endregion
	}
}
