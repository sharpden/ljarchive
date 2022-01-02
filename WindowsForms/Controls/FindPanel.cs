using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace EF.ljArchive.WindowsForms.Controls
{
	/// <summary>
	/// Summary description for FindPanel.
	/// </summary>
	public class FindPanel : System.Windows.Forms.UserControl
	{
		private EF.ljArchive.WindowsForms.Controls.LineControl lineControl1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.TextBox txtSearchText;
		private System.Windows.Forms.Label lblFindJournalItems;
		private System.Windows.Forms.Label lblWithText;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Label lblSearch;
		private System.Windows.Forms.RadioButton rbComments;
		private System.Windows.Forms.RadioButton rbBoth;
		private System.Windows.Forms.RadioButton rbEvents;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FindPanel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			InitializeForm();
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FindPanel));
			this.btnGo = new System.Windows.Forms.Button();
			this.lineControl1 = new EF.ljArchive.WindowsForms.Controls.LineControl();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.txtSearchText = new System.Windows.Forms.TextBox();
			this.lblFindJournalItems = new System.Windows.Forms.Label();
			this.lblWithText = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.rbEvents = new System.Windows.Forms.RadioButton();
			this.lblSearch = new System.Windows.Forms.Label();
			this.rbComments = new System.Windows.Forms.RadioButton();
			this.rbBoth = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// btnGo
			// 
			this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnGo.Location = new System.Drawing.Point(368, 48);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(56, 20);
			this.btnGo.TabIndex = 0;
			this.btnGo.Text = "Go!";
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// lineControl1
			// 
			this.lineControl1.BackColor = System.Drawing.SystemColors.Control;
			this.lineControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lineControl1.Location = new System.Drawing.Point(0, 126);
			this.lineControl1.Name = "lineControl1";
			this.lineControl1.Size = new System.Drawing.Size(592, 2);
			this.lineControl1.TabIndex = 1;
			this.lineControl1.Text = "lineControl1";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(96, 96);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// txtSearchText
			// 
			this.txtSearchText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSearchText.Location = new System.Drawing.Point(192, 48);
			this.txtSearchText.Name = "txtSearchText";
			this.txtSearchText.Size = new System.Drawing.Size(160, 20);
			this.txtSearchText.TabIndex = 3;
			this.txtSearchText.Text = "";
			this.txtSearchText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchText_KeyPress);
			// 
			// lblFindJournalItems
			// 
			this.lblFindJournalItems.AutoSize = true;
			this.lblFindJournalItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblFindJournalItems.Location = new System.Drawing.Point(104, 8);
			this.lblFindJournalItems.Name = "lblFindJournalItems";
			this.lblFindJournalItems.Size = new System.Drawing.Size(202, 28);
			this.lblFindJournalItems.TabIndex = 4;
			this.lblFindJournalItems.Text = "Find Journal Items";
			// 
			// lblWithText
			// 
			this.lblWithText.AutoSize = true;
			this.lblWithText.Location = new System.Drawing.Point(112, 50);
			this.lblWithText.Name = "lblWithText";
			this.lblWithText.Size = new System.Drawing.Size(55, 16);
			this.lblWithText.TabIndex = 5;
			this.lblWithText.Text = "With Text:";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClose.Location = new System.Drawing.Point(520, 8);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(56, 20);
			this.btnClose.TabIndex = 7;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// rbEvents
			// 
			this.rbEvents.Checked = true;
			this.rbEvents.Location = new System.Drawing.Point(192, 72);
			this.rbEvents.Name = "rbEvents";
			this.rbEvents.TabIndex = 9;
			this.rbEvents.TabStop = true;
			this.rbEvents.Text = "Entries";
			// 
			// lblSearch
			// 
			this.lblSearch.AutoSize = true;
			this.lblSearch.Location = new System.Drawing.Point(112, 72);
			this.lblSearch.Name = "lblSearch";
			this.lblSearch.Size = new System.Drawing.Size(43, 16);
			this.lblSearch.TabIndex = 10;
			this.lblSearch.Text = "Search:";
			// 
			// rbComments
			// 
			this.rbComments.Location = new System.Drawing.Point(304, 72);
			this.rbComments.Name = "rbComments";
			this.rbComments.TabIndex = 11;
			this.rbComments.Text = "Comments";
			// 
			// rbBoth
			// 
			this.rbBoth.Location = new System.Drawing.Point(416, 72);
			this.rbBoth.Name = "rbBoth";
			this.rbBoth.TabIndex = 12;
			this.rbBoth.Text = "Both";
			// 
			// FindPanel
			// 
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.rbBoth);
			this.Controls.Add(this.rbComments);
			this.Controls.Add(this.lblSearch);
			this.Controls.Add(this.rbEvents);
			this.Controls.Add(this.lineControl1);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.lblWithText);
			this.Controls.Add(this.lblFindJournalItems);
			this.Controls.Add(this.txtSearchText);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.btnGo);
			this.Name = "FindPanel";
			this.Size = new System.Drawing.Size(592, 128);
			this.ResumeLayout(false);

		}
		#endregion

		private void InitializeForm()
		{
			// Localization
			Localizer.SetControlText(this.GetType(), btnGo, lblFindJournalItems, lblWithText, lblSearch, rbEvents,
				rbComments, rbBoth);
		}

		public string EscapedFindText
		{
			get
			{
				return EscapeString(txtSearchText.Text);
			}
		}

		public string FindText
		{
			get
			{
				return txtSearchText.Text;
			}
		}

		public bool SearchEvents
		{
			get
			{
				return rbEvents.Checked || rbBoth.Checked;
			}
		}

		public bool SearchComments
		{
			get
			{
				return rbComments.Checked || rbBoth.Checked;
			}
		}

		public event System.EventHandler FindClicked;
		public event System.EventHandler CloseClicked;

		private void btnGo_Click(object sender, System.EventArgs e)
		{
			if (FindClicked != null)
				FindClicked(this, EventArgs.Empty);
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			if (CloseClicked != null)
				CloseClicked(this, EventArgs.Empty);
		}

		private string EscapeString(string value)
		{
			string ret = value;
			ret = ret.Replace("*", "[*]");
			ret = ret.Replace("%", "[%]");
			ret = ret.Replace("'", "''");
			ret = ret.Replace("[", "\\[\\");
			ret = ret.Replace("]", "[]]");
			ret = ret.Replace("\\[\\", "[[]");
			return ret;
		}

		private void txtSearchText_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char) 13)
			{
				e.Handled = true;
				if (FindClicked != null)
					FindClicked(this, EventArgs.Empty);
			}
		}
	}
}
