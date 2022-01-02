using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace EF.ljArchive.WindowsForms.Dialogs
{
	/// <summary>
	/// Summary description for Properties.
	/// </summary>
	public class Properties : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblLocationTag;
		private System.Windows.Forms.Label lblLocation;
		private System.Windows.Forms.Label lblSizeTag;
		private System.Windows.Forms.Label lblSize;
		private System.Windows.Forms.GroupBox grpJournalSettings;
		private System.Windows.Forms.Label lblEntryCount;
		private System.Windows.Forms.Label lblEntryCountTag;
		private System.Windows.Forms.Label lblUsernameTag;
		private System.Windows.Forms.Label lblUsername;
		private System.Windows.Forms.Label lblServerURL;
		private System.Windows.Forms.TextBox txtServerURL;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.Label lblWarning;
		private System.Windows.Forms.Label lblCommentCount;
		private System.Windows.Forms.Label lblCommentCountTag;
		private System.Windows.Forms.CheckBox chkGetComments;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Properties()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public string FriendlyFileSize(long fileSize)
		{
			double resultSize = fileSize;
			string fileSuffix = " bytes";
			if (resultSize / 1024 > 1)
			{
				resultSize /= 1024;
				fileSuffix = " KB";
			}
			if (resultSize / 1024 > 1)
			{
				resultSize /= 1024;
				fileSuffix = " MB";
			}
			if (resultSize / 1024 > 1)
			{
				resultSize /= 1024;
				fileSuffix = " GB";
			}
			return string.Format("{0:###0.00} {1} ({2:#,#} bytes)", resultSize, fileSuffix, fileSize);
		}

		public Properties(Engine.Journal j) : this()
		{
			Localizer.SetControlText(this.GetType(), this, lblUsernameTag, lblLocationTag, lblSizeTag,
				lblEntryCountTag, lblCommentCountTag, lblEntryCount, lblCommentCount, grpJournalSettings,
				lblServerURL, lblPassword, lblWarning, chkGetComments, btnOK, btnCancel);
			this.Text = string.Format(this.Text, j.Path);
			lblUsername.Text = j.Options.UserName;
			lblLocation.Text = System.IO.Path.GetDirectoryName(j.Path);
			lblSize.Text = FriendlyFileSize((new System.IO.FileInfo(j.Path)).Length);
			lblEntryCount.Text = string.Format(lblEntryCount.Text, j.Events.Count);
			lblCommentCount.Text = string.Format(lblCommentCount.Text, j.Comments.Count);
			txtServerURL.Text = j.Options.ServerURL;
			chkGetComments.Checked = j.Options.GetComments;
		}

		static public void Go(Engine.Journal j)
		{
			Properties p = new Properties(j);
			if (p.ShowDialog() == DialogResult.OK)
			{
				if (p.txtPassword.Text.Length > 0)
					j.SetPassword(p.txtPassword.Text);
				j.Options.GetComments = p.chkGetComments.Checked;
				j.Options.ServerURL = p.txtServerURL.Text.TrimEnd('/');
				j.Save();
			}
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
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblLocationTag = new System.Windows.Forms.Label();
			this.lblLocation = new System.Windows.Forms.Label();
			this.lblSizeTag = new System.Windows.Forms.Label();
			this.lblSize = new System.Windows.Forms.Label();
			this.grpJournalSettings = new System.Windows.Forms.GroupBox();
			this.chkGetComments = new System.Windows.Forms.CheckBox();
			this.lblWarning = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.txtServerURL = new System.Windows.Forms.TextBox();
			this.lblServerURL = new System.Windows.Forms.Label();
			this.lblEntryCount = new System.Windows.Forms.Label();
			this.lblEntryCountTag = new System.Windows.Forms.Label();
			this.lblUsername = new System.Windows.Forms.Label();
			this.lblUsernameTag = new System.Windows.Forms.Label();
			this.lblCommentCount = new System.Windows.Forms.Label();
			this.lblCommentCountTag = new System.Windows.Forms.Label();
			this.grpJournalSettings.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(160, 368);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "O&K";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(256, 368);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			// 
			// lblLocationTag
			// 
			this.lblLocationTag.AutoSize = true;
			this.lblLocationTag.Location = new System.Drawing.Point(16, 64);
			this.lblLocationTag.Name = "lblLocationTag";
			this.lblLocationTag.Size = new System.Drawing.Size(51, 13);
			this.lblLocationTag.TabIndex = 2;
			this.lblLocationTag.Text = "Location:";
			// 
			// lblLocation
			// 
			this.lblLocation.Location = new System.Drawing.Point(144, 64);
			this.lblLocation.Name = "lblLocation";
			this.lblLocation.Size = new System.Drawing.Size(192, 40);
			this.lblLocation.TabIndex = 3;
			// 
			// lblSizeTag
			// 
			this.lblSizeTag.AutoSize = true;
			this.lblSizeTag.Location = new System.Drawing.Point(16, 104);
			this.lblSizeTag.Name = "lblSizeTag";
			this.lblSizeTag.Size = new System.Drawing.Size(30, 13);
			this.lblSizeTag.TabIndex = 4;
			this.lblSizeTag.Text = "Size:";
			// 
			// lblSize
			// 
			this.lblSize.Location = new System.Drawing.Point(144, 104);
			this.lblSize.Name = "lblSize";
			this.lblSize.Size = new System.Drawing.Size(192, 16);
			this.lblSize.TabIndex = 5;
			// 
			// grpJournalSettings
			// 
			this.grpJournalSettings.Controls.Add(this.chkGetComments);
			this.grpJournalSettings.Controls.Add(this.lblWarning);
			this.grpJournalSettings.Controls.Add(this.txtPassword);
			this.grpJournalSettings.Controls.Add(this.lblPassword);
			this.grpJournalSettings.Controls.Add(this.txtServerURL);
			this.grpJournalSettings.Controls.Add(this.lblServerURL);
			this.grpJournalSettings.Location = new System.Drawing.Point(8, 216);
			this.grpJournalSettings.Name = "grpJournalSettings";
			this.grpJournalSettings.Size = new System.Drawing.Size(328, 144);
			this.grpJournalSettings.TabIndex = 6;
			this.grpJournalSettings.TabStop = false;
			this.grpJournalSettings.Text = "Journal Settings";
			// 
			// chkGetComments
			// 
			this.chkGetComments.Location = new System.Drawing.Point(88, 120);
			this.chkGetComments.Name = "chkGetComments";
			this.chkGetComments.Size = new System.Drawing.Size(224, 16);
			this.chkGetComments.TabIndex = 5;
			this.chkGetComments.Text = "Download Comments";
			// 
			// lblWarning
			// 
			this.lblWarning.Location = new System.Drawing.Point(8, 88);
			this.lblWarning.Name = "lblWarning";
			this.lblWarning.Size = new System.Drawing.Size(312, 32);
			this.lblWarning.TabIndex = 4;
			this.lblWarning.Text = "Only enter a new password into the text box if you want to change it.";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(120, 56);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(192, 21);
			this.txtPassword.TabIndex = 3;
			// 
			// lblPassword
			// 
			this.lblPassword.Location = new System.Drawing.Point(8, 56);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(112, 16);
			this.lblPassword.TabIndex = 2;
			this.lblPassword.Text = "Password:";
			// 
			// txtServerURL
			// 
			this.txtServerURL.Location = new System.Drawing.Point(120, 22);
			this.txtServerURL.Name = "txtServerURL";
			this.txtServerURL.Size = new System.Drawing.Size(192, 21);
			this.txtServerURL.TabIndex = 1;
			// 
			// lblServerURL
			// 
			this.lblServerURL.Location = new System.Drawing.Point(8, 24);
			this.lblServerURL.Name = "lblServerURL";
			this.lblServerURL.Size = new System.Drawing.Size(112, 16);
			this.lblServerURL.TabIndex = 0;
			this.lblServerURL.Text = "Server URL:";
			// 
			// lblEntryCount
			// 
			this.lblEntryCount.Location = new System.Drawing.Point(144, 144);
			this.lblEntryCount.Name = "lblEntryCount";
			this.lblEntryCount.Size = new System.Drawing.Size(192, 16);
			this.lblEntryCount.TabIndex = 8;
			this.lblEntryCount.Text = "{0} entries";
			// 
			// lblEntryCountTag
			// 
			this.lblEntryCountTag.AutoSize = true;
			this.lblEntryCountTag.Location = new System.Drawing.Point(16, 144);
			this.lblEntryCountTag.Name = "lblEntryCountTag";
			this.lblEntryCountTag.Size = new System.Drawing.Size(69, 13);
			this.lblEntryCountTag.TabIndex = 7;
			this.lblEntryCountTag.Text = "Entry Count:";
			// 
			// lblUsername
			// 
			this.lblUsername.Location = new System.Drawing.Point(144, 24);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(192, 16);
			this.lblUsername.TabIndex = 10;
			// 
			// lblUsernameTag
			// 
			this.lblUsernameTag.AutoSize = true;
			this.lblUsernameTag.Location = new System.Drawing.Point(16, 24);
			this.lblUsernameTag.Name = "lblUsernameTag";
			this.lblUsernameTag.Size = new System.Drawing.Size(59, 13);
			this.lblUsernameTag.TabIndex = 9;
			this.lblUsernameTag.Text = "Username:";
			// 
			// lblCommentCount
			// 
			this.lblCommentCount.Location = new System.Drawing.Point(144, 184);
			this.lblCommentCount.Name = "lblCommentCount";
			this.lblCommentCount.Size = new System.Drawing.Size(192, 16);
			this.lblCommentCount.TabIndex = 12;
			this.lblCommentCount.Text = "{0} comments";
			// 
			// lblCommentCountTag
			// 
			this.lblCommentCountTag.AutoSize = true;
			this.lblCommentCountTag.Location = new System.Drawing.Point(16, 184);
			this.lblCommentCountTag.Name = "lblCommentCountTag";
			this.lblCommentCountTag.Size = new System.Drawing.Size(88, 13);
			this.lblCommentCountTag.TabIndex = 11;
			this.lblCommentCountTag.Text = "Comment Count:";
			// 
			// Properties
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(344, 399);
			this.Controls.Add(this.lblCommentCount);
			this.Controls.Add(this.lblCommentCountTag);
			this.Controls.Add(this.lblUsernameTag);
			this.Controls.Add(this.lblEntryCountTag);
			this.Controls.Add(this.lblSizeTag);
			this.Controls.Add(this.lblLocationTag);
			this.Controls.Add(this.lblUsername);
			this.Controls.Add(this.lblEntryCount);
			this.Controls.Add(this.grpJournalSettings);
			this.Controls.Add(this.lblSize);
			this.Controls.Add(this.lblLocation);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Properties";
			this.Text = "{0} Properties";
			this.grpJournalSettings.ResumeLayout(false);
			this.grpJournalSettings.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion

	}
}
