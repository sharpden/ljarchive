using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace EF.ljArchive.Plugins.WindowsForms
{
	/// <summary>
	/// Summary description for RIDBaseline.
	/// </summary>
	public class RIDBaseline : System.Windows.Forms.Form
	{
		private System.Windows.Forms.NumericUpDown numAge;
		private System.Windows.Forms.Label lblGender;
		private System.Windows.Forms.ComboBox cmbGender;
		private System.Windows.Forms.Label lblInitialSetup;
		private System.Windows.Forms.Label lblGrovelling;
		private System.Windows.Forms.Label lblAge;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnSkip;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RIDBaseline()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			cmbGender.SelectedIndex = 0;
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
			this.numAge = new System.Windows.Forms.NumericUpDown();
			this.lblGender = new System.Windows.Forms.Label();
			this.cmbGender = new System.Windows.Forms.ComboBox();
			this.lblInitialSetup = new System.Windows.Forms.Label();
			this.lblGrovelling = new System.Windows.Forms.Label();
			this.lblAge = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnSkip = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numAge)).BeginInit();
			this.SuspendLayout();
			// 
			// numAge
			// 
			this.numAge.Location = new System.Drawing.Point(120, 184);
			this.numAge.Maximum = new System.Decimal(new int[] {
																   120,
																   0,
																   0,
																   0});
			this.numAge.Name = "numAge";
			this.numAge.TabIndex = 1;
			// 
			// lblGender
			// 
			this.lblGender.Location = new System.Drawing.Point(16, 152);
			this.lblGender.Name = "lblGender";
			this.lblGender.Size = new System.Drawing.Size(56, 16);
			this.lblGender.TabIndex = 2;
			this.lblGender.Text = "Gender:";
			// 
			// cmbGender
			// 
			this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbGender.Items.AddRange(new object[] {
														   "Female",
														   "Male"});
			this.cmbGender.Location = new System.Drawing.Point(120, 152);
			this.cmbGender.Name = "cmbGender";
			this.cmbGender.Size = new System.Drawing.Size(121, 21);
			this.cmbGender.TabIndex = 3;
			// 
			// lblInitialSetup
			// 
			this.lblInitialSetup.BackColor = System.Drawing.Color.White;
			this.lblInitialSetup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblInitialSetup.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblInitialSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblInitialSetup.Location = new System.Drawing.Point(0, 0);
			this.lblInitialSetup.Name = "lblInitialSetup";
			this.lblInitialSetup.Size = new System.Drawing.Size(292, 32);
			this.lblInitialSetup.TabIndex = 0;
			this.lblInitialSetup.Text = "Initial Setup";
			// 
			// lblGrovelling
			// 
			this.lblGrovelling.Location = new System.Drawing.Point(0, 40);
			this.lblGrovelling.Name = "lblGrovelling";
			this.lblGrovelling.Size = new System.Drawing.Size(292, 96);
			this.lblGrovelling.TabIndex = 4;
			this.lblGrovelling.Text = "Entering the following data will allow this plugin to customize its analysis into" +
				" three separate categories: your results vs. the rest of LiveJournal, vs. your g" +
				"ender, and vs. your age group.\r\n\r\nThe data is anonymous and cannot be tied to yo" +
				"ur account.";
			// 
			// lblAge
			// 
			this.lblAge.Location = new System.Drawing.Point(16, 184);
			this.lblAge.Name = "lblAge";
			this.lblAge.Size = new System.Drawing.Size(56, 16);
			this.lblAge.TabIndex = 5;
			this.lblAge.Text = "Age:";
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(112, 240);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "OK";
			// 
			// btnSkip
			// 
			this.btnSkip.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnSkip.Location = new System.Drawing.Point(200, 240);
			this.btnSkip.Name = "btnSkip";
			this.btnSkip.TabIndex = 7;
			this.btnSkip.Text = "&Skip";
			// 
			// RIDBaseline
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.btnSkip);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lblAge);
			this.Controls.Add(this.lblGrovelling);
			this.Controls.Add(this.cmbGender);
			this.Controls.Add(this.lblGender);
			this.Controls.Add(this.numAge);
			this.Controls.Add(this.lblInitialSetup);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RIDBaseline";
			this.Text = "Baseline Information";
			((System.ComponentModel.ISupportInitialize)(this.numAge)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		public int Age
		{
			get { return (int) numAge.Value; }
		}

		public char Gender
		{
			get { return (cmbGender.SelectedIndex == 0 ? 'f' : 'm'); }
		}
	}
}
