using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace EF.ljArchive.WindowsForms.Dialogs
{
	/// <summary>
	/// Summary description for Options.
	/// </summary>
	public class Options : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox grpGridOrientation;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.RadioButton rbAlignLeft;
		private System.Windows.Forms.PictureBox pbAlignTop;
		private System.Windows.Forms.PictureBox pbAlignLeft;
		private System.Windows.Forms.RadioButton rbAlignTop;
		private System.Windows.Forms.Label lblAlign;
		private System.Windows.Forms.GroupBox grpMiscellaneous;
		private System.Windows.Forms.CheckBox chkSyncOnStartup;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Options()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			Localizer.SetControlText(this.GetType(), grpGridOrientation, lblAlign, rbAlignLeft, rbAlignTop,
				grpMiscellaneous, chkSyncOnStartup, btnOK,
				btnCancel);
		}

		static public void Go(ref DockStyle dock, ref bool syncOnStartup)
		{
			Options o = new Options();
			if (dock == DockStyle.Top)
				o.rbAlignTop.Checked = true;
			else
				o.rbAlignLeft.Checked = true;
			o.chkSyncOnStartup.Checked = syncOnStartup;
			if (o.ShowDialog() == DialogResult.OK)
			{
				if (o.rbAlignTop.Checked)
					dock = DockStyle.Top;
				else
					dock = DockStyle.Left;
				syncOnStartup = o.chkSyncOnStartup.Checked;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Options));
			this.grpGridOrientation = new System.Windows.Forms.GroupBox();
			this.lblAlign = new System.Windows.Forms.Label();
			this.rbAlignTop = new System.Windows.Forms.RadioButton();
			this.pbAlignLeft = new System.Windows.Forms.PictureBox();
			this.pbAlignTop = new System.Windows.Forms.PictureBox();
			this.rbAlignLeft = new System.Windows.Forms.RadioButton();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.grpMiscellaneous = new System.Windows.Forms.GroupBox();
			this.chkSyncOnStartup = new System.Windows.Forms.CheckBox();
			this.grpGridOrientation.SuspendLayout();
			this.grpMiscellaneous.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpGridOrientation
			// 
			this.grpGridOrientation.Controls.Add(this.lblAlign);
			this.grpGridOrientation.Controls.Add(this.rbAlignTop);
			this.grpGridOrientation.Controls.Add(this.pbAlignLeft);
			this.grpGridOrientation.Controls.Add(this.pbAlignTop);
			this.grpGridOrientation.Controls.Add(this.rbAlignLeft);
			this.grpGridOrientation.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpGridOrientation.Location = new System.Drawing.Point(0, 5);
			this.grpGridOrientation.Name = "grpGridOrientation";
			this.grpGridOrientation.Size = new System.Drawing.Size(346, 107);
			this.grpGridOrientation.TabIndex = 0;
			this.grpGridOrientation.TabStop = false;
			this.grpGridOrientation.Text = "Grid Orientation";
			// 
			// lblAlign
			// 
			this.lblAlign.Location = new System.Drawing.Point(16, 16);
			this.lblAlign.Name = "lblAlign";
			this.lblAlign.Size = new System.Drawing.Size(320, 32);
			this.lblAlign.TabIndex = 4;
			this.lblAlign.Text = "How would you like to orient the events/comments grid?";
			// 
			// rbAlignTop
			// 
			this.rbAlignTop.Location = new System.Drawing.Point(232, 56);
			this.rbAlignTop.Name = "rbAlignTop";
			this.rbAlignTop.Size = new System.Drawing.Size(104, 40);
			this.rbAlignTop.TabIndex = 3;
			this.rbAlignTop.Text = "Align top";
			// 
			// pbAlignLeft
			// 
			this.pbAlignLeft.Image = ((System.Drawing.Image)(resources.GetObject("pbAlignLeft.Image")));
			this.pbAlignLeft.Location = new System.Drawing.Point(16, 56);
			this.pbAlignLeft.Name = "pbAlignLeft";
			this.pbAlignLeft.Size = new System.Drawing.Size(40, 40);
			this.pbAlignLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbAlignLeft.TabIndex = 2;
			this.pbAlignLeft.TabStop = false;
			// 
			// pbAlignTop
			// 
			this.pbAlignTop.Image = ((System.Drawing.Image)(resources.GetObject("pbAlignTop.Image")));
			this.pbAlignTop.Location = new System.Drawing.Point(184, 56);
			this.pbAlignTop.Name = "pbAlignTop";
			this.pbAlignTop.Size = new System.Drawing.Size(40, 40);
			this.pbAlignTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbAlignTop.TabIndex = 1;
			this.pbAlignTop.TabStop = false;
			// 
			// rbAlignLeft
			// 
			this.rbAlignLeft.Location = new System.Drawing.Point(64, 56);
			this.rbAlignLeft.Name = "rbAlignLeft";
			this.rbAlignLeft.Size = new System.Drawing.Size(104, 40);
			this.rbAlignLeft.TabIndex = 0;
			this.rbAlignLeft.Text = "Align left";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(264, 240);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(176, 240);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "O&K";
			// 
			// grpMiscellaneous
			// 
			this.grpMiscellaneous.Controls.Add(this.chkSyncOnStartup);
			this.grpMiscellaneous.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpMiscellaneous.Location = new System.Drawing.Point(0, 112);
			this.grpMiscellaneous.Name = "grpMiscellaneous";
			this.grpMiscellaneous.Size = new System.Drawing.Size(346, 120);
			this.grpMiscellaneous.TabIndex = 4;
			this.grpMiscellaneous.TabStop = false;
			this.grpMiscellaneous.Text = "Miscellaneous";
			// 
			// chkSyncOnStartup
			// 
			this.chkSyncOnStartup.Location = new System.Drawing.Point(16, 52);
			this.chkSyncOnStartup.Name = "chkSyncOnStartup";
			this.chkSyncOnStartup.Size = new System.Drawing.Size(320, 24);
			this.chkSyncOnStartup.TabIndex = 1;
			this.chkSyncOnStartup.Text = "Sync on startup";
			// 
			// Options
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(346, 271);
			this.Controls.Add(this.grpMiscellaneous);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.grpGridOrientation);
			this.DockPadding.Top = 5;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Options";
			this.Text = "Options";
			this.grpGridOrientation.ResumeLayout(false);
			this.grpMiscellaneous.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
