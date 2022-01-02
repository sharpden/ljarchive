using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EF.ljArchive.WindowsForms.Controls
{
	public class NewJournalArchiveIntroPage : Genghis.Windows.Forms.WizardPage
	{
		private System.Windows.Forms.Label lblIntro;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.Button btnPathDialog;
		private System.Windows.Forms.SaveFileDialog sfd;
		private System.ComponentModel.IContainer components = null;

		public NewJournalArchiveIntroPage(string title, string description) : base (title, description)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			Localizer.SetControlText(this.GetType(), lblIntro);
			sfd.Filter = Localizer.GetString(this.GetType(), "sfd.Filter");
			sfd.FileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
				sfd.FileName);
			txtPath.Text = sfd.FileName;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NewJournalArchiveIntroPage));
			this.lblIntro = new System.Windows.Forms.Label();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.btnPathDialog = new System.Windows.Forms.Button();
			this.sfd = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// lblIntro
			// 
			this.lblIntro.Location = new System.Drawing.Point(8, 88);
			this.lblIntro.Name = "lblIntro";
			this.lblIntro.Size = new System.Drawing.Size(384, 72);
			this.lblIntro.TabIndex = 1;
			this.lblIntro.Text = "Welcome!  This dialog will guide you through creating a new journal archive.\n\nFir" +
				"st, please specify where you\'d like to store the journal archive file.\n\nAfter yo" +
				"u have selected a file location, click \'Next\'.";
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(32, 184);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(264, 20);
			this.txtPath.TabIndex = 2;
			this.txtPath.Text = "";
			this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
			// 
			// btnPathDialog
			// 
			this.btnPathDialog.Image = ((System.Drawing.Image)(resources.GetObject("btnPathDialog.Image")));
			this.btnPathDialog.Location = new System.Drawing.Point(312, 184);
			this.btnPathDialog.Name = "btnPathDialog";
			this.btnPathDialog.Size = new System.Drawing.Size(40, 20);
			this.btnPathDialog.TabIndex = 3;
			this.btnPathDialog.Click += new System.EventHandler(this.btnPathDialog_Click);
			// 
			// sfd
			// 
			this.sfd.FileName = "archive.lja";
			this.sfd.Filter = "ljArchive Files (*.lja)|*.lja|All files (*.*)|*.*";
			this.sfd.Title = "Save Journal File";
			// 
			// NewJournalArchiveIntroPage
			// 
			this.Controls.Add(this.btnPathDialog);
			this.Controls.Add(this.txtPath);
			this.Controls.Add(this.lblIntro);
			this.Name = "NewJournalArchiveIntroPage";
			this.Enter += new System.EventHandler(this.NewJournalArchiveIntroPage_Enter);
			this.Controls.SetChildIndex(this.lblIntro, 0);
			this.Controls.SetChildIndex(this.txtPath, 0);
			this.Controls.SetChildIndex(this.btnPathDialog, 0);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnPathDialog_Click(object sender, System.EventArgs e)
		{
			if (sfd.ShowDialog() == DialogResult.OK)
				txtPath.Text = sfd.FileName;
		}

		private void txtPath_TextChanged(object sender, System.EventArgs e)
		{
			ValidatePath();
		}

		private void NewJournalArchiveIntroPage_Enter(object sender, System.EventArgs e)
		{
			ValidatePath();
		}

		private void ValidatePath()
		{
			System.IO.FileInfo fi = null;
			System.IO.DirectoryInfo di = null;
			if (this.WizardSheet == null)
				return;
			try
			{
				fi = new System.IO.FileInfo(txtPath.Text);
				di = new System.IO.DirectoryInfo(txtPath.Text);
			}
			catch {}
			this.WizardSheet.EnableNextButton = (fi != null && di != null && !di.Exists);
		}

		public string Path
		{
			get
			{
				return txtPath.Text;
			}
		}
	}
}

