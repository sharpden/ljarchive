using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using EF.ljArchive.Common;

namespace EF.ljArchive.Plugins.WindowsForms
{
	/// <summary>
	/// Summary description for PostFrequency.
	/// </summary>
	public class PostFrequency : System.Windows.Forms.Form, IPlugin
	{
		private System.Windows.Forms.Panel pnlControls;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ComboBox cmbShowGradients;
		private System.Windows.Forms.Label lblShowGradients;
		private ZedGraph.ZedGraphControl graphControl;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PostFrequency()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PostFrequency));
			this.pnlControls = new System.Windows.Forms.Panel();
			this.btnOK = new System.Windows.Forms.Button();
			this.cmbShowGradients = new System.Windows.Forms.ComboBox();
			this.lblShowGradients = new System.Windows.Forms.Label();
			this.graphControl = new ZedGraph.ZedGraphControl();
			this.pnlControls.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlControls
			// 
			this.pnlControls.Controls.Add(this.btnOK);
			this.pnlControls.Controls.Add(this.cmbShowGradients);
			this.pnlControls.Controls.Add(this.lblShowGradients);
			this.pnlControls.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlControls.Location = new System.Drawing.Point(0, 421);
			this.pnlControls.Name = "pnlControls";
			this.pnlControls.Size = new System.Drawing.Size(504, 40);
			this.pnlControls.TabIndex = 2;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(408, 8);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "&OK";
			// 
			// cmbShowGradients
			// 
			this.cmbShowGradients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbShowGradients.Items.AddRange(new object[] {
																  "Per Day",
																  "Per Month",
																  "Per Year"});
			this.cmbShowGradients.Location = new System.Drawing.Point(88, 8);
			this.cmbShowGradients.Name = "cmbShowGradients";
			this.cmbShowGradients.Size = new System.Drawing.Size(136, 21);
			this.cmbShowGradients.TabIndex = 1;
			this.cmbShowGradients.SelectedIndexChanged += new System.EventHandler(this.cmbShowGradients_SelectedIndexChanged);
			// 
			// lblShowGradients
			// 
			this.lblShowGradients.Location = new System.Drawing.Point(8, 12);
			this.lblShowGradients.Name = "lblShowGradients";
			this.lblShowGradients.Size = new System.Drawing.Size(88, 16);
			this.lblShowGradients.TabIndex = 0;
			this.lblShowGradients.Text = "Graph bars:";
			// 
			// graphControl
			// 
			this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.graphControl.Location = new System.Drawing.Point(0, 0);
			this.graphControl.Name = "graphControl";
			this.graphControl.Size = new System.Drawing.Size(504, 421);
			this.graphControl.TabIndex = 3;
			// 
			// PostFrequency
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 461);
			this.Controls.Add(this.graphControl);
			this.Controls.Add(this.pnlControls);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PostFrequency";
			this.ShowInTaskbar = false;
			this.Text = "Post Frequency Analyzer";
			this.pnlControls.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region IPlugin Members
		public Image MenuIcon
		{
			get { return new System.Drawing.Bitmap(this.GetType(), "res.PostFrequency.gif"); }
		}

		public string Description
		{
			get { return "Analyzes post frequency over the life of the journal."; }
		}

		public string Author
		{
			get { return "Erik Frey"; }
		}

		public void Go(Journal j)
		{
			this.j = j;
			cmbShowGradients.SelectedIndex = 1;
			ShowDialog();
		}

		public string Title
		{
			get { return "Post Frequency Analyzer"; }
		}

		public string URL
		{
			get { return "http://ljarchive.sourceforge.net/"; }
		}

		public Version Version
		{
			get { return new Version(0, 9, 3, 1); }
		}

		public object Settings
		{
			get { return null; }
			set { }
		}

		public int SelectedEventID
		{
			set { }
		}
		#endregion

		private void cmbShowGradients_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			graphControl.GraphPane = EF.ljArchive.Plugins.Core.PostFrequency.GetGraph(j, graphControl.Width - 1,
				graphControl.Height - 1, (EF.ljArchive.Plugins.Core.SplitPosts) cmbShowGradients.SelectedIndex);
			graphControl.Invalidate();
		}

		private Journal j;
	}
}
