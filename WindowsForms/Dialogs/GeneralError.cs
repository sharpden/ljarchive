using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace EF.ljArchive.WindowsForms.Dialogs
{
	/// <summary>
	/// Summary description for GeneralError.
	/// </summary>
	public class GeneralError : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.PictureBox imgFatalError;
		internal System.Windows.Forms.TextBox txtDescription;
		internal System.Windows.Forms.Button btnCopyToClipboard;
		internal System.Windows.Forms.Button btnClose;
		internal System.Windows.Forms.Label lblFoundTheFollowingError;
		private EF.ljArchive.WindowsForms.Controls.LineControl lineControl1;
		private System.Windows.Forms.Label lblTitle;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GeneralError()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			Localizer.SetControlText(this.GetType(), this, lblTitle, lblFoundTheFollowingError, btnCopyToClipboard,
				btnClose);
		}

		public GeneralError(string errorDescription) : this()
		{
			txtDescription.Text = errorDescription;
			txtDescription.SelectionLength = 0;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GeneralError));
			this.imgFatalError = new System.Windows.Forms.PictureBox();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnCopyToClipboard = new System.Windows.Forms.Button();
			this.lblFoundTheFollowingError = new System.Windows.Forms.Label();
			this.lineControl1 = new EF.ljArchive.WindowsForms.Controls.LineControl();
			this.lblTitle = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// imgFatalError
			// 
			this.imgFatalError.Dock = System.Windows.Forms.DockStyle.Top;
			this.imgFatalError.Image = ((System.Drawing.Image)(resources.GetObject("imgFatalError.Image")));
			this.imgFatalError.Location = new System.Drawing.Point(0, 0);
			this.imgFatalError.Name = "imgFatalError";
			this.imgFatalError.Size = new System.Drawing.Size(440, 50);
			this.imgFatalError.TabIndex = 4;
			this.imgFatalError.TabStop = false;
			// 
			// txtDescription
			// 
			this.txtDescription.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtDescription.Location = new System.Drawing.Point(0, 68);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.ReadOnly = true;
			this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtDescription.Size = new System.Drawing.Size(440, 132);
			this.txtDescription.TabIndex = 10;
			this.txtDescription.Text = "";
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClose.Location = new System.Drawing.Point(237, 216);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(160, 23);
			this.btnClose.TabIndex = 13;
			this.btnClose.Text = "Continue";
			// 
			// btnCopyToClipboard
			// 
			this.btnCopyToClipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCopyToClipboard.Location = new System.Drawing.Point(48, 216);
			this.btnCopyToClipboard.Name = "btnCopyToClipboard";
			this.btnCopyToClipboard.Size = new System.Drawing.Size(160, 23);
			this.btnCopyToClipboard.TabIndex = 12;
			this.btnCopyToClipboard.Text = "Copy Error to Clipboard";
			this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
			// 
			// lblFoundTheFollowingError
			// 
			this.lblFoundTheFollowingError.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblFoundTheFollowingError.Location = new System.Drawing.Point(0, 52);
			this.lblFoundTheFollowingError.Name = "lblFoundTheFollowingError";
			this.lblFoundTheFollowingError.Size = new System.Drawing.Size(440, 16);
			this.lblFoundTheFollowingError.TabIndex = 14;
			this.lblFoundTheFollowingError.Text = "ljArchive found the following error while trying to process your request:";
			this.lblFoundTheFollowingError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lineControl1
			// 
			this.lineControl1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(241)), ((System.Byte)(243)), ((System.Byte)(208)));
			this.lineControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.lineControl1.Location = new System.Drawing.Point(0, 50);
			this.lineControl1.Name = "lineControl1";
			this.lineControl1.Size = new System.Drawing.Size(440, 2);
			this.lineControl1.TabIndex = 16;
			this.lineControl1.Text = "lineControl1";
			// 
			// lblTitle
			// 
			this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(241)), ((System.Byte)(243)), ((System.Byte)(208)));
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(68)), ((System.Byte)(112)), ((System.Byte)(186)));
			this.lblTitle.Location = new System.Drawing.Point(96, 8);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(320, 32);
			this.lblTitle.TabIndex = 17;
			this.lblTitle.Text = "General Error";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// GeneralError
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(440, 245);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnCopyToClipboard);
			this.Controls.Add(this.lblFoundTheFollowingError);
			this.Controls.Add(this.lineControl1);
			this.Controls.Add(this.imgFatalError);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GeneralError";
			this.ShowInTaskbar = false;
			this.Text = "Error";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCopyToClipboard_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject("ljArchive Error: " + txtDescription.Text, true);
		}

		public static void Go(string errorDescription, Form owner)
		{
			GeneralError ge = new GeneralError(errorDescription);
			ge.ShowDialog(owner);
		}
	}
}
