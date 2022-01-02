using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace EF.ljArchive.WindowsForms.Dialogs
{
	/// <summary>
	/// Summary description for ExpectedError.
	/// </summary>
	public class ExpectedError : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.Button btnCopyToClipboard;
		private EF.ljArchive.WindowsForms.Controls.LineControl lineControl1;
		private System.Windows.Forms.LinkLabel llMoreInfo;
		private System.Windows.Forms.Label lblErrorInfo;
		private System.Windows.Forms.PictureBox pbIcon;
		private System.Windows.Forms.Panel pnlTop;
		private System.Windows.Forms.Panel pnlBottom;
		private System.Windows.Forms.TextBox txtException;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.LinkLabel llEmail;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region Public Instance Constructors
		public ExpectedError()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			InitializeDialog();
		}

		public ExpectedError(ExpectedErrorCategories expectedErrorCategory, Exception exception) : this()
		{
			lblErrorInfo.Text = Localizer.GetString(this.GetType(), "ExpectedErrorCategories." +
				expectedErrorCategory.ToString());
			txtException.Text = exception.ToString();
		}
		#endregion

		public static void Go(ExpectedErrorCategories expectedErrorCategory, Exception exception, Form owner)
		{
			ExpectedError ee = new ExpectedError(expectedErrorCategory, exception);
			ee.ShowDialog(owner);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ExpectedError));
			this.btnCopyToClipboard = new System.Windows.Forms.Button();
			this.pbIcon = new System.Windows.Forms.PictureBox();
			this.pnlTop = new System.Windows.Forms.Panel();
			this.btnOK = new System.Windows.Forms.Button();
			this.lblErrorInfo = new System.Windows.Forms.Label();
			this.llMoreInfo = new System.Windows.Forms.LinkLabel();
			this.pnlBottom = new System.Windows.Forms.Panel();
			this.txtException = new System.Windows.Forms.TextBox();
			this.llEmail = new System.Windows.Forms.LinkLabel();
			this.lineControl1 = new EF.ljArchive.WindowsForms.Controls.LineControl();
			this.pnlTop.SuspendLayout();
			this.pnlBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCopyToClipboard
			// 
			this.btnCopyToClipboard.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnCopyToClipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCopyToClipboard.Location = new System.Drawing.Point(0, -23);
			this.btnCopyToClipboard.Name = "btnCopyToClipboard";
			this.btnCopyToClipboard.Size = new System.Drawing.Size(274, 23);
			this.btnCopyToClipboard.TabIndex = 12;
			this.btnCopyToClipboard.Text = "Copy Error to Clipboard";
			this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
			// 
			// pbIcon
			// 
			this.pbIcon.Image = ((System.Drawing.Image)(resources.GetObject("pbIcon.Image")));
			this.pbIcon.Location = new System.Drawing.Point(8, 8);
			this.pbIcon.Name = "pbIcon";
			this.pbIcon.Size = new System.Drawing.Size(48, 48);
			this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbIcon.TabIndex = 14;
			this.pbIcon.TabStop = false;
			// 
			// pnlTop
			// 
			this.pnlTop.BackColor = System.Drawing.Color.White;
			this.pnlTop.Controls.Add(this.btnOK);
			this.pnlTop.Controls.Add(this.lblErrorInfo);
			this.pnlTop.Controls.Add(this.llMoreInfo);
			this.pnlTop.Controls.Add(this.pbIcon);
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTop.Location = new System.Drawing.Point(0, 0);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Size = new System.Drawing.Size(274, 144);
			this.pnlTop.TabIndex = 15;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOK.Location = new System.Drawing.Point(100, 112);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 17;
			this.btnOK.Text = "O&K";
			// 
			// lblErrorInfo
			// 
			this.lblErrorInfo.Location = new System.Drawing.Point(64, 8);
			this.lblErrorInfo.Name = "lblErrorInfo";
			this.lblErrorInfo.Size = new System.Drawing.Size(208, 96);
			this.lblErrorInfo.TabIndex = 16;
			// 
			// llMoreInfo
			// 
			this.llMoreInfo.Location = new System.Drawing.Point(184, 112);
			this.llMoreInfo.Name = "llMoreInfo";
			this.llMoreInfo.Size = new System.Drawing.Size(80, 23);
			this.llMoreInfo.TabIndex = 15;
			this.llMoreInfo.TabStop = true;
			this.llMoreInfo.Text = "More Info >";
			this.llMoreInfo.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			this.llMoreInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llMoreInfo_LinkClicked);
			// 
			// pnlBottom
			// 
			this.pnlBottom.Controls.Add(this.txtException);
			this.pnlBottom.Controls.Add(this.llEmail);
			this.pnlBottom.Controls.Add(this.lineControl1);
			this.pnlBottom.Controls.Add(this.btnCopyToClipboard);
			this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlBottom.Location = new System.Drawing.Point(0, 144);
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Size = new System.Drawing.Size(274, 0);
			this.pnlBottom.TabIndex = 16;
			// 
			// txtException
			// 
			this.txtException.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtException.Location = new System.Drawing.Point(0, 48);
			this.txtException.Multiline = true;
			this.txtException.Name = "txtException";
			this.txtException.ReadOnly = true;
			this.txtException.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtException.Size = new System.Drawing.Size(274, 0);
			this.txtException.TabIndex = 14;
			this.txtException.Text = "";
			// 
			// llEmail
			// 
			this.llEmail.Dock = System.Windows.Forms.DockStyle.Top;
			this.llEmail.LinkArea = new System.Windows.Forms.LinkArea(36, 18);
			this.llEmail.Location = new System.Drawing.Point(0, 4);
			this.llEmail.Name = "llEmail";
			this.llEmail.Size = new System.Drawing.Size(274, 44);
			this.llEmail.TabIndex = 15;
			this.llEmail.TabStop = true;
			this.llEmail.Text = "Have you found a bug?  If so, email ljarchive-support@lists.sourceforge.net" +
				" and supply the following info:";
			this.llEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llEmail_LinkClicked);
			// 
			// lineControl1
			// 
			this.lineControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.lineControl1.Location = new System.Drawing.Point(0, 0);
			this.lineControl1.Name = "lineControl1";
			this.lineControl1.Size = new System.Drawing.Size(274, 4);
			this.lineControl1.TabIndex = 13;
			this.lineControl1.Text = "lineControl1";
			// 
			// ExpectedError
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnOK;
			this.ClientSize = new System.Drawing.Size(274, 143);
			this.Controls.Add(this.pnlBottom);
			this.Controls.Add(this.pnlTop);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExpectedError";
			this.ShowInTaskbar = false;
			this.Text = "Info";
			this.pnlTop.ResumeLayout(false);
			this.pnlBottom.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Private Instance Methods
		private void InitializeDialog()
		{
			int pos;
			Localizer.SetControlText(this.GetType(), this, llEmail, btnCopyToClipboard, llMoreInfo, btnOK);
			pos = llEmail.Text.IndexOf("{0}");
			llEmail.Text = string.Format(llEmail.Text, email);
			llEmail.LinkArea = new LinkArea(pos, email.Length);
		}

		private void btnCopyToClipboard_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject("ljArchive Error: " + txtException.Text, true);
		}

		private void llMoreInfo_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (dropped)
				this.Height -= dropHeight;
			else
				this.Height += dropHeight;
			dropped = !dropped;
		}

		private void llEmail_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("mailto:" + email);
		}
		#endregion

		#region Private Instance Fields
		private bool dropped = false;
		private const int dropHeight = 200;
		#endregion

		public enum ExpectedErrorCategories
		{
			BadFileFormat,
			FileNotFound,
			SyncInvalidPassword,
			SyncRepeatedRequests,
			SyncCommunityAccessDenied,
			SyncNoEncodingSettings,
			SyncXMLRPCNotSupported,
			SyncExportCommentsNotSupported,
			SyncServerNotResponding,
			SyncCancel
		}

		private const string email = "ljarchive-support@lists.sourceforge.net";
	}
}
