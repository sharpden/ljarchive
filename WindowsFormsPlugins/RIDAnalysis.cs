using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using EF.ljArchive.Common;
using EF.ljArchive.Plugins.Core;

namespace EF.ljArchive.Plugins.WindowsForms
{
	/// <summary>
	/// Summary description for RIDAnalysis.
	/// </summary>
	public class RIDAnalysis : System.Windows.Forms.Form, IPlugin
	{
		private System.Windows.Forms.Panel pnlControls;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tpSummary;
		private System.Windows.Forms.TabPage tpOverTime;
		private System.Windows.Forms.RichTextBox rtbInfo;
		private System.Windows.Forms.ProgressBar pgb;
		private System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ComboBox cmbPresets;
		private System.Windows.Forms.TreeView tv;
		private ZedGraph.ZedGraphControl graphSummary;
		private ZedGraph.ZedGraphControl graphOverTime;
		private System.Windows.Forms.Label lblBaseline;
		private System.Windows.Forms.TabPage tpIntro;
		private System.Windows.Forms.ComboBox cmbBaseline;
		private System.Windows.Forms.Panel pnlCatgories;
		private System.Windows.Forms.TextBox txtHTML;
		private System.Windows.Forms.Button btnPreview;
		private System.Windows.Forms.Button btnCopyToClipboard;
		private System.Windows.Forms.TabPage tpHTMLCode;
		private System.Windows.Forms.Panel pnlHTMLCode;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RIDAnalysis()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitializeDialog();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RIDAnalysis));
			this.pnlControls = new System.Windows.Forms.Panel();
			this.cmbBaseline = new System.Windows.Forms.ComboBox();
			this.lblBaseline = new System.Windows.Forms.Label();
			this.lblProgress = new System.Windows.Forms.Label();
			this.pgb = new System.Windows.Forms.ProgressBar();
			this.btnOK = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tpIntro = new System.Windows.Forms.TabPage();
			this.rtbInfo = new System.Windows.Forms.RichTextBox();
			this.tpSummary = new System.Windows.Forms.TabPage();
			this.graphSummary = new ZedGraph.ZedGraphControl();
			this.tpOverTime = new System.Windows.Forms.TabPage();
			this.graphOverTime = new ZedGraph.ZedGraphControl();
			this.tpHTMLCode = new System.Windows.Forms.TabPage();
			this.txtHTML = new System.Windows.Forms.TextBox();
			this.pnlHTMLCode = new System.Windows.Forms.Panel();
			this.btnPreview = new System.Windows.Forms.Button();
			this.btnCopyToClipboard = new System.Windows.Forms.Button();
			this.pnlCatgories = new System.Windows.Forms.Panel();
			this.tv = new System.Windows.Forms.TreeView();
			this.cmbPresets = new System.Windows.Forms.ComboBox();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.pnlControls.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tpIntro.SuspendLayout();
			this.tpSummary.SuspendLayout();
			this.tpOverTime.SuspendLayout();
			this.tpHTMLCode.SuspendLayout();
			this.pnlHTMLCode.SuspendLayout();
			this.pnlCatgories.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlControls
			// 
			this.pnlControls.Controls.Add(this.cmbBaseline);
			this.pnlControls.Controls.Add(this.lblBaseline);
			this.pnlControls.Controls.Add(this.lblProgress);
			this.pnlControls.Controls.Add(this.pgb);
			this.pnlControls.Controls.Add(this.btnOK);
			this.pnlControls.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlControls.Location = new System.Drawing.Point(0, 429);
			this.pnlControls.Name = "pnlControls";
			this.pnlControls.Size = new System.Drawing.Size(576, 40);
			this.pnlControls.TabIndex = 3;
			// 
			// cmbBaseline
			// 
			this.cmbBaseline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbBaseline.Enabled = false;
			this.cmbBaseline.Items.AddRange(new object[] {
															 "Everyone",
															 "My Gender",
															 "My Age Group"});
			this.cmbBaseline.Location = new System.Drawing.Point(312, 10);
			this.cmbBaseline.Name = "cmbBaseline";
			this.cmbBaseline.Size = new System.Drawing.Size(121, 21);
			this.cmbBaseline.TabIndex = 6;
			this.cmbBaseline.SelectedIndexChanged += new System.EventHandler(this.cmbBaseline_SelectedIndexChanged);
			// 
			// lblBaseline
			// 
			this.lblBaseline.AutoSize = true;
			this.lblBaseline.Location = new System.Drawing.Point(256, 12);
			this.lblBaseline.Name = "lblBaseline";
			this.lblBaseline.Size = new System.Drawing.Size(51, 16);
			this.lblBaseline.TabIndex = 5;
			this.lblBaseline.Text = "Baseline:";
			// 
			// lblProgress
			// 
			this.lblProgress.AutoSize = true;
			this.lblProgress.Location = new System.Drawing.Point(8, 12);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(63, 16);
			this.lblProgress.TabIndex = 4;
			this.lblProgress.Text = "Analyzing...";
			// 
			// pgb
			// 
			this.pgb.Location = new System.Drawing.Point(80, 10);
			this.pgb.Name = "pgb";
			this.pgb.Size = new System.Drawing.Size(152, 20);
			this.pgb.TabIndex = 3;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(480, 8);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "&OK";
			// 
			// tabControl
			// 
			this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabControl.Controls.Add(this.tpIntro);
			this.tabControl.Controls.Add(this.tpSummary);
			this.tabControl.Controls.Add(this.tpOverTime);
			this.tabControl.Controls.Add(this.tpHTMLCode);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(187, 3);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(389, 426);
			this.tabControl.TabIndex = 4;
			// 
			// tpIntro
			// 
			this.tpIntro.Controls.Add(this.rtbInfo);
			this.tpIntro.Location = new System.Drawing.Point(4, 25);
			this.tpIntro.Name = "tpIntro";
			this.tpIntro.Size = new System.Drawing.Size(381, 397);
			this.tpIntro.TabIndex = 0;
			this.tpIntro.Text = "Introduction";
			// 
			// rtbInfo
			// 
			this.rtbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbInfo.Location = new System.Drawing.Point(0, 0);
			this.rtbInfo.Name = "rtbInfo";
			this.rtbInfo.ReadOnly = true;
			this.rtbInfo.Size = new System.Drawing.Size(381, 397);
			this.rtbInfo.TabIndex = 0;
			this.rtbInfo.Text = "";
			// 
			// tpSummary
			// 
			this.tpSummary.Controls.Add(this.graphSummary);
			this.tpSummary.Location = new System.Drawing.Point(4, 25);
			this.tpSummary.Name = "tpSummary";
			this.tpSummary.Size = new System.Drawing.Size(381, 397);
			this.tpSummary.TabIndex = 1;
			this.tpSummary.Text = "Summary";
			// 
			// graphSummary
			// 
			this.graphSummary.Dock = System.Windows.Forms.DockStyle.Fill;
			this.graphSummary.Location = new System.Drawing.Point(0, 0);
			this.graphSummary.Name = "graphSummary";
			this.graphSummary.Size = new System.Drawing.Size(381, 397);
			this.graphSummary.TabIndex = 4;
			// 
			// tpOverTime
			// 
			this.tpOverTime.Controls.Add(this.graphOverTime);
			this.tpOverTime.Location = new System.Drawing.Point(4, 25);
			this.tpOverTime.Name = "tpOverTime";
			this.tpOverTime.Size = new System.Drawing.Size(381, 397);
			this.tpOverTime.TabIndex = 2;
			this.tpOverTime.Text = "Over Time";
			// 
			// graphOverTime
			// 
			this.graphOverTime.Dock = System.Windows.Forms.DockStyle.Fill;
			this.graphOverTime.Location = new System.Drawing.Point(0, 0);
			this.graphOverTime.Name = "graphOverTime";
			this.graphOverTime.Size = new System.Drawing.Size(381, 397);
			this.graphOverTime.TabIndex = 5;
			// 
			// tpHTMLCode
			// 
			this.tpHTMLCode.Controls.Add(this.txtHTML);
			this.tpHTMLCode.Controls.Add(this.pnlHTMLCode);
			this.tpHTMLCode.Location = new System.Drawing.Point(4, 25);
			this.tpHTMLCode.Name = "tpHTMLCode";
			this.tpHTMLCode.Size = new System.Drawing.Size(381, 397);
			this.tpHTMLCode.TabIndex = 3;
			this.tpHTMLCode.Text = "HTML Code";
			// 
			// txtHTML
			// 
			this.txtHTML.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtHTML.Location = new System.Drawing.Point(0, 0);
			this.txtHTML.Multiline = true;
			this.txtHTML.Name = "txtHTML";
			this.txtHTML.ReadOnly = true;
			this.txtHTML.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtHTML.Size = new System.Drawing.Size(381, 357);
			this.txtHTML.TabIndex = 0;
			this.txtHTML.Text = "";
			// 
			// pnlHTMLCode
			// 
			this.pnlHTMLCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlHTMLCode.Controls.Add(this.btnPreview);
			this.pnlHTMLCode.Controls.Add(this.btnCopyToClipboard);
			this.pnlHTMLCode.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlHTMLCode.Location = new System.Drawing.Point(0, 357);
			this.pnlHTMLCode.Name = "pnlHTMLCode";
			this.pnlHTMLCode.Size = new System.Drawing.Size(381, 40);
			this.pnlHTMLCode.TabIndex = 1;
			// 
			// btnPreview
			// 
			this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPreview.Location = new System.Drawing.Point(252, 9);
			this.btnPreview.Name = "btnPreview";
			this.btnPreview.Size = new System.Drawing.Size(112, 23);
			this.btnPreview.TabIndex = 1;
			this.btnPreview.Text = "Preview";
			this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
			// 
			// btnCopyToClipboard
			// 
			this.btnCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCopyToClipboard.Location = new System.Drawing.Point(132, 8);
			this.btnCopyToClipboard.Name = "btnCopyToClipboard";
			this.btnCopyToClipboard.Size = new System.Drawing.Size(112, 23);
			this.btnCopyToClipboard.TabIndex = 0;
			this.btnCopyToClipboard.Text = "Copy to Clipboard";
			this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
			// 
			// pnlCatgories
			// 
			this.pnlCatgories.Controls.Add(this.tv);
			this.pnlCatgories.Controls.Add(this.cmbPresets);
			this.pnlCatgories.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlCatgories.Location = new System.Drawing.Point(0, 3);
			this.pnlCatgories.Name = "pnlCatgories";
			this.pnlCatgories.Size = new System.Drawing.Size(184, 426);
			this.pnlCatgories.TabIndex = 5;
			// 
			// tv
			// 
			this.tv.CheckBoxes = true;
			this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv.Enabled = false;
			this.tv.ImageIndex = -1;
			this.tv.Location = new System.Drawing.Point(0, 21);
			this.tv.Name = "tv";
			this.tv.SelectedImageIndex = -1;
			this.tv.Size = new System.Drawing.Size(184, 405);
			this.tv.TabIndex = 6;
			this.tv.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterCheck);
			// 
			// cmbPresets
			// 
			this.cmbPresets.Dock = System.Windows.Forms.DockStyle.Top;
			this.cmbPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPresets.Enabled = false;
			this.cmbPresets.ItemHeight = 13;
			this.cmbPresets.Items.AddRange(new object[] {
															"Top 3 Categories",
															"Bottom 3 Categories",
															"Root Categories",
															"Primary Process",
															"Secondary Process",
															"Emotions",
															"Custom Categories"});
			this.cmbPresets.Location = new System.Drawing.Point(0, 0);
			this.cmbPresets.Name = "cmbPresets";
			this.cmbPresets.Size = new System.Drawing.Size(184, 21);
			this.cmbPresets.TabIndex = 0;
			this.cmbPresets.SelectedIndexChanged += new System.EventHandler(this.cmbPresets_SelectedIndexChanged);
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(184, 3);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 426);
			this.splitter1.TabIndex = 6;
			this.splitter1.TabStop = false;
			// 
			// RIDAnalysis
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 469);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.pnlCatgories);
			this.Controls.Add(this.pnlControls);
			this.DockPadding.Top = 3;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(456, 256);
			this.Name = "RIDAnalysis";
			this.ShowInTaskbar = false;
			this.Text = "Regressive Imagery Analysis";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.RIDAnalysis_Closing);
			this.pnlControls.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.tpIntro.ResumeLayout(false);
			this.tpSummary.ResumeLayout(false);
			this.tpOverTime.ResumeLayout(false);
			this.tpHTMLCode.ResumeLayout(false);
			this.pnlHTMLCode.ResumeLayout(false);
			this.pnlCatgories.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region IPlugin Members
		public Image MenuIcon
		{
			get
			{
				return new System.Drawing.Bitmap(this.GetType(), "res.RIDAnalysis.png");
			}
		}

		public string Title
		{
			get
			{
				return "Regressive Imagery Analysis";
			}
		}

		public string Description
		{
			get
			{
				return "Performs a Regressive Imagery Analysis on your journal.";
			}
		}

		public string Author
		{
			get
			{
				return "Erik Frey";
			}
		}

		public void Go(Journal j)
		{
			RIDAnalysisSettings ridas = new RIDAnalysisSettings(-1, 'm');
			if (settings.ContainsKey(j.Options[0].ServerURL + j.Options[0].UserName))
			{
				ridas = (RIDAnalysisSettings) settings[j.Options[0].ServerURL + j.Options[0].UserName];
			}
			else
			{
				RIDBaseline ridb = new RIDBaseline();
				if (ridb.ShowDialog() == DialogResult.OK && ridb.Age > 0)
				{
					ridas.Age = ridb.Age;
					ridas.Gender = ridb.Gender;
					settings.Add(j.Options[0].ServerURL + j.Options[0].UserName, ridas);
				}
			}
			tabControl.TabPages.Remove(tpSummary);
			tabControl.TabPages.Remove(tpOverTime);
			tabControl.TabPages.Remove(tpHTMLCode);
			tv.Nodes.Clear();
			tv.Enabled = false;
			cmbPresets.Enabled = false;
			cmbPresets.SelectedIndex = -1;
			pgb.Value = 0;
			lblProgress.Text = "Analyzing...";
			cmbBaseline.Enabled = false;
			cmbBaseline.SelectedIndex = 0;
			EF.ljArchive.Plugins.Core.RIDAnalysis.Start(j, ridas.Gender, ridas.Age,
				new AnalysisStatusCallBack(rid_AnalysisStatusCallBack));
			this.ShowDialog();
		}

		public string URL
		{
			get
			{
				return "http://fawx.com/ljArchive";
			}
		}

		public Version Version
		{
			get
			{
				return new Version(0, 9, 4, 0);
			}
		}

		public object Settings
		{
			get { return settings; }
			set { settings = (Hashtable) value; }
		}

		public int SelectedEventID
		{
			set { }
		}
		#endregion

		private void InitializeDialog()
		{
			using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(
					   "EF.ljArchive.Plugins.WindowsForms.res.RIDInfo.rtf"))
				rtbInfo.LoadFile(s, RichTextBoxStreamType.RichText);
			settings = new Hashtable();
		}

		private void RIDAnalysis_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (EF.ljArchive.Plugins.Core.RIDAnalysis.IsAlive)
				EF.ljArchive.Plugins.Core.RIDAnalysis.Abort();
		}

		private void rid_AnalysisStatusCallBack(AnalysisStatusEventArgs asea)
		{
			this.Invoke(new UpdateStatusDelegate(UpdateStatus), new object[] {asea.Status, asea.PercentComplete});
		}

		private void UpdateStatus(AnalysisStatus status, int percentComplete)
		{
			if (status == AnalysisStatus.NetFailed && MessageBox.Show("The server is not responding.  Retry?",
				"Error Contacting Server", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				EF.ljArchive.Plugins.Core.RIDAnalysis.RetryServer();
			}
			else if (status == AnalysisStatus.NetFailed)
			{
				pgb.Value = 0;
				lblProgress.Text = "Failed.";
			}
			else if (status == AnalysisStatus.Success)
			{
				UpdateTreeView();
				lblProgress.Text = "Done.";
				if (EF.ljArchive.Plugins.Core.RIDAnalysis.Averages.Length > 1)
					cmbBaseline.Enabled = true;
				cmbBaseline.SelectedIndex = 0;
				cmbPresets.Enabled = true;
				cmbPresets.SelectedIndex = 0;
				tabControl.TabPages.Add(tpSummary);
				tabControl.TabPages.Add(tpOverTime);
				tabControl.TabPages.Add(tpHTMLCode);
				tabControl.SelectedIndex = 1;
			}
			pgb.Value = percentComplete;
		}

		private void UpdateTreeView()
		{
			tv.BeginUpdate();
			string[] old = new string[0];
			foreach (string s in EF.ljArchive.Plugins.Core.RIDAnalysis.Categories)
			{
				string[] sections = s.Split(':');
				TreeNode tn = null;
				for (int i = 0; i < sections.Length; ++i)
				{
					string section = sections[i];
					if (i >= old.Length || !((IList) old).Contains(section))
					{
						if (tn == null)
							tn = tv.Nodes.Add(section);
						else
							tn = tn.Nodes.Add(section);
					}
					else
					{
						if (tn == null)
							tn = FindNode(tv.Nodes, section);
						else
							tn = FindNode(tn.Nodes, section);
					}
				}
				old = sections;
			}
			tv.ExpandAll();
			tv.Enabled = true;
			tv.EndUpdate();
		}

		private TreeNode FindNode(TreeNodeCollection tnc, string text)
		{
			foreach (TreeNode tn in tnc)
				if (tn.Text == text)
					return tn;
			return null;
		}

		private TreeNode FindNodeRecurse(TreeNodeCollection tnc, string text)
		{
			foreach (TreeNode tn in tnc)
			{
				if (tn.Text == text)
				{
					return tn;
				}
				else if (tn.Nodes.Count > 0)
				{
					TreeNode tnFind = FindNodeRecurse(tn.Nodes, text);
					if (tnFind != null)
						return tnFind;
				}
			}
			return null;
		}

		private delegate void UpdateStatusDelegate(AnalysisStatus status, int percentComplete);
		private Hashtable settings;
		private bool manualUpdating = false;

		private void tv_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (!manualUpdating)
			{
				if (cmbPresets.SelectedIndex != cmbPresets.Items.Count - 1)
                    cmbPresets.SelectedIndex = cmbPresets.Items.Count - 1;
				else
					UpdateGraphs();
			}
		}

		private void cmbPresets_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string[] categories;
			manualUpdating = true;
			switch (cmbPresets.SelectedIndex)
			{
				case 0:
					categories = EF.ljArchive.Plugins.Core.RIDAnalysis.GetTopThreeCategories(
						(EF.ljArchive.Plugins.Core.RIDBaselines) cmbBaseline.SelectedIndex);
					ClearChecks(tv.Nodes);
					foreach (string category in categories)
                        FindNodeRecurse(tv.Nodes, category.Substring(category.LastIndexOf(":") + 1)).Checked = true;
					break;
				case 1:
					categories = EF.ljArchive.Plugins.Core.RIDAnalysis.GetBottomThreeCategories(
						(EF.ljArchive.Plugins.Core.RIDBaselines) cmbBaseline.SelectedIndex);
					ClearChecks(tv.Nodes);
					foreach (string category in categories)
						FindNodeRecurse(tv.Nodes, category.Substring(category.LastIndexOf(":") + 1)).Checked = true;
					break;
				case 2:
					tv.BeginUpdate();
					ClearChecks(tv.Nodes);
					tv.Nodes[0].Checked = true;
					tv.Nodes[1].Checked = true;
					tv.Nodes[2].Checked = true;
					tv.EndUpdate();
					break;
				case 3:
				case 4:
				case 5:
					tv.BeginUpdate();
					ClearChecks(tv.Nodes);
					foreach (TreeNode tn in tv.Nodes[cmbPresets.SelectedIndex - 3].Nodes)
						tn.Checked = true;
					tv.EndUpdate();
					break;
			}
			manualUpdating = false;
			UpdateGraphs();
		}

		private void ClearChecks(TreeNodeCollection tnc)
		{
			foreach (TreeNode tn in tnc)
			{
				tn.Checked = false;
				ClearChecks(tn.Nodes);
			}
		}

		private void UpdateGraphs()
		{
			ArrayList categories = new ArrayList();
			TreeNode tn;
			if (tv.Nodes.Count < 1)
				return;
			tn = tv.Nodes[0];
			while (tn != null)
			{
				if (tn.Checked)
					categories.Add(tn.FullPath.Replace("\\", ":"));
				if (tn.Nodes.Count > 0)
				{
					tn = tn.Nodes[0];
					continue;
				}
				while (tn.NextNode == null && tn.Parent != null)
					tn = tn.Parent;
				tn = tn.NextNode;
			}
			graphSummary.GraphPane = EF.ljArchive.Plugins.Core.RIDAnalysis.GetSummaryGraph(
				(string[]) categories.ToArray(typeof(string)), (string) cmbPresets.SelectedItem,
				graphSummary.Width - 1, graphSummary.Height - 1, (EF.ljArchive.Plugins.Core.RIDBaselines)
				cmbBaseline.SelectedIndex);
			graphOverTime.GraphPane = EF.ljArchive.Plugins.Core.RIDAnalysis.GetOverTimeGraph(
				(string[]) categories.ToArray(typeof(string)), (string) cmbPresets.SelectedItem,
				graphOverTime.Width - 1, graphOverTime.Height - 1, (EF.ljArchive.Plugins.Core.RIDBaselines)
				cmbBaseline.SelectedIndex);
			txtHTML.Text = EF.ljArchive.Plugins.Core.RIDAnalysis.GetHTMLSummary(
				(string[]) categories.ToArray(typeof(string)), (string) cmbPresets.SelectedItem,
				(EF.ljArchive.Plugins.Core.RIDBaselines) cmbBaseline.SelectedIndex);
		}

		private void cmbBaseline_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateGraphs();
		}

		private void btnCopyToClipboard_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject(txtHTML.Text, true);
		}

		private void btnPreview_Click(object sender, System.EventArgs e)
		{
			string path = Path.Combine(System.IO.Path.GetTempPath(), "ljarchiverid.htm");
			using (StreamWriter sw = new StreamWriter(path))
			{
				sw.WriteLine("<html><head></head><body>");
				sw.WriteLine(txtHTML.Text);
				sw.Write("</body></html>");
			}
			System.Diagnostics.Process.Start(path);
		}
	}

	#region RIDAnalysisSettings
	[Serializable()]
	public class RIDAnalysisSettings
	{
		public RIDAnalysisSettings(int age, char gender)
		{
			this.age = age;
			this.gender = gender;
		}

		public char Gender
		{
			get { return this.gender; }
			set { this.gender = value; }
		}

		public int Age
		{
			get { return this.age; }
			set { this.age = value; }
		}

		private int age;
		private char gender;
	}
	#endregion
}
