using System;
using System.ComponentModel;
using EF.ljArchive.Common;
using System.Collections;
using System.Runtime.Serialization;
using System.Xml;

namespace XMLJournalWriter
{
	#region XMLJournalWriterSettings
	[Serializable()]
	public class XMLJournalWriterSettings
	{
		public XMLJournalWriterSettings() {}

		public XMLJournalWriterSettings(bool moodField, bool musicField, bool exportPrivate, bool exportProtected)
		{
			this.moodField = moodField;
			this.musicField = musicField;
			this.exportPrivate = exportPrivate;
			this.exportProtected = exportProtected;
		}

		[Description("Export current mood for each journal entry.")]
		[DefaultValue(true)]
		public bool MoodField
		{
			get
			{
				return moodField;
			}
			set
			{
				moodField = value;
			}
		}

		[Description("Export current music for each journal entry.")]
		[DefaultValue(true)]
		public bool MusicField
		{
			get
			{
				return musicField;
			}
			set
			{
				musicField = value;
			}
		}

		[Description("Export private journal entries.")]
		[DefaultValue(true)]
		public bool ExportPrivate
		{
			get
			{
				return exportPrivate;
			}
			set
			{
				exportPrivate = value;
			}
		}

		[Description("Export protected (friends) journal entries.")]
		[DefaultValue(true)]
		public bool ExportProtected
		{
			get
			{
				return exportProtected;
			}
			set
			{
				exportProtected = value;
			}
		}

		private bool moodField;
		private bool musicField;
		private bool exportPrivate;
		private bool exportProtected;
	}
	#endregion

	/// <summary>
	/// Writes a journal to a stream in XML.
	/// </summary>
	public class XMLJournalWriter : IJournalWriter
	{
		#region Public Instance Constructors
		public XMLJournalWriter()
		{
			settings = new XMLJournalWriterSettings(true, true, true, true);
		}
		#endregion

		#region IJournalWriter Members
		public void WriteJournal(System.IO.Stream s, Journal j, int[] eventIDs, int[] commentIDs, bool header, bool footer)
		{
			XmlTextWriter xtw = new XmlTextWriter(s, System.Text.Encoding.UTF8);
			xtw.Formatting = Formatting.Indented;
			xtw.WriteStartDocument();
			xtw.WriteStartElement("livejournal");
			foreach (Journal.EventsRow er in j.Events)
			{
				if (eventIDs != null && !((IList) eventIDs).Contains(er.ID))
					continue;
				if (!er.IsSecurityNull() && er.Security == "usemask" && !settings.ExportProtected)
					continue;
				if (!er.IsSecurityNull() && er.Security == "private" && !settings.ExportPrivate)
					continue;
				xtw.WriteStartElement("entry");
				xtw.WriteElementString("itemid", er.ID.ToString());
				if (!er.IsDateNull())
					xtw.WriteElementString("eventtime", er.Date.ToString(DateTimeFormat));
				if (!er.IsSubjectNull())
					xtw.WriteElementString("subject", er.Subject);
				if (!er.IsBodyNull())
					xtw.WriteElementString("event", er.Body);
				if (!er.IsSecurityNull() && er.Security.Trim() != string.Empty)
					xtw.WriteElementString("security", er.Security);
				else if (!er.IsSecurityNull())
					xtw.WriteElementString("security", "public");
				if (!er.IsAllowMaskNull())
					xtw.WriteElementString("allowmask", er.AllowMask.ToString());
				if (!er.IsCurrentMusicNull() && settings.MusicField)
					xtw.WriteElementString("current_music", er.CurrentMusic);
				if (!er.IsCurrentMoodNull() && settings.MoodField)
				{
					xtw.WriteElementString("current_mood", er.CurrentMood);
				}
				else if (!er.IsCurrentMoodIDNull() && settings.MoodField)
				{
					if (j.Moods.FindByID(er.CurrentMoodID) != null)
						xtw.WriteElementString("current_mood", j.Moods.FindByID(er.CurrentMoodID).Name);
				}
				if (!er.IsTagListNull())
					xtw.WriteElementString("taglist", er.TagList);
				foreach (Journal.CommentsRow cr in j.Comments.Select("JItemID = " + er.ID.ToString()))
				{
					if (commentIDs != null && !((IList) commentIDs).Contains(cr.ID))
						continue;
					xtw.WriteStartElement("comment");
					xtw.WriteElementString("itemid", cr.ID.ToString());
					if (cr.ParentID != 0)
						xtw.WriteElementString("parent_itemid", cr.ParentID.ToString());
					xtw.WriteElementString("eventtime", cr.Date.ToString(DateTimeFormat));
					xtw.WriteElementString("subject", cr.Subject);
					xtw.WriteElementString("event", cr.Body);
					xtw.WriteStartElement("author");
					xtw.WriteElementString("name", j.Users.FindByID(cr.PosterID).User);
					xtw.WriteElementString("email", j.Users.FindByID(cr.PosterID).User + "@livejournal.com");
					xtw.WriteEndElement();
					xtw.WriteEndElement();
				}
				xtw.WriteEndElement();
			}
			xtw.WriteEndElement();
			xtw.WriteEndDocument();
			xtw.Close();
		}

		public string Filter
		{
			get { return "XML files (*.xml)|*.xml|All files (*.*)|*.*"; }
		}

		public object Settings
		{
			get { return settings; }
			set
			{
				if (value is XMLJournalWriterSettings)
					settings = (XMLJournalWriterSettings) value;
				else
					throw new ArgumentException("Settings must be of type XMLJournalWriterSettings", "Settings");
			}
		}

		public string Description
		{
			get
			{
				return "Exports to XML, using the same format as Livejournal's export feature.  Useful for "
					+ "interoperability with other services such as LJBook (http://www.ljbook.com/xml.html).";
			}
		}

		public string Author
		{
			get { return "Erik Frey"; }
		}

		public string Name
		{
			get { return "XML Writer"; }
		}

		public string URL
		{
			get { return "http://ljarchive.sourceforge.net/"; }
		}

		public Version Version
		{
			get { return new Version(0, 9, 4, 0); }
		}
		#endregion

		#region Private Instance Fields
		private XMLJournalWriterSettings settings;
		private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
		#endregion
	}
}
