using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;

namespace EF.ljArchive.Engine.HTML
{
	/// <summary>
	/// Settings class for <see cref="HTMLJournalWriter"/>.
	/// </summary>
	[Serializable()]
	public class HTMLJournalWriterSettings
	{
		/// <summary>
		/// Creates a new instance of the <see cref="HTMLJournalWriterSettings"/> class.
		/// </summary>
		public HTMLJournalWriterSettings() {}

		/// <summary>
		/// Creates a default instance of the <see cref="HTMLJournalWriterSettings"/> class.
		/// </summary>
		/// <returns>a default instance of the <see cref="HTMLJournalWriterSettings"/> class.</returns>
		public static HTMLJournalWriterSettings CreateDefault()
		{
			HTMLJournalWriterSettings hjws = new HTMLJournalWriterSettings();
			hjws.protectedIconPath = "/img/icon_protected.gif";
			hjws.privateIconPath = "/img/icon_private.gif";
			hjws.userInfoIconPath = "/img/userinfo.gif";
			hjws.communityInfoIconPath = "/img/community.gif";
			hjws.spacerPath = "/img/dot.gif";
			hjws.pageBackgroundColor = Color.FromArgb(38, 72, 128);
			hjws.pageAlternateBackgroundColor = Color.FromArgb(241, 232, 0);
			hjws.pageTextColor = Color.White;
			hjws.pageLinkColor = Color.Blue;
			hjws.pageVisitedLinkColor = Color.FromArgb(206, 73, 0);
			hjws.pageActiveLinkColor = Color.FromArgb(255, 158, 43);
			hjws.entryBackgroundColor = Color.White;
			hjws.entryTextColor = Color.Black;
			hjws.entryHeaderBackgroundColor = Color.FromArgb(0, 84, 92);
			hjws.entryHeaderTextColor = Color.White;
			hjws.entryFooterBackgroundColor = Color.FromArgb(255, 237, 221);
			hjws.entryFooterTextColor = Color.Black;
			hjws.highlightBackgroundColor = Color.Yellow;
			hjws.highlightTextColor = Color.Black;
			hjws.commentBackgroundColor = Color.FromArgb(167, 199, 232);
			hjws.commentTextColor = Color.Black;
			hjws.screenedCommentBackgroundColor = Color.LightGray;
			hjws.screenedCommentTextColor = Color.DarkGray;
			hjws.selectedCommentBackgroundColor = Color.Coral;
			hjws.selectedCommentTextColor = Color.Blue;
			hjws.blockImages = false;
			return hjws;
		}

		/// <summary>
		/// Relative path for the icon that signifies protected entries.
		/// </summary>
		[Category("Advanced")]
		[Description("Relative path for the icon that signifies protected entries.")]
		public string ProtectedIconPath
		{
			get { return this.protectedIconPath; }
			set { this.protectedIconPath = value; }
		}

		/// <summary>
		/// Relative path for the icon that signifies private entries.
		/// </summary>
		[Category("Advanced")]
		[Description("Relative path for the icon that signifies private entries.")]
		public string PrivateIconPath
		{
			get { return this.privateIconPath; }
			set { this.privateIconPath = value; }
		}

		/// <summary>
		/// Relative path for the user info icon.
		/// </summary>
		[Category("Advanced")]
		[Description("Relative path for the user info icon.")]
		public string UserInfoIconPath
		{
			get { return this.userInfoIconPath; }
			set { this.userInfoIconPath = value; }
		}
		
		/// <summary>
		/// Relative path for the community info icon.
		/// </summary>
		[Category("Advanced")]
		[Description("Relative path for the community info icon.")]
		public string CommunityInfoIconPath
		{
			get { return this.communityInfoIconPath; }
			set { this.communityInfoIconPath = value; }
		}

		/// <summary>
		/// Relative path for the blank image used to nudge html elements.
		/// </summary>
		[Category("Advanced")]
		[Description("Relative path for the blank image used to nudge html elements.")]
		public string SpacerPath
		{
			get { return this.spacerPath; }
			set { this.spacerPath = value; }
		}

		/// <summary>
		/// Background color of the page.
		/// </summary>
		[Category("Color")]
		[Description("Background color of the page.")]
		public Color PageBackgroundColor
		{
			get { return this.pageBackgroundColor; }
			set { this.pageBackgroundColor = value; }
		}

		/// <summary>
		/// Alternate background color, used for alternating backgrounds.
		/// </summary>
		[Category("Color")]
		[Description("Alternate background color, used for alternating backgrounds.")]
		public Color PageAlternateBackgroundColor
		{
			get { return this.pageAlternateBackgroundColor; }
			set { this.pageAlternateBackgroundColor = value; }
		}

		/// <summary>
		/// Text color of the page.
		/// </summary>
		[Category("Color")]
		[Description("Text color of the page.")]
		public Color PageTextColor
		{
			get { return this.pageTextColor; }
			set { this.pageTextColor = value; }
		}

		/// <summary>
		/// Link color of the page.
		/// </summary>
		[Category("Color")]
		[Description("Link color of the page.")]
		public Color PageLinkColor
		{
			get { return this.pageLinkColor; }
			set { this.pageLinkColor = value; }
		}

		/// <summary>
		/// Visited link color of the page.
		/// </summary>
		[Category("Color")]
		[Description("Visited link color of the page.")]
		public Color PageVisitedLinkColor
		{
			get { return this.pageVisitedLinkColor; }
			set { this.pageVisitedLinkColor = value; }
		}

		/// <summary>
		/// Active link color of the page.
		/// </summary>
		[Category("Color")]
		[Description("Active link color of the page.")]
		public Color PageActiveLinkColor
		{
			get { return this.pageActiveLinkColor; }
			set { this.pageActiveLinkColor = value; }
		}

		/// <summary>
		/// Background color of a journal entry.
		/// </summary>
		[Category("Color")]
		[Description("Background color of a journal entry.")]
		public Color EntryBackgroundColor
		{
			get { return this.entryBackgroundColor; }
			set { this.entryBackgroundColor = value; }
		}

		/// <summary>
		/// Text color of a journal entry.
		/// </summary>
		[Category("Color")]
		[Description("Text color of a journal entry.")]		
		public Color EntryTextColor
		{
			get { return this.entryTextColor; }
			set { this.entryTextColor = value; }
		}

		/// <summary>
		/// Background color of a journal entry title.
		/// </summary>
		[Category("Color")]
		[Description("Background color of a journal entry title.")]
		public Color EntryHeaderBackgroundColor
		{
			get { return this.entryHeaderBackgroundColor; }
			set { this.entryHeaderBackgroundColor = value; }
		}

		/// <summary>
		/// Text color of a journal entry title.
		/// </summary>
		[Category("Color")]
		[Description("Text color of a journal entry title.")]
		public Color EntryHeaderTextColor
		{
			get { return this.entryHeaderTextColor; }
			set { this.entryHeaderTextColor = value; }
		}

		/// <summary>
		/// Background color of a journal entry title.
		/// </summary>
		[Category("Color")]
		[Description("Background color of a journal entry title.")]
		public Color EntryFooterBackgroundColor
		{
			get { return this.entryFooterBackgroundColor; }
			set { this.entryFooterBackgroundColor = value; }
		}

		/// <summary>
		/// Text color of a journal entry title.
		/// </summary>
		[Category("Color")]
		[Description("Text color of a journal entry title.")]
		public Color EntryFooterTextColor
		{
			get { return this.entryFooterTextColor; }
			set { this.entryFooterTextColor = value; }
		}

		/// <summary>
		/// Background color of highlighted text.
		/// </summary>
		[Category("Color")]
		[Description("Background color of highlighted text.")]
		public Color HighlightBackgroundColor
		{
			get { return this.highlightBackgroundColor; }
			set { this.highlightBackgroundColor = value; }
		}

		/// <summary>
		/// Text color of highlighted text.
		/// </summary>
		[Category("Color")]
		[Description("Text color of highlighted text.")]
		public Color HighlightTextColor
		{
			get { return this.highlightTextColor; }
			set { this.highlightTextColor = value; }
		}

		/// <summary>
		/// Background color of a journal comment.
		/// </summary>
		[Category("Color")]
		[Description("Background color of a journal comment.")]
		public Color CommentBackgroundColor
		{
			get { return this.commentBackgroundColor; }
			set { this.commentBackgroundColor = value; }
		}

		/// <summary>
		/// Text color of a journal comment.
		/// </summary>
		[Category("Color")]
		[Description("Text color of a journal comment.")]
		public Color CommentTextColor
		{
			get { return this.commentTextColor; }
			set { this.commentTextColor = value; }
		}

		/// <summary>
		/// Background color of a screened journal comment.
		/// </summary>
		[Category("Color")]
		[Description("Background color of a screened journal comment.")]
		public Color ScreenedCommentBackgroundColor
		{
			get { return this.screenedCommentBackgroundColor; }
			set { this.screenedCommentBackgroundColor = value; }
		}

		/// <summary>
		/// Text color of a screened journal comment.
		/// </summary>
		[Category("Color")]
		[Description("Text color of a screened journal comment.")]
		public Color ScreenedCommentTextColor
		{
			get { return this.screenedCommentTextColor; }
			set { this.screenedCommentTextColor = value; }
		}

		/// <summary>
		/// Background color of a selected journal comment.
		/// </summary>
		[Category("Color")]
		[Description("Background color of a selected journal comment.")]
		public Color SelectedCommentBackgroundColor
		{
			get { return this.selectedCommentBackgroundColor; }
			set { this.selectedCommentBackgroundColor = value; }
		}

		/// <summary>
		/// Text color of a selected journal comment.
		/// </summary>
		[Category("Color")]
		[Description("Text color of a selected journal comment.")]
		public Color SelectedCommentTextColor
		{
			get { return this.selectedCommentTextColor; }
			set { this.selectedCommentTextColor = value; }
		}

		/// <summary>
		/// If true, blocks all entry and comment images.
		/// </summary>
		[Category("Misc")]
		[Description("If true, blocks all entry and comment images.")]
		public bool BlockImages
		{
			get { return this.blockImages; }
			set { this.blockImages = value; }
		}

		private string protectedIconPath;
		private string privateIconPath;
		private string userInfoIconPath;
		private string communityInfoIconPath;
		private string spacerPath;
		private Color pageBackgroundColor;
		private Color pageAlternateBackgroundColor;
		private Color pageTextColor;
		private Color pageLinkColor;
		private Color pageVisitedLinkColor;
		private Color pageActiveLinkColor;
		private Color entryBackgroundColor;
		private Color entryTextColor;
		private Color entryHeaderBackgroundColor;
		private Color entryHeaderTextColor;
		private Color entryFooterBackgroundColor;
		private Color entryFooterTextColor;
		private Color highlightBackgroundColor;
		private Color highlightTextColor;
		private Color commentBackgroundColor;
		private Color commentTextColor;
		private Color screenedCommentBackgroundColor;
		private Color screenedCommentTextColor;
		private Color selectedCommentBackgroundColor;
		private Color selectedCommentTextColor;
		private bool blockImages;
	}
}
