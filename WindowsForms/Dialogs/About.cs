using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace EF.ljArchive.WindowsForms.Dialogs
{
	/// <summary>
	/// Summary description for About.
	/// </summary>
	public class About : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblAbout;
		private System.Windows.Forms.LinkLabel llURL;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.PictureBox pictureBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public About()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			Localizer.SetControlText(this.GetType(), this, lblAbout, btnOK);
			lblAbout.Text = string.Format(lblAbout.Text, this.GetType().Assembly.GetName().Version.ToString(3));
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(About));
			this.lblAbout = new System.Windows.Forms.Label();
			this.llURL = new System.Windows.Forms.LinkLabel();
			this.btnOK = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// lblAbout
			// 
			this.lblAbout.Location = new System.Drawing.Point(8, 16);
			this.lblAbout.Name = "lblAbout";
			this.lblAbout.Size = new System.Drawing.Size(192, 40);
			this.lblAbout.TabIndex = 0;
			// 
			// llURL
			// 
			this.llURL.AutoSize = true;
			this.llURL.Location = new System.Drawing.Point(8, 64);
			this.llURL.Name = "llURL";
			this.llURL.Size = new System.Drawing.Size(127, 16);
			this.llURL.TabIndex = 1;
			this.llURL.TabStop = true;
			this.llURL.Text = "https://github.com/sharpden/ljarchive";
			this.llURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llURL_LinkClicked);
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(168, 64);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "&OK";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(208, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			// 
			// About
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnOK;
			this.ClientSize = new System.Drawing.Size(250, 95);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.llURL);
			this.Controls.Add(this.lblAbout);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "About";
			this.Text = "About";
			this.ResumeLayout(false);

		}
		#endregion

		private void llURL_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(llURL.Text);
		}
	}
}
