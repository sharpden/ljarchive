using System;
using System.Drawing;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using EF.ljArchive.Common;

namespace EF.ljArchive.Plugins.WindowsForms
{
	/// <summary>
	/// Summary description for WordCount.
	/// </summary>
	public class WordCount : System.Windows.Forms.Form, IPlugin
	{
		private System.Windows.Forms.ColumnHeader chWord;
		private System.Windows.Forms.ListView lv;
		private System.Windows.Forms.ColumnHeader chCount;
		private System.Windows.Forms.Panel pnlButtons;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Panel pnlFilter;
		private System.Windows.Forms.TrackBar tckMinSize;
		private System.Windows.Forms.Label lblMinSize;
		private System.Windows.Forms.CheckBox chkCountComments;
		private System.Windows.Forms.CheckBox chkIgnoreCommonWords;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WordCount()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WordCount));
			this.chWord = new System.Windows.Forms.ColumnHeader();
			this.lv = new System.Windows.Forms.ListView();
			this.chCount = new System.Windows.Forms.ColumnHeader();
			this.pnlButtons = new System.Windows.Forms.Panel();
			this.chkIgnoreCommonWords = new System.Windows.Forms.CheckBox();
			this.chkCountComments = new System.Windows.Forms.CheckBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.pnlFilter = new System.Windows.Forms.Panel();
			this.tckMinSize = new System.Windows.Forms.TrackBar();
			this.lblMinSize = new System.Windows.Forms.Label();
			this.pnlButtons.SuspendLayout();
			this.pnlFilter.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tckMinSize)).BeginInit();
			this.SuspendLayout();
			// 
			// chWord
			// 
			this.chWord.Text = "Word";
			this.chWord.Width = 180;
			// 
			// lv
			// 
			this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																				 this.chWord,
																				 this.chCount});
			this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lv.Location = new System.Drawing.Point(0, 72);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(360, 333);
			this.lv.TabIndex = 4;
			this.lv.View = System.Windows.Forms.View.Details;
			this.lv.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lv_ColumnClick);
			// 
			// chCount
			// 
			this.chCount.Text = "Count";
			// 
			// pnlButtons
			// 
			this.pnlButtons.Controls.Add(this.chkIgnoreCommonWords);
			this.pnlButtons.Controls.Add(this.chkCountComments);
			this.pnlButtons.Controls.Add(this.btnOK);
			this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlButtons.Location = new System.Drawing.Point(0, 405);
			this.pnlButtons.Name = "pnlButtons";
			this.pnlButtons.Size = new System.Drawing.Size(360, 40);
			this.pnlButtons.TabIndex = 5;
			// 
			// chkIgnoreCommonWords
			// 
			this.chkIgnoreCommonWords.Checked = true;
			this.chkIgnoreCommonWords.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkIgnoreCommonWords.Location = new System.Drawing.Point(128, 8);
			this.chkIgnoreCommonWords.Name = "chkIgnoreCommonWords";
			this.chkIgnoreCommonWords.Size = new System.Drawing.Size(136, 24);
			this.chkIgnoreCommonWords.TabIndex = 4;
			this.chkIgnoreCommonWords.Text = "Ignore common words";
			this.chkIgnoreCommonWords.CheckedChanged += new System.EventHandler(this.chkCountCommonWords_CheckedChanged);
			// 
			// chkCountComments
			// 
			this.chkCountComments.Location = new System.Drawing.Point(8, 8);
			this.chkCountComments.Name = "chkCountComments";
			this.chkCountComments.Size = new System.Drawing.Size(112, 24);
			this.chkCountComments.TabIndex = 3;
			this.chkCountComments.Text = "Count comments";
			this.chkCountComments.CheckedChanged += new System.EventHandler(this.chkCountComments_CheckedChanged);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(272, 8);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "&OK";
			// 
			// pnlFilter
			// 
			this.pnlFilter.Controls.Add(this.tckMinSize);
			this.pnlFilter.Controls.Add(this.lblMinSize);
			this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlFilter.Location = new System.Drawing.Point(0, 0);
			this.pnlFilter.Name = "pnlFilter";
			this.pnlFilter.Size = new System.Drawing.Size(360, 72);
			this.pnlFilter.TabIndex = 6;
			// 
			// tckMinSize
			// 
			this.tckMinSize.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tckMinSize.Location = new System.Drawing.Point(0, 30);
			this.tckMinSize.Maximum = 20;
			this.tckMinSize.Name = "tckMinSize";
			this.tckMinSize.Size = new System.Drawing.Size(360, 42);
			this.tckMinSize.TabIndex = 1;
			this.tckMinSize.Value = 4;
			this.tckMinSize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tckMinSize_MouseUp);
			this.tckMinSize.Scroll += new System.EventHandler(this.tckMinSize_Scroll);
			// 
			// lblMinSize
			// 
			this.lblMinSize.Location = new System.Drawing.Point(8, 8);
			this.lblMinSize.Name = "lblMinSize";
			this.lblMinSize.Size = new System.Drawing.Size(192, 16);
			this.lblMinSize.TabIndex = 0;
			this.lblMinSize.Text = "Minimum Size: 4 characters.";
			// 
			// WordCount
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(360, 445);
			this.Controls.Add(this.lv);
			this.Controls.Add(this.pnlButtons);
			this.Controls.Add(this.pnlFilter);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WordCount";
			this.ShowInTaskbar = false;
			this.Text = "Word Count Analyzer";
			this.pnlButtons.ResumeLayout(false);
			this.pnlFilter.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tckMinSize)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region IPlugin Members
		public Image MenuIcon
		{
			get { return new System.Drawing.Bitmap(this.GetType(), "res.WordCount.png"); }
		}

		public string Description
		{
			get { return "Analyzes word counts in your journal."; }
		}

		public string Author
		{
			get { return "Erik Frey"; }
		}

		public void Go(Journal j)
		{
			this.j = j;
			UpdateWords();
			ShowDialog();
		}

		public string Title
		{
			get { return "Word Count Analyzer"; }
		}

		public string URL
		{
			get { return "http://ljarchive.sourceforge.net/"; }
		}

		public Version Version
		{
			get { return new Version(0, 9, 3, 1); }
		}

		public object Settings
		{
			get { return null; }
			set { }
		}

		public int SelectedEventID
		{
			set { }
		}
		#endregion

		private Journal j;
		private Hashtable words;

		protected void UpdateListView()
		{
			lv.BeginUpdate();
			lv.Items.Clear();
			lv.ListViewItemSorter = null;
			IDictionaryEnumerator ide = words.GetEnumerator();
			while (ide.MoveNext())
			{
				string s = (string) ide.Key;
				if (s.Length >= tckMinSize.Value)
					lv.Items.Add(new ListViewItem(new string[] {s, ide.Value.ToString()}));
			}
			lv.ListViewItemSorter = new ListViewItemComparer(1);
			lv.Sort();
			lv.EndUpdate();
		}

		private void UpdateWords()
		{
			Cursor.Current = Cursors.WaitCursor;
			words = Plugins.Core.WordCount.GetWordCount(j, chkCountComments.Checked, chkIgnoreCommonWords.Checked);
			UpdateListView();
			Cursor.Current = Cursors.Default;
		}

		private void lv_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			lv.ListViewItemSorter = new ListViewItemComparer(e.Column);
			lv.Sort();
		}

		private void tckMinSize_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			UpdateListView();
			Cursor.Current = Cursors.Default;
		}

		private void tckMinSize_Scroll(object sender, System.EventArgs e)
		{
			lblMinSize.Text = string.Format("Minimum Size: {0} characters.", tckMinSize.Value);
		}

		private void chkCountComments_CheckedChanged(object sender, System.EventArgs e)
		{
			UpdateWords();
		}

		private void chkCountCommonWords_CheckedChanged(object sender, System.EventArgs e)
		{
			UpdateWords();
		}
	}

	#region ListViewItemComparer
	class ListViewItemComparer : IComparer
	{
		private int col;

		public ListViewItemComparer() 
		{
			col=0;
		}
		public ListViewItemComparer(int column) 
		{
			col=column;
		}

		#region IComparer Members
		public int Compare(object x, object y)
		{
			if (col == 1)
				return int.Parse(((ListViewItem)y).SubItems[1].Text).CompareTo(
					int.Parse(((ListViewItem)x).SubItems[1].Text));
			else
				return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
		}
		#endregion
	}
	#endregion
}
