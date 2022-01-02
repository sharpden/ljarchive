using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace EF.ljArchive.WindowsForms.Dialogs
{
	/// <summary>
	/// Summary description for Exporter.
	/// </summary>
	public class Export : System.Windows.Forms.Form
	{
		private System.Windows.Forms.SaveFileDialog sfd;
		private System.Windows.Forms.GroupBox gbExportSettings;
		private System.Windows.Forms.PropertyGrid pg;
		private System.Windows.Forms.GroupBox gbFileSettings;
		private System.Windows.Forms.Button btnPathDialog;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.Panel pnlButtons;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.GroupBox gbExporterInfo;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Label lblAuthor;
		private System.Windows.Forms.LinkLabel llURL;
		private System.Windows.Forms.ComboBox cmbSplitExport;
		private System.Windows.Forms.Label lblSplitExport;
		private System.Windows.Forms.Label lblFileName;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Export(Common.IJournalWriter ijw)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.ijw = ijw;
			InitializeForm();
		}

		static public void Go(Engine.Journal j, Common.IJournalWriter ijw)
		{
			Export ed = new Export(ijw);
			if (ed.ShowDialog() == DialogResult.OK)
			{
				using (Busy b = new Busy())
					Engine.Exporter.Export(ed.SplitExport, ed.FileName, j, ijw);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Export));
			this.sfd = new System.Windows.Forms.SaveFileDialog();
			this.gbExportSettings = new System.Windows.Forms.GroupBox();
			this.pg = new System.Windows.Forms.PropertyGrid();
			this.gbFileSettings = new System.Windows.Forms.GroupBox();
			this.lblSplitExport = new System.Windows.Forms.Label();
			this.btnPathDialog = new System.Windows.Forms.Button();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.lblFileName = new System.Windows.Forms.Label();
			this.cmbSplitExport = new System.Windows.Forms.ComboBox();
			this.pnlButtons = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.gbExporterInfo = new System.Windows.Forms.GroupBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.llURL = new System.Windows.Forms.LinkLabel();
			this.lblAuthor = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.gbExportSettings.SuspendLayout();
			this.gbFileSettings.SuspendLayout();
			this.pnlButtons.SuspendLayout();
			this.gbExporterInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbExportSettings
			// 
			this.gbExportSettings.Controls.Add(this.pg);
			this.gbExportSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbExportSettings.Location = new System.Drawing.Point(0, 188);
			this.gbExportSettings.Name = "gbExportSettings";
			this.gbExportSettings.Size = new System.Drawing.Size(416, 225);
			this.gbExportSettings.TabIndex = 7;
			this.gbExportSettings.TabStop = false;
			this.gbExportSettings.Text = "Export Settings";
			// 
			// pg
			// 
			this.pg.CommandsVisibleIfAvailable = true;
			this.pg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pg.LargeButtons = false;
			this.pg.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.pg.Location = new System.Drawing.Point(3, 16);
			this.pg.Name = "pg";
			this.pg.Size = new System.Drawing.Size(410, 206);
			this.pg.TabIndex = 0;
			this.pg.Text = "PropertyGrid";
			this.pg.ViewBackColor = System.Drawing.SystemColors.Window;
			this.pg.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// gbFileSettings
			// 
			this.gbFileSettings.Controls.Add(this.lblSplitExport);
			this.gbFileSettings.Controls.Add(this.btnPathDialog);
			this.gbFileSettings.Controls.Add(this.txtPath);
			this.gbFileSettings.Controls.Add(this.lblFileName);
			this.gbFileSettings.Controls.Add(this.cmbSplitExport);
			this.gbFileSettings.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbFileSettings.Location = new System.Drawing.Point(0, 0);
			this.gbFileSettings.Name = "gbFileSettings";
			this.gbFileSettings.Size = new System.Drawing.Size(416, 88);
			this.gbFileSettings.TabIndex = 10;
			this.gbFileSettings.TabStop = false;
			this.gbFileSettings.Text = "File Settings";
			// 
			// lblSplitExport
			// 
			this.lblSplitExport.Location = new System.Drawing.Point(8, 58);
			this.lblSplitExport.Name = "lblSplitExport";
			this.lblSplitExport.Size = new System.Drawing.Size(88, 16);
			this.lblSplitExport.TabIndex = 14;
			this.lblSplitExport.Text = "Split Export:";
			// 
			// btnPathDialog
			// 
			this.btnPathDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPathDialog.Location = new System.Drawing.Point(368, 24);
			this.btnPathDialog.Name = "btnPathDialog";
			this.btnPathDialog.Size = new System.Drawing.Size(40, 20);
			this.btnPathDialog.TabIndex = 12;
			this.btnPathDialog.Text = "...";
			this.btnPathDialog.Click += new System.EventHandler(this.btnPathDialog_Click);
			// 
			// txtPath
			// 
			this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtPath.Location = new System.Drawing.Point(104, 24);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(256, 20);
			this.txtPath.TabIndex = 11;
			this.txtPath.Text = "";
			this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
			// 
			// lblFileName
			// 
			this.lblFileName.Location = new System.Drawing.Point(8, 26);
			this.lblFileName.Name = "lblFileName";
			this.lblFileName.Size = new System.Drawing.Size(88, 16);
			this.lblFileName.TabIndex = 13;
			this.lblFileName.Text = "File Name:";
			// 
			// cmbSplitExport
			// 
			this.cmbSplitExport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSplitExport.Location = new System.Drawing.Point(104, 56);
			this.cmbSplitExport.Name = "cmbSplitExport";
			this.cmbSplitExport.Size = new System.Drawing.Size(121, 21);
			this.cmbSplitExport.TabIndex = 10;
			// 
			// pnlButtons
			// 
			this.pnlButtons.BackColor = System.Drawing.Color.Transparent;
			this.pnlButtons.Controls.Add(this.btnCancel);
			this.pnlButtons.Controls.Add(this.btnOk);
			this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlButtons.Location = new System.Drawing.Point(0, 413);
			this.pnlButtons.Name = "pnlButtons";
			this.pnlButtons.Size = new System.Drawing.Size(416, 32);
			this.pnlButtons.TabIndex = 13;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(323, 5);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 14;
			this.btnCancel.Text = "&Cancel";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Enabled = false;
			this.btnOk.Location = new System.Drawing.Point(235, 5);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 13;
			this.btnOk.Text = "O&k";
			// 
			// gbExporterInfo
			// 
			this.gbExporterInfo.Controls.Add(this.lblDescription);
			this.gbExporterInfo.Controls.Add(this.llURL);
			this.gbExporterInfo.Controls.Add(this.lblAuthor);
			this.gbExporterInfo.Controls.Add(this.lblName);
			this.gbExporterInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbExporterInfo.Location = new System.Drawing.Point(0, 88);
			this.gbExporterInfo.Name = "gbExporterInfo";
			this.gbExporterInfo.Size = new System.Drawing.Size(416, 100);
			this.gbExporterInfo.TabIndex = 14;
			this.gbExporterInfo.TabStop = false;
			this.gbExporterInfo.Text = "Exporter Info";
			// 
			// lblDescription
			// 
			this.lblDescription.Location = new System.Drawing.Point(3, 40);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(410, 56);
			this.lblDescription.TabIndex = 1;
			// 
			// llURL
			// 
			this.llURL.AutoSize = true;
			this.llURL.Location = new System.Drawing.Point(24, 16);
			this.llURL.Name = "llURL";
			this.llURL.Size = new System.Drawing.Size(0, 16);
			this.llURL.TabIndex = 3;
			this.llURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llURL_LinkClicked);
			// 
			// lblAuthor
			// 
			this.lblAuthor.AutoSize = true;
			this.lblAuthor.Location = new System.Drawing.Point(16, 16);
			this.lblAuthor.Name = "lblAuthor";
			this.lblAuthor.Size = new System.Drawing.Size(0, 16);
			this.lblAuthor.TabIndex = 2;
			this.lblAuthor.SizeChanged += new System.EventHandler(this.lblAuthor_SizeChanged);
			// 
			// lblName
			// 
			this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblName.AutoSize = true;
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblName.Location = new System.Drawing.Point(8, 16);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(0, 16);
			this.lblName.TabIndex = 0;
			this.lblName.SizeChanged += new System.EventHandler(this.lblName_SizeChanged);
			// 
			// Export
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(416, 445);
			this.Controls.Add(this.gbExportSettings);
			this.Controls.Add(this.gbExporterInfo);
			this.Controls.Add(this.pnlButtons);
			this.Controls.Add(this.gbFileSettings);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(424, 378);
			this.Name = "Export";
			this.ShowInTaskbar = false;
			this.Text = "Export File";
			this.gbExportSettings.ResumeLayout(false);
			this.gbFileSettings.ResumeLayout(false);
			this.pnlButtons.ResumeLayout(false);
			this.gbExporterInfo.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		private Common.IJournalWriter ijw;

		protected void InitializeForm()
		{
			// Localizer
			Localizer.SetControlText(this.GetType(), this, gbFileSettings, lblFileName, lblSplitExport,
				gbExporterInfo, gbExportSettings, btnOk, btnCancel, lblAuthor);
			cmbSplitExport.Items.AddRange(Localizer.GetString(this.GetType(), "cmbSplitExport.Items").Split('|'));

			// cmbSplitExport
			cmbSplitExport.SelectedIndex = 0;

			// pg
			pg.SelectedObject = ijw.Settings;

			// llURL
			llURL.Text = ijw.URL;

			// lblDescription
			lblDescription.Text = ijw.Description;

			// lblAuthor
			lblAuthor.Text = string.Format(lblAuthor.Text, ijw.Author);

			// lblName
			lblName.Text = ijw.Name;

			// this
			this.Text = string.Format(this.Text, ijw.Name);

			// sfd
			sfd.Filter = ijw.Filter;
		}

		private void btnPathDialog_Click(object sender, System.EventArgs e)
		{
			if (sfd.ShowDialog() == DialogResult.OK)
				txtPath.Text = sfd.FileName;
		}

		private void lblName_SizeChanged(object sender, System.EventArgs e)
		{
			lblAuthor.Left = lblName.Right + 5;
			llURL.Left = lblAuthor.Right + 5;
		}

		private void lblAuthor_SizeChanged(object sender, System.EventArgs e)
		{
			lblAuthor.Left = lblName.Right + 5;
			llURL.Left = lblAuthor.Right + 5;
		}

		private void llURL_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(llURL.Text);
		}

		private void txtPath_TextChanged(object sender, System.EventArgs e)
		{
			btnOk.Enabled = txtPath.Text.Length > 0;
		}

		public string FileName
		{
			get
			{
				return txtPath.Text;
			}
			set
			{
				sfd.FileName = value;
				txtPath.Text = value;
			}
		}

		public Engine.SplitExport SplitExport
		{
			get
			{
				return (Engine.SplitExport) cmbSplitExport.SelectedIndex;
			}
		}
	}
}
