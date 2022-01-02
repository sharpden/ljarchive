using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EF.ljArchive.WindowsForms.Controls
{
	public class NewJournalArchiveFinish : Genghis.Windows.Forms.WizardPage
	{
		private System.Windows.Forms.Label lblFinish;
		private System.ComponentModel.IContainer components = null;

		public NewJournalArchiveFinish(string title, string description) : base (title, description)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			Localizer.SetControlText(this.GetType(), lblFinish);
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
			this.lblFinish = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblFinish
			// 
			this.lblFinish.Location = new System.Drawing.Point(8, 100);
			this.lblFinish.Name = "lblFinish";
			this.lblFinish.Size = new System.Drawing.Size(384, 124);
			this.lblFinish.TabIndex = 2;
			this.lblFinish.Text = "Thank you.\n\nOnce you click \'Finish\', ljArchive will create your journal archive, " +
				"connect to the LiveJournal server, and download your entries.\n\nRemember, this co" +
				"uld take a while!";
			// 
			// NewJournalArchiveFinish
			// 
			this.Controls.Add(this.lblFinish);
			this.Name = "NewJournalArchiveFinish";
			this.Enter += new System.EventHandler(this.NewJournalArchiveFinish_Enter);
			this.Leave += new System.EventHandler(this.NewJournalArchiveFinish_Leave);
			this.Controls.SetChildIndex(this.lblFinish, 0);
			this.ResumeLayout(false);

		}
		#endregion

		private void NewJournalArchiveFinish_Enter(object sender, System.EventArgs e)
		{
			NewJournalArchiveAccountSettings njas = (NewJournalArchiveAccountSettings) this.WizardSheet.Pages[1];
			newJournal = new Engine.Journal(njas.ServerURL, njas.UserName, njas.Password, njas.GetComments, njas.UseJournal);
			this.WizardSheet.EnableFinishButton = true;
		}

		private void NewJournalArchiveFinish_Leave(object sender, System.EventArgs e)
		{
			this.WizardSheet.EnableFinishButton = false;
		}

		private Engine.Journal newJournal;

		public Engine.Journal NewJournal
		{
			get
			{
				return newJournal;
			}
		}
	}
}

