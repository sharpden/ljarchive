using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using EF.ljArchive.Common;

namespace EF.ljArchive.Plugins.WindowsForms
{
	/// <summary>
	/// Summary description for CommentCount.
	/// </summary>
	public class CommentCount : System.Windows.Forms.Form, IPlugin
	{
		private System.Windows.Forms.Panel pnlControls;
		private System.Windows.Forms.Button btnOK;
		private ZedGraph.ZedGraphControl graphControl;
		private System.Windows.Forms.Label lblShowTop;
		private System.Windows.Forms.NumericUpDown numShowTop;
		private System.Windows.Forms.Label lblCommenters;
		private System.Windows.Forms.ToolTip toolTip;
		private System.ComponentModel.IContainer components;

		public CommentCount()
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CommentCount));
			this.pnlControls = new System.Windows.Forms.Panel();
			this.lblCommenters = new System.Windows.Forms.Label();
			this.lblShowTop = new System.Windows.Forms.Label();
			this.numShowTop = new System.Windows.Forms.NumericUpDown();
			this.btnOK = new System.Windows.Forms.Button();
			this.graphControl = new ZedGraph.ZedGraphControl();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.pnlControls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numShowTop)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlControls
			// 
			this.pnlControls.Controls.Add(this.lblCommenters);
			this.pnlControls.Controls.Add(this.lblShowTop);
			this.pnlControls.Controls.Add(this.numShowTop);
			this.pnlControls.Controls.Add(this.btnOK);
			this.pnlControls.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlControls.Location = new System.Drawing.Point(0, 421);
			this.pnlControls.Name = "pnlControls";
			this.pnlControls.Size = new System.Drawing.Size(504, 40);
			this.pnlControls.TabIndex = 2;
			// 
			// lblCommenters
			// 
			this.lblCommenters.Location = new System.Drawing.Point(152, 12);
			this.lblCommenters.Name = "lblCommenters";
			this.lblCommenters.Size = new System.Drawing.Size(72, 20);
			this.lblCommenters.TabIndex = 7;
			this.lblCommenters.Text = "commenters";
			// 
			// lblShowTop
			// 
			this.lblShowTop.Location = new System.Drawing.Point(8, 12);
			this.lblShowTop.Name = "lblShowTop";
			this.lblShowTop.Size = new System.Drawing.Size(72, 20);
			this.lblShowTop.TabIndex = 6;
			this.lblShowTop.Text = "Show top:";
			// 
			// numShowTop
			// 
			this.numShowTop.Location = new System.Drawing.Point(96, 10);
			this.numShowTop.Maximum = new System.Decimal(new int[] {
																	   1000000,
																	   0,
																	   0,
																	   0});
			this.numShowTop.Name = "numShowTop";
			this.numShowTop.Size = new System.Drawing.Size(48, 20);
			this.numShowTop.TabIndex = 5;
			this.numShowTop.Value = new System.Decimal(new int[] {
																	 10,
																	 0,
																	 0,
																	 0});
			this.numShowTop.ValueChanged += new System.EventHandler(this.numShowTop_ValueChanged);
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
			// graphControl
			// 
			this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.graphControl.Location = new System.Drawing.Point(0, 0);
			this.graphControl.Name = "graphControl";
			this.graphControl.RotateClockwise = true;
			this.graphControl.Size = new System.Drawing.Size(504, 421);
			this.graphControl.TabIndex = 3;
			this.graphControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.graphControl_MouseMove);
			// 
			// CommentCount
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 461);
			this.Controls.Add(this.graphControl);
			this.Controls.Add(this.pnlControls);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimizeBox = false;
			this.Name = "CommentCount";
			this.ShowInTaskbar = false;
			this.Text = "Comment Count Analyzer";
			this.pnlControls.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numShowTop)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region IPlugin Members
		public Image MenuIcon
		{
			get { return new System.Drawing.Bitmap(this.GetType(), "res.CommentCount.png"); }
		}

		public string Description
		{
			get { return "Analyzes comment counts in your journal, both given and received."; }
		}

		public string Author
		{
			get { return "Erik Frey"; }
		}

		public void Go(Journal j)
		{
			this.j = j;
			Cursor.Current = Cursors.WaitCursor;
			c = new EF.ljArchive.Plugins.Core.CommentCount(j);
			graphControl.GraphPane = c.GetGraph((int) numShowTop.Value, graphControl.Height - 1,
				graphControl.Width - 1);
			toolTip = new ToolTip(this.components);
			Cursor.Current = Cursors.Default;
			ShowDialog();
		}

		public string Title
		{
			get { return "Comment Count Analyzer"; }
		}

		public string URL
		{
			get { return "http://ljarchive.sourceforge.net/"; }
		}

		public Version Version
		{
			get { return new Version(0, 9, 4, 3); }
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

		private Journal j;
		private Plugins.Core.CommentCount c;
		private string toolTipText = null;

		private void numShowTop_ValueChanged(object sender, System.EventArgs e)
		{
			graphControl.GraphPane = c.GetGraph((int) numShowTop.Value, graphControl.Height - 1,
				graphControl.Width - 1);
			graphControl.Invalidate();
		}

		private void graphControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			double x, y, y2;
			int i;
			ZedGraph.GraphPane pane = graphControl.GraphPane;
			pane.ReverseTransform(new PointF(e.Y, e.X), out x, out y, out y2);
			i = (int) Math.Round(x) - 1;
			if (i >= 0 && i < pane.XAxis.TextLabels.Length)
			{
				string s = string.Format("Comments\nReceived from {0}: {1}\nGiven to {0}: {2}",
					pane.XAxis.TextLabels[i], pane.CurveList[0].Y[i], pane.CurveList[1].Y[i]);
				if (toolTipText != s)
				{
					toolTipText = s;
					toolTip.SetToolTip(graphControl, s);	
				}
			}
		}
	}
}
