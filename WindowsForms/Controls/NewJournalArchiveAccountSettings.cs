using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EF.ljArchive.WindowsForms.Controls
{
	public class NewJournalArchiveAccountSettings : Genghis.Windows.Forms.WizardPage
	{
		private System.Windows.Forms.CheckBox chkAlternateServer;
		private System.Windows.Forms.TextBox txtServerURL;
		private System.Windows.Forms.GroupBox grpLogin;
		private System.Windows.Forms.TextBox txtLogin;
		private System.Windows.Forms.Label lblLogin;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.GroupBox grpAlternateServer;
		private System.Windows.Forms.CheckBox chkGetComments;
		private System.Windows.Forms.GroupBox grpOptions;
		private System.ComponentModel.IContainer components = null;

		public NewJournalArchiveAccountSettings(string title, string description) : base (title, description)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			Localizer.SetControlText(this.GetType(), grpLogin, lblLogin, lblPassword, grpOptions, chkGetComments,
				grpAlternateServer, chkAlternateServer, grpCommunity, chkCommunity);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.chkAlternateServer = new System.Windows.Forms.CheckBox();
			this.txtServerURL = new System.Windows.Forms.TextBox();
			this.grpLogin = new System.Windows.Forms.GroupBox();
			this.txtLogin = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.lblLogin = new System.Windows.Forms.Label();
			this.grpAlternateServer = new System.Windows.Forms.GroupBox();
			this.chkGetComments = new System.Windows.Forms.CheckBox();
			this.grpOptions = new System.Windows.Forms.GroupBox();
			this.grpCommunity = new System.Windows.Forms.GroupBox();
			this.cmbCommunity = new System.Windows.Forms.ComboBox();
			this.chkCommunity = new System.Windows.Forms.CheckBox();
			this.grpLogin.SuspendLayout();
			this.grpAlternateServer.SuspendLayout();
			this.grpOptions.SuspendLayout();
			this.grpCommunity.SuspendLayout();
			this.SuspendLayout();
			// 
			// chkAlternateServer
			// 
			this.chkAlternateServer.Location = new System.Drawing.Point(24, 20);
			this.chkAlternateServer.Name = "chkAlternateServer";
			this.chkAlternateServer.Size = new System.Drawing.Size(320, 16);
			this.chkAlternateServer.TabIndex = 3;
			this.chkAlternateServer.Text = "I want to connect to the following server:";
			this.chkAlternateServer.CheckedChanged += new System.EventHandler(this.chkAlternateServer_CheckedChanged);
			// 
			// txtServerURL
			// 
			this.txtServerURL.Enabled = false;
			this.txtServerURL.Location = new System.Drawing.Point(24, 44);
			this.txtServerURL.Name = "txtServerURL";
			this.txtServerURL.Size = new System.Drawing.Size(352, 21);
			this.txtServerURL.TabIndex = 4;
			this.txtServerURL.Text = "http://www.livejournal.com";
			// 
			// grpLogin
			// 
			this.grpLogin.Controls.Add(this.txtLogin);
			this.grpLogin.Controls.Add(this.txtPassword);
			this.grpLogin.Controls.Add(this.lblPassword);
			this.grpLogin.Controls.Add(this.lblLogin);
			this.grpLogin.Location = new System.Drawing.Point(8, 64);
			this.grpLogin.Name = "grpLogin";
			this.grpLogin.Size = new System.Drawing.Size(224, 88);
			this.grpLogin.TabIndex = 5;
			this.grpLogin.TabStop = false;
			this.grpLogin.Text = "Login Information";
			// 
			// txtLogin
			// 
			this.txtLogin.Location = new System.Drawing.Point(88, 22);
			this.txtLogin.Name = "txtLogin";
			this.txtLogin.Size = new System.Drawing.Size(128, 21);
			this.txtLogin.TabIndex = 0;
			this.txtLogin.TextChanged += new System.EventHandler(this.txtLogin_TextChanged);
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(88, 56);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(128, 21);
			this.txtPassword.TabIndex = 2;
			this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
			// 
			// lblPassword
			// 
			this.lblPassword.Location = new System.Drawing.Point(8, 58);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(80, 16);
			this.lblPassword.TabIndex = 3;
			this.lblPassword.Text = "Password:";
			// 
			// lblLogin
			// 
			this.lblLogin.Location = new System.Drawing.Point(8, 24);
			this.lblLogin.Name = "lblLogin";
			this.lblLogin.Size = new System.Drawing.Size(80, 16);
			this.lblLogin.TabIndex = 1;
			this.lblLogin.Text = "Login:";
			// 
			// grpAlternateServer
			// 
			this.grpAlternateServer.Controls.Add(this.txtServerURL);
			this.grpAlternateServer.Controls.Add(this.chkAlternateServer);
			this.grpAlternateServer.Location = new System.Drawing.Point(8, 158);
			this.grpAlternateServer.Name = "grpAlternateServer";
			this.grpAlternateServer.Size = new System.Drawing.Size(384, 77);
			this.grpAlternateServer.TabIndex = 7;
			this.grpAlternateServer.TabStop = false;
			this.grpAlternateServer.Text = "Alternate Server";
			// 
			// chkGetComments
			// 
			this.chkGetComments.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.chkGetComments.Checked = true;
			this.chkGetComments.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkGetComments.Location = new System.Drawing.Point(8, 24);
			this.chkGetComments.Name = "chkGetComments";
			this.chkGetComments.Size = new System.Drawing.Size(136, 40);
			this.chkGetComments.TabIndex = 8;
			this.chkGetComments.Text = "Download comments";
			this.chkGetComments.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// grpOptions
			// 
			this.grpOptions.Controls.Add(this.chkGetComments);
			this.grpOptions.Location = new System.Drawing.Point(240, 64);
			this.grpOptions.Name = "grpOptions";
			this.grpOptions.Size = new System.Drawing.Size(152, 88);
			this.grpOptions.TabIndex = 9;
			this.grpOptions.TabStop = false;
			this.grpOptions.Text = "Options";
			// 
			// grpCommunity
			// 
			this.grpCommunity.Controls.Add(this.cmbCommunity);
			this.grpCommunity.Controls.Add(this.chkCommunity);
			this.grpCommunity.Location = new System.Drawing.Point(8, 241);
			this.grpCommunity.Name = "grpCommunity";
			this.grpCommunity.Size = new System.Drawing.Size(384, 75);
			this.grpCommunity.TabIndex = 10;
			this.grpCommunity.TabStop = false;
			this.grpCommunity.Text = "Community";
			// 
			// cmbCommunity
			// 
			this.cmbCommunity.Enabled = false;
			this.cmbCommunity.Location = new System.Drawing.Point(24, 43);
			this.cmbCommunity.Name = "cmbCommunity";
			this.cmbCommunity.Size = new System.Drawing.Size(352, 21);
			this.cmbCommunity.TabIndex = 4;
			this.cmbCommunity.DropDown += new System.EventHandler(this.cmbCommunity_DropDown);
			// 
			// chkCommunity
			// 
			this.chkCommunity.Location = new System.Drawing.Point(24, 20);
			this.chkCommunity.Name = "chkCommunity";
			this.chkCommunity.Size = new System.Drawing.Size(320, 16);
			this.chkCommunity.TabIndex = 3;
			this.chkCommunity.Text = "I want to archive the following community:";
			this.chkCommunity.CheckedChanged += new System.EventHandler(this.chkCommunity_CheckedChanged);
			// 
			// NewJournalArchiveAccountSettings
			// 
			this.Controls.Add(this.grpCommunity);
			this.Controls.Add(this.grpOptions);
			this.Controls.Add(this.grpAlternateServer);
			this.Controls.Add(this.grpLogin);
			this.Name = "NewJournalArchiveAccountSettings";
			this.Size = new System.Drawing.Size(400, 327);
			this.Enter += new System.EventHandler(this.NewJournalArchiveAccountSettings_Enter);
			this.Leave += new System.EventHandler(this.NewJournalArchiveAccountSettings_Leave);
			this.Controls.SetChildIndex(this.grpLogin, 0);
			this.Controls.SetChildIndex(this.grpAlternateServer, 0);
			this.Controls.SetChildIndex(this.grpOptions, 0);
			this.Controls.SetChildIndex(this.grpCommunity, 0);
			this.grpLogin.ResumeLayout(false);
			this.grpLogin.PerformLayout();
			this.grpAlternateServer.ResumeLayout(false);
			this.grpAlternateServer.PerformLayout();
			this.grpOptions.ResumeLayout(false);
			this.grpCommunity.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox chkCommunity;
		private System.Windows.Forms.ComboBox cmbCommunity;
		private System.Windows.Forms.GroupBox grpCommunity;
		#endregion

		private void chkAlternateServer_CheckedChanged(object sender, System.EventArgs e)
		{
			txtServerURL.Enabled = chkAlternateServer.Checked;
		}
		
		void chkCommunity_CheckedChanged(object sender, System.EventArgs e)
		{
			cmbCommunity.Enabled = chkCommunity.Checked;
		}

		private void txtLogin_TextChanged(object sender, System.EventArgs e)
		{
			ValidateTextBoxes();
		}

		private void txtPassword_TextChanged(object sender, System.EventArgs e)
		{
			ValidateTextBoxes();
		}

		private void NewJournalArchiveAccountSettings_Enter(object sender, System.EventArgs e)
		{
			ValidateTextBoxes();
		}

		private void ValidateTextBoxes()
		{
			this.WizardSheet.EnableNextButton = txtLogin.Text.Length > 0 && txtPassword.Text.Length > 0;
		}

		private void NewJournalArchiveAccountSettings_Leave(object sender, System.EventArgs e)
		{
			txtServerURL.Text = txtServerURL.Text.TrimEnd('/');
		}

		public string UserName
		{
			get
			{
				return txtLogin.Text;
			}
		}

		public string Password
		{
			get
			{
				return txtPassword.Text;
			}
		}

		public string ServerURL
		{
			get
			{
				if (chkAlternateServer.Checked)
					return txtServerURL.Text;
				else
					return "http://www.livejournal.com";
			}
		}
		
		public string UseJournal
		{
			get
			{
				if (chkCommunity.Checked && cmbCommunity.Text.Length > 0)
					return cmbCommunity.Text;
				else
					return null;
			}
		}

		public bool GetComments
		{
			get
			{
				return chkGetComments.Checked;
			}
		}
		
		void cmbCommunity_DropDown(object sender, System.EventArgs e)
		{
			cmbCommunity.Items.Clear();
			try
			{
				cmbCommunity.Items.AddRange(Engine.Server.GetUseJournals(this.UserName, this.Password, this.ServerURL));
			}
			catch (EF.ljArchive.Engine.ExpectedSyncException ese)
			{
				switch (ese.ExpectedError)
				{
					case EF.ljArchive.Engine.ExpectedError.InvalidPassword:
						Dialogs.ExpectedError.Go(Dialogs.ExpectedError.ExpectedErrorCategories.SyncInvalidPassword, ese, this.ParentForm);
						break;
					case EF.ljArchive.Engine.ExpectedError.ServerNotResponding:
						Dialogs.ExpectedError.Go(Dialogs.ExpectedError.ExpectedErrorCategories.SyncServerNotResponding, ese, this.ParentForm);
						break;
				}
			}
		}
	}
}

