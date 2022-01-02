using System;
using System.Drawing;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EF.ljArchive.WindowsForms
{
	/// <summary>
	/// Summary description for Explorer.
	/// </summary>
	public class Explorer : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl;
		private Genghis.Windows.Forms.ContainerStatusBar statusBar;
		private Genghis.Windows.Forms.ContainerStatusBarPanel sbpProgressText;
		private Genghis.Windows.Forms.ContainerStatusBarPanel sbpProgressBar;
		private System.Windows.Forms.PropertyGrid templateProperties;
		private System.Windows.Forms.ComboBox templatePicker;
		private EF.ljArchive.WindowsForms.Controls.FindPanel findPanel;
		private System.Windows.Forms.Panel shim;
		private EF.ljArchive.WindowsForms.Controls.DataGridList dgEvents;
		private EF.ljArchive.WindowsForms.Controls.DataGridList dgComments;
		private System.Windows.Forms.Splitter tabControlSplitter;
		private System.Windows.Forms.TabPage tpEvents;
		private System.Windows.Forms.TabPage tpComments;
		private System.Windows.Forms.Panel htmlSettingsPane;
		private System.Windows.Forms.Splitter htmlSettingsPaneSplitter;
		private System.Windows.Forms.OpenFileDialog ofd;
		private Genghis.Windows.Forms.WindowSerializer windowSerializer;
		private System.Windows.Forms.DataGridTableStyle eventsTableStyle;
		private System.Windows.Forms.DataGridTableStyle commentsTableStyle;
		private Genghis.Windows.Forms.GradientProgressBar gpb;
		private Writer.Html.HtmlControl browser;
		private System.Windows.Forms.Splitter eventsSplitter;
		private EF.ljArchive.WindowsForms.Controls.CalendarPanel calendarPanel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Explorer()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitializeForm();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Explorer));
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tpEvents = new System.Windows.Forms.TabPage();
			this.dgEvents = new EF.ljArchive.WindowsForms.Controls.DataGridList();
			this.eventsTableStyle = new System.Windows.Forms.DataGridTableStyle();
			this.eventsSplitter = new System.Windows.Forms.Splitter();
			this.calendarPanel = new EF.ljArchive.WindowsForms.Controls.CalendarPanel();
			this.tpComments = new System.Windows.Forms.TabPage();
			this.dgComments = new EF.ljArchive.WindowsForms.Controls.DataGridList();
			this.commentsTableStyle = new System.Windows.Forms.DataGridTableStyle();
			this.tabControlSplitter = new System.Windows.Forms.Splitter();
			this.statusBar = new Genghis.Windows.Forms.ContainerStatusBar();
			this.gpb = new Genghis.Windows.Forms.GradientProgressBar();
			this.sbpProgressText = new Genghis.Windows.Forms.ContainerStatusBarPanel();
			this.sbpProgressBar = new Genghis.Windows.Forms.ContainerStatusBarPanel();
			this.htmlSettingsPane = new System.Windows.Forms.Panel();
			this.templateProperties = new System.Windows.Forms.PropertyGrid();
			this.templatePicker = new System.Windows.Forms.ComboBox();
			this.htmlSettingsPaneSplitter = new System.Windows.Forms.Splitter();
			this.findPanel = new EF.ljArchive.WindowsForms.Controls.FindPanel();
			this.shim = new System.Windows.Forms.Panel();
			this.ofd = new System.Windows.Forms.OpenFileDialog();
			this.windowSerializer = new Genghis.Windows.Forms.WindowSerializer();
			this.browser = new Writer.Html.HtmlControl();
			this.tabControl.SuspendLayout();
			this.tpEvents.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgEvents)).BeginInit();
			this.tpComments.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgComments)).BeginInit();
			this.statusBar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sbpProgressText)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpProgressBar)).BeginInit();
			this.htmlSettingsPane.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabControl.Controls.Add(this.tpEvents);
			this.tabControl.Controls.Add(this.tpComments);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Left;
			this.tabControl.Location = new System.Drawing.Point(0, 124);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(300, 387);
			this.tabControl.TabIndex = 0;
			this.tabControl.Visible = false;
			// 
			// tpEvents
			// 
			this.tpEvents.Controls.Add(this.dgEvents);
			this.tpEvents.Controls.Add(this.eventsSplitter);
			this.tpEvents.Controls.Add(this.calendarPanel);
			this.tpEvents.Location = new System.Drawing.Point(4, 25);
			this.tpEvents.Name = "tpEvents";
			this.tpEvents.Size = new System.Drawing.Size(292, 358);
			this.tpEvents.TabIndex = 0;
			this.tpEvents.Text = "Entries";
			// 
			// dgEvents
			// 
			this.dgEvents.CaptionVisible = false;
			this.dgEvents.DataMember = "";
			this.dgEvents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgEvents.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgEvents.Location = new System.Drawing.Point(0, 163);
			this.dgEvents.Name = "dgEvents";
			this.dgEvents.Size = new System.Drawing.Size(292, 195);
			this.dgEvents.TabIndex = 0;
			this.dgEvents.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																								 this.eventsTableStyle});
			this.dgEvents.CurrentRowChanged += new System.EventHandler(this.dg_CurrentRowChanged);
			// 
			// eventsTableStyle
			// 
			this.eventsTableStyle.DataGrid = this.dgEvents;
			this.eventsTableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.eventsTableStyle.MappingName = "Events";
			this.eventsTableStyle.RowHeadersVisible = false;
			this.eventsTableStyle.SelectionBackColor = System.Drawing.Color.PowderBlue;
			this.eventsTableStyle.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			// 
			// eventsSplitter
			// 
			this.eventsSplitter.Dock = System.Windows.Forms.DockStyle.Top;
			this.eventsSplitter.Location = new System.Drawing.Point(0, 160);
			this.eventsSplitter.Name = "eventsSplitter";
			this.eventsSplitter.Size = new System.Drawing.Size(292, 3);
			this.eventsSplitter.TabIndex = 3;
			this.eventsSplitter.TabStop = false;
			this.eventsSplitter.Visible = false;
			// 
			// calendarPanel
			// 
			this.calendarPanel.BackColor = System.Drawing.SystemColors.Window;
			this.calendarPanel.BoldedDates = new System.DateTime[0];
			this.calendarPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.calendarPanel.Location = new System.Drawing.Point(0, 0);
			this.calendarPanel.Name = "calendarPanel";
			this.calendarPanel.SelectionRange = new System.Windows.Forms.SelectionRange(new System.DateTime(2004, 8, 21, 0, 0, 0, 0), new System.DateTime(2004, 8, 21, 0, 0, 0, 0));
			this.calendarPanel.Size = new System.Drawing.Size(292, 160);
			this.calendarPanel.TabIndex = 4;
			this.calendarPanel.Visible = false;
			this.calendarPanel.CloseClicked += new System.EventHandler(this.calendarPanel_CloseClicked);
			this.calendarPanel.DateClicked += new System.EventHandler(this.calendarPanel_DateClicked);
			// 
			// tpComments
			// 
			this.tpComments.Controls.Add(this.dgComments);
			this.tpComments.Location = new System.Drawing.Point(4, 25);
			this.tpComments.Name = "tpComments";
			this.tpComments.Size = new System.Drawing.Size(292, 358);
			this.tpComments.TabIndex = 1;
			this.tpComments.Text = "Comments";
			// 
			// dgComments
			// 
			this.dgComments.CaptionVisible = false;
			this.dgComments.DataMember = "";
			this.dgComments.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgComments.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgComments.Location = new System.Drawing.Point(0, 0);
			this.dgComments.Name = "dgComments";
			this.dgComments.Size = new System.Drawing.Size(292, 358);
			this.dgComments.TabIndex = 1;
			this.dgComments.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																								   this.commentsTableStyle});
			this.dgComments.CurrentRowChanged += new System.EventHandler(this.dg_CurrentRowChanged);
			// 
			// commentsTableStyle
			// 
			this.commentsTableStyle.DataGrid = this.dgComments;
			this.commentsTableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.commentsTableStyle.MappingName = "Comments";
			this.commentsTableStyle.RowHeadersVisible = false;
			this.commentsTableStyle.SelectionBackColor = System.Drawing.Color.PowderBlue;
			this.commentsTableStyle.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			// 
			// tabControlSplitter
			// 
			this.tabControlSplitter.Location = new System.Drawing.Point(300, 124);
			this.tabControlSplitter.Name = "tabControlSplitter";
			this.tabControlSplitter.Size = new System.Drawing.Size(3, 387);
			this.tabControlSplitter.TabIndex = 1;
			this.tabControlSplitter.TabStop = false;
			this.tabControlSplitter.Visible = false;
			// 
			// statusBar
			// 
			this.statusBar.Controls.Add(this.gpb);
			this.statusBar.Location = new System.Drawing.Point(0, 511);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new Genghis.Windows.Forms.ContainerStatusBarPanel[] {
																								   this.sbpProgressText,
																								   this.sbpProgressBar});
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(728, 22);
			this.statusBar.TabIndex = 3;
			// 
			// gpb
			// 
			this.gpb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.gpb.EndColor = System.Drawing.Color.Navy;
			this.gpb.GradientType = Genghis.Windows.Forms.GradientType.StartToEnd;
			this.gpb.Location = new System.Drawing.Point(303, 3);
			this.gpb.Maximum = 100;
			this.gpb.Minimum = 0;
			this.gpb.Name = "gpb";
			this.gpb.Size = new System.Drawing.Size(408, 18);
			this.gpb.StartColor = System.Drawing.Color.SteelBlue;
			this.gpb.Step = 1;
			this.gpb.TabIndex = 7;
			this.gpb.TickWidth = 3;
			this.gpb.Value = 0;
			// 
			// sbpProgressText
			// 
			this.sbpProgressText.Text = "Ready";
			this.sbpProgressText.Width = 300;
			// 
			// sbpProgressBar
			// 
			this.sbpProgressBar.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.sbpProgressBar.HostedControl = this.gpb;
			this.sbpProgressBar.Style = System.Windows.Forms.StatusBarPanelStyle.OwnerDraw;
			this.sbpProgressBar.Width = 412;
			// 
			// htmlSettingsPane
			// 
			this.htmlSettingsPane.Controls.Add(this.templateProperties);
			this.htmlSettingsPane.Controls.Add(this.templatePicker);
			this.htmlSettingsPane.Dock = System.Windows.Forms.DockStyle.Right;
			this.htmlSettingsPane.Location = new System.Drawing.Point(480, 124);
			this.htmlSettingsPane.Name = "htmlSettingsPane";
			this.htmlSettingsPane.Size = new System.Drawing.Size(248, 387);
			this.htmlSettingsPane.TabIndex = 4;
			this.htmlSettingsPane.Visible = false;
			// 
			// templateProperties
			// 
			this.templateProperties.CommandsVisibleIfAvailable = true;
			this.templateProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.templateProperties.LargeButtons = false;
			this.templateProperties.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.templateProperties.Location = new System.Drawing.Point(0, 21);
			this.templateProperties.Name = "templateProperties";
			this.templateProperties.Size = new System.Drawing.Size(248, 366);
			this.templateProperties.TabIndex = 1;
			this.templateProperties.Text = "propertyGrid1";
			this.templateProperties.ViewBackColor = System.Drawing.SystemColors.Window;
			this.templateProperties.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.templateProperties.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.templateProperties_PropertyValueChanged);
			// 
			// templatePicker
			// 
			this.templatePicker.Dock = System.Windows.Forms.DockStyle.Top;
			this.templatePicker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.templatePicker.Location = new System.Drawing.Point(0, 0);
			this.templatePicker.Name = "templatePicker";
			this.templatePicker.Size = new System.Drawing.Size(248, 21);
			this.templatePicker.TabIndex = 0;
			this.templatePicker.SelectedIndexChanged += new System.EventHandler(this.templatePicker_SelectedIndexChanged);
			// 
			// htmlSettingsPaneSplitter
			// 
			this.htmlSettingsPaneSplitter.Dock = System.Windows.Forms.DockStyle.Right;
			this.htmlSettingsPaneSplitter.Location = new System.Drawing.Point(477, 124);
			this.htmlSettingsPaneSplitter.Name = "htmlSettingsPaneSplitter";
			this.htmlSettingsPaneSplitter.Size = new System.Drawing.Size(3, 387);
			this.htmlSettingsPaneSplitter.TabIndex = 5;
			this.htmlSettingsPaneSplitter.TabStop = false;
			this.htmlSettingsPaneSplitter.Visible = false;
			// 
			// findPanel
			// 
			this.findPanel.BackColor = System.Drawing.SystemColors.Window;
			this.findPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.findPanel.Location = new System.Drawing.Point(0, 0);
			this.findPanel.Name = "findPanel";
			this.findPanel.Size = new System.Drawing.Size(728, 120);
			this.findPanel.TabIndex = 1;
			this.findPanel.Visible = false;
			this.findPanel.CloseClicked += new System.EventHandler(this.findPanel_CloseClicked);
			this.findPanel.FindClicked += new System.EventHandler(this.findPanel_FindClicked);
			// 
			// shim
			// 
			this.shim.Dock = System.Windows.Forms.DockStyle.Top;
			this.shim.Location = new System.Drawing.Point(0, 120);
			this.shim.Name = "shim";
			this.shim.Size = new System.Drawing.Size(728, 4);
			this.shim.TabIndex = 6;
			// 
			// windowSerializer
			// 
			this.windowSerializer.Form = this;
			this.windowSerializer.FormName = "Explorer";
			// 
			// browser
			// 
			this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.browser.IsDesignMode = false;
			this.browser.Location = new System.Drawing.Point(303, 124);
			this.browser.Name = "browser";
			this.browser.ScriptEnabled = false;
			this.browser.ScriptObject = null;
			this.browser.Size = new System.Drawing.Size(174, 387);
			this.browser.TabIndex = 7;
			this.browser.Text = "htmlControl1";
			this.browser.ReadyStateComplete += new System.EventHandler(this.browser_ReadyStateComplete);
			// 
			// Explorer
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(728, 533);
			this.Controls.Add(this.browser);
			this.Controls.Add(this.htmlSettingsPaneSplitter);
			this.Controls.Add(this.htmlSettingsPane);
			this.Controls.Add(this.tabControlSplitter);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.shim);
			this.Controls.Add(this.findPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(440, 300);
			this.Name = "Explorer";
			this.Text = "ljArchive";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Explorer_Closing);
			this.Load += new System.EventHandler(this.Explorer_Load);
			this.tabControl.ResumeLayout(false);
			this.tpEvents.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgEvents)).EndInit();
			this.tpComments.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgComments)).EndInit();
			this.statusBar.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.sbpProgressText)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpProgressBar)).EndInit();
			this.htmlSettingsPane.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Protected Instance Methods
		protected void InitializeForm()
		{
			DataGridColumnStyle col;
			Hashtable imageMap = new Hashtable();

			// prefs
			prefs = Genghis.Preferences.GetUserNode(this.GetType());

			// tc
			tc = FindTemplateCollection();

			// hjw
			hjw = new EF.ljArchive.Engine.HTML.HTMLJournalWriter();

			// plugins
			plugins = new EF.ljArchive.Engine.Collections.PluginCollection(Path.Combine(Application.StartupPath
				,"plugins"));
			plugins.Settings = prefs.GetString("plugins.Settings", plugins.Settings);

			// journalWriters
			journalWriters = new EF.ljArchive.Engine.Collections.JournalWriterCollection(
				Path.Combine(Application.StartupPath ,"plugins"));
			journalWriters.Insert(0, hjw);
			journalWriters.Settings = prefs.GetString("journalWriters.Settings", journalWriters.Settings);

			// cbm
			cbm = new Controls.CommandBarManager(plugins, journalWriters);
			cbm.CommandClick += new System.EventHandler(cbm_Command_Click);
			cbm.CommandState = EF.ljArchive.WindowsForms.Controls.CommandBarManager.CommandStates.FileClosed;
			cbm.Grid.IsChecked = prefs.GetBoolean("cbm.Grid.IsChecked", cbm.Grid.IsChecked);
			cbm.Find.IsChecked = prefs.GetBoolean("cbm.Find.IsChecked", cbm.Find.IsChecked);
			cbm.Calendar.IsChecked = prefs.GetBoolean("cbm.Calendar.IsChecked", cbm.Calendar.IsChecked);
			cbm.HtmlSettings.IsChecked = prefs.GetBoolean("cbm.HtmlSettings.IsChecked", cbm.HtmlSettings.IsChecked);
			cbm.PluginClick += new EventHandler(Plugin_Click);
			cbm.JournalWriterClick += new EventHandler(JournalWriter_Click);
			this.Controls.Add(cbm);

			// Localize
			Localizer.SetControlText(this.GetType(), new Control[] {tpEvents, tpComments});

			// ofd
			ofd.Filter = Localizer.GetString(this.GetType(), "ofd.Filter");

			// templateProperties
			if (hjw.Settings == null)
				hjw.Settings = Engine.HTML.HTMLJournalWriterSettings.CreateDefault();
			templateProperties.SelectedObject = hjw.Settings;

			// tabControl
			if (prefs.GetString("tabControl.Dock", "Left") == "Left")
			{
				this.tabControl.Dock = DockStyle.Left;
				this.tabControlSplitter.Dock = DockStyle.Left;
				this.tabControl.Width = prefs.GetInt32("tabControl.Size", tabControl.Width);
				this.calendarPanel.Dock = DockStyle.Top;
				this.calendarPanel.Height = prefs.GetInt32("calendarPanel.Size", calendarPanel.Height);
				this.eventsSplitter.Dock = DockStyle.Top;
			}
			else
			{
				this.tabControl.Dock = DockStyle.Top;
				this.tabControlSplitter.Dock = DockStyle.Top;
				this.tabControl.Height = prefs.GetInt32("tabControl.Size", tabControl.Height);
				this.calendarPanel.Dock = DockStyle.Left;
				this.calendarPanel.Width = prefs.GetInt32("calendarPanel.Size", calendarPanel.Width);
				this.eventsSplitter.Dock = DockStyle.Left;
			}

			// templatePicker
			templatePicker.Items.AddRange(tc.Names);
			if (templatePicker.Items.Contains(prefs.GetString("templatePicker.SelectedItem", "component")))
				templatePicker.SelectedItem = prefs.GetString("templatePicker.SelectedItem", "component");
			else
				templatePicker.SelectedIndex = 0;

			// dgEvents
			imageMap.Add("private", new Bitmap(this.GetType(), "res.IconPrivate.png"));
			imageMap.Add("usemask", new Bitmap(this.GetType(), "res.IconProtected.png"));
			col = new Controls.DataGridImageColumn("Security", string.Empty, 20, imageMap);
			eventsTableStyle.GridColumnStyles.Add(col);
			col = new Controls.DataGridTextViewColumn("ID", "ID", 80, false);
			eventsTableStyle.GridColumnStyles.Add(col);
			col = new Controls.DataGridTextViewColumn("Date", "Date", 120, false);
			eventsTableStyle.GridColumnStyles.Add(col);
			col = new Controls.DataGridTextViewColumn("Subject", "Subject", 240, false);
			eventsTableStyle.GridColumnStyles.Add(col);
			col = new Controls.DataGridTextViewColumn("Body", "Event Text", 480, true);
			eventsTableStyle.GridColumnStyles.Add(col);
			dgEvents.ColumnSettings = prefs.GetString("dgEvents.ColumnSettings",
				".2+.2+.2+.2+.2;20+20+20+20+20");

			// dgComments
			commentsTableStyle.GridColumnStyles.Clear();
			imageMap = new Hashtable();
			imageMap.Add("D", new Bitmap(this.GetType(), "res.IconDeleted.png"));
			col = new Controls.DataGridImageColumn("State", string.Empty, 20, imageMap);
			commentsTableStyle.GridColumnStyles.Add(col);
			col = new Controls.DataGridTextViewColumn("ID", "ID", 80, false);
			commentsTableStyle.GridColumnStyles.Add(col);
			col = new Controls.DataGridTextViewColumn("PosterUserName", "User", 120, false);
			commentsTableStyle.GridColumnStyles.Add(col);
			col = new Controls.DataGridTextViewColumn("Date", "Date", 120, false);
			commentsTableStyle.GridColumnStyles.Add(col);
			col = new Controls.DataGridTextViewColumn("Subject", "Subject", 120, true);
			commentsTableStyle.GridColumnStyles.Add(col);
			col = new Controls.DataGridTextViewColumn("Body", "Body", 120, true);
			commentsTableStyle.GridColumnStyles.Add(col);
			dgComments.ColumnSettings = prefs.GetString("dgComments.ColumnSettings",
				".16+.16+.16+.16+.16+.16;20+20+20+20+20+20");

			// moveToComment
			moveToComment = -1;

			// lastUpdateCheck
			// I'm open to a cleaner implementation of this.  --Deb
			lastUpdateCheck = DateTime.MinValue;
			try 
			{
				lastUpdateCheck = (DateTime) prefs.GetProperty("lastUpdateCheck", DateTime.MinValue);
			}
			catch (FormatException)  {}
			catch (InvalidCastException) {}

			if (DateTime.MinValue == lastUpdateCheck) 
			{
				// Ah, well.  Reset out lastUpdateCheck and use MinValue.
				// Done here so that if we fall through any of the exception handlers,
				// we do the right things.
				prefs.SetProperty("lastUpdateCheck", DateTime.MinValue);
			}

			// updateRequested
			updateRequested = false;

			// syncOnStartup
			syncOnStartup = prefs.GetBoolean("syncOnStartup", true);
		}

		protected void UpdateStatus(Engine.SyncOperationEventArgs soe)
		{
			switch (soe.SyncOperation)
			{
				case Engine.SyncOperation.ExportCommentsMeta:
				case Engine.SyncOperation.Initialize:
				case Engine.SyncOperation.Login:
				case Engine.SyncOperation.SessionGenerate:
					sbpProgressText.Text = Localizer.GetString("SyncOperation." + soe.SyncOperation);
					break;
				case Engine.SyncOperation.ExportCommentsBody:
					sbpProgressText.Text = string.Format(Localizer.GetString("SyncOperation.ExportCommentsBody"),
						soe.Param1, soe.Param2);
					if (soe.Param2 != 0)
						gpb.Value = 55 + ((40 * soe.Param1) / soe.Param2);
					break;
				case Engine.SyncOperation.GetEvents:
					sbpProgressText.Text = string.Format(Localizer.GetString("SyncOperation.GetEvents"),
						soe.Param1, soe.Param2);
					if (soe.Param2 != 0)
						gpb.Value = 15 + ((40 * soe.Param1) / soe.Param2);
					break;
				case Engine.SyncOperation.SyncItems:
					sbpProgressText.Text = string.Format(Localizer.GetString("SyncOperation.SyncItems"),
						soe.Param1, soe.Param2);
					if (soe.Param2 != 0)
						gpb.Value = ((15 * soe.Param1) / soe.Param2);
					break;
				case Engine.SyncOperation.Merge:
					cbm.CommandState = EF.ljArchive.WindowsForms.Controls.CommandBarManager.CommandStates.FileClosed;
					UnBind();
					sbpProgressText.Text = Localizer.GetString("SyncOperation.Merge");
					break;
				case Engine.SyncOperation.Success:
					using (Busy b = new Busy())
					{
						j.Save();
						Bind();
					}
					gpb.Value = 0;
					cbm.CommandState = EF.ljArchive.WindowsForms.Controls.CommandBarManager.CommandStates.Normal;
					sbpProgressText.Text = string.Format(Localizer.GetString("SyncOperation.Success"),
						soe.Param1, soe.Param2);;
					break;
				case Engine.SyncOperation.PartialSync:
					using (Busy b = new Busy())
					{
						j.Save();
						Bind();
					}
					gpb.Value = 0;
					cbm.CommandState = EF.ljArchive.WindowsForms.Controls.CommandBarManager.CommandStates.Normal;
					sbpProgressText.Text = string.Format(Localizer.GetString("SyncOperation.PartialSync"),
						soe.Param1, soe.Param2);;
					ReportError();
					break;
				case Engine.SyncOperation.Failure:
					Bind();
					gpb.Value = 0;
					cbm.CommandState = EF.ljArchive.WindowsForms.Controls.CommandBarManager.CommandStates.Normal;
					sbpProgressText.Text = Localizer.GetString("SyncOperation.Failure");
					ReportError();
					break;
			}
		}

		protected void ReportError()
		{
			if (Engine.Sync.SyncException.GetType() == typeof(Engine.ExpectedSyncException))
			{
				Engine.ExpectedSyncException ese = (Engine.ExpectedSyncException) Engine.Sync.SyncException;
				switch (ese.ExpectedError)
				{
					case Engine.ExpectedError.Cancel:
						// this one we don't need a popup box for
						sbpProgressText.Text =
							Localizer.GetString("ExpectedError.ExpectedErrorCategories.SyncCancel");
						break;
					default:
						Dialogs.ExpectedError.Go((Dialogs.ExpectedError.ExpectedErrorCategories)
							Enum.Parse(typeof(Dialogs.ExpectedError.ExpectedErrorCategories), "Sync" +
							ese.ExpectedError.ToString()), ese, this);
						break;
				}
			}
			else
			{
				Dialogs.GeneralError.Go(Engine.Sync.SyncException.ToString(), this);
			}
		}

		protected void Open(string path, bool showError)
		{
			ArrayList al = new ArrayList();
			try
			{
				using (Busy b = new Busy())
				{
					EF.ljArchive.Engine.Journal jNew = Engine.Journal.Load(path);
					if (dgEvents.DataSource != null)
						UnBind();
					j = jNew;
					Bind();
					cbm.CommandState = EF.ljArchive.WindowsForms.Controls.CommandBarManager.CommandStates.Normal;
				}
			}
			catch (System.InvalidCastException ice)
			{
				if (showError)
					Dialogs.ExpectedError.Go(Dialogs.ExpectedError.ExpectedErrorCategories.BadFileFormat, ice, this);
			}
			catch (System.IO.FileNotFoundException fnfe)
			{
				if (showError)
					Dialogs.ExpectedError.Go(Dialogs.ExpectedError.ExpectedErrorCategories.BadFileFormat, fnfe, this);
			}
			catch (System.Reflection.TargetInvocationException tie)
			{
				if (showError)
					Dialogs.ExpectedError.Go(Dialogs.ExpectedError.ExpectedErrorCategories.BadFileFormat, tie, this);				
			}
			cbm.Grid.IsChecked &= (j != null);
			cbm_Command_Click(cbm.Grid, EventArgs.Empty);
			cbm.HtmlSettings.IsChecked &= (j != null);
			cbm_Command_Click(cbm.HtmlSettings, EventArgs.Empty);
			cbm.Find.IsChecked &= (j != null);
			cbm_Command_Click(cbm.Find, EventArgs.Empty);
			cbm.Calendar.IsChecked &= (j != null);
			cbm_Command_Click(cbm.Calendar, EventArgs.Empty);
			if (j != null)
			{
				this.Text = "ljArchive - " + path;
				foreach (Common.Journal.EventsRow er in j.Events)
					al.Add(er.Date);
				calendarPanel.BoldedDates = (DateTime[]) al.ToArray(typeof(DateTime));
				if (prefs.GetString("dgEvents.View.Sort", null) == null)
				{
					dgComments.View.Sort = "[Date] DESC";
					dgEvents.View.Sort = "[Date] DESC";
				}
			}
		}

		protected void New()
		{
			Type t = this.GetType();
			Genghis.Windows.Forms.WizardSheet ws =
				new Genghis.Windows.Forms.WizardSheet("Create New Journal Archive");
			Controls.NewJournalArchiveIntroPage njaip = new Controls.NewJournalArchiveIntroPage(Localizer.GetString(t,
				"njaip.PageTitle"), Localizer.GetString(t, "njaip.PageDescription"));
			Controls.NewJournalArchiveAccountSettings njaas = new Controls.NewJournalArchiveAccountSettings(Localizer.GetString(t,
				"njaas.PageTitle"), Localizer.GetString(t, "njaas.PageDescription"));
			Controls.NewJournalArchiveFinish njaf = new Controls.NewJournalArchiveFinish(Localizer.GetString(t,
				"njaf.PageTitle"), Localizer.GetString(t, "njaf.PageDescription"));
			ws.AddPage(njaip);
			ws.AddPage(njaas);
			ws.AddPage(njaf);
			ws.EnableFinishButton = false;
			ws.AutoScale = false;
			if (ws.ShowDialog() == DialogResult.OK)
			{
				if (njaip.Path.IndexOfAny(Path.InvalidPathChars) > -1)
					return;
				UnBind();
				j = njaf.NewJournal;
				if (!Directory.Exists(Path.GetDirectoryName(njaip.Path)))
					Directory.CreateDirectory(Path.GetDirectoryName(njaip.Path));
				j.Save(njaip.Path);
				this.Text = "ljArchive - " + j.Path;
				Bind();
				cbm_Command_Click(cbm.Sync, EventArgs.Empty);
			}
		}

		protected void UnBind()
		{
			dgEvents.DataSource = null;
			dgComments.DataSource = null;
			
		}

		protected void Bind()
		{
			dgEvents.DataSource = j;
			dgEvents.DataMember = "Events";
			dgComments.DataSource = j;
			dgComments.DataMember = "Comments";
			dgEvents.View.AllowNew = false;
			dgComments.View.AllowNew = false;
			dgEvents.RefreshSelect();
		}

		// Handle keyboard shortcuts
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (cbm.PreProcessMessage(ref msg))
				return true;
			return base.ProcessCmdKey(ref msg, keyData);
		}

		protected void UpdateBrowser()
		{
			Common.Journal.CommentsRow cr;
			Common.Journal.EventsRow er;
			string html = string.Empty;

			if (!this.Visible)
				return;
			cr = dgComments.CurrentDataRow as Common.Journal.CommentsRow;
			if (cr != null)
				hjw.SelectedCommentID = cr.ID;
			using (MemoryStream ms = new MemoryStream())
			{
				er = dgEvents.CurrentDataRow as Common.Journal.EventsRow;
				if (er != null)
				{
					hjw.WriteJournal(ms, j, new int[] {er.ID}, null, true, true);
					ms.Seek(0, SeekOrigin.Begin);
					using (StreamReader sr = new StreamReader(ms))
						html = sr.ReadToEnd();
				}
				else
				{
					html = Localizer.GetString(this.GetType(), "browser.NoEntryText");
				}
			}
			browser.LoadHtml(html, null);
		}

		protected void AskUpdate()
		{
			if (MessageBox.Show(Localizer.GetString(this.GetType(), "UpdateExistsMessage"),
				Localizer.GetString(this.GetType(), "UpdateExistsTitle"), MessageBoxButtons.YesNo) ==
				DialogResult.Yes)
			{
				updateRequested = true;
				this.Close();
			}
		}

		protected Size FlipSize(Size oldSize, Rectangle bounds)
		{
			System.Drawing.Size s = new Size(oldSize.Height, oldSize.Width);
			if (s.Width > bounds.Width - 10)
				s.Width = bounds.Width - 10;
			if (s.Height > bounds.Height - 10)
				s.Height = bounds.Height - 10;
			return s;
		}
		#endregion

		#region Private Instance Methods
		private void Explorer_Load(object sender, System.EventArgs e)
		{
			Application.DoEvents();
			if (System.Environment.GetCommandLineArgs().Length > 1)
				Open(System.Environment.GetCommandLineArgs()[1], true);
			else if (prefs.GetString("j.Path", "") != "")
				Open(prefs.GetString("j.Path", ""), false);
			if (j == null)
			{
				browser.LoadHtml(Localizer.GetString(this.GetType(), "browser.DefaultText"));
			}
			else
			{
				dgComments.View.Sort = prefs.GetString("dgComments.View.Sort", dgComments.View.Sort);
				dgEvents.View.Sort = prefs.GetString("dgEvents.View.Sort", dgComments.View.Sort);
				if (prefs.GetInt32("dgComments.CurrentRowIndex", -1) != -1)
					dgComments.CurrentRowIndex = prefs.GetInt32("dgComments.CurrentRowIndex", 0);
				if (prefs.GetInt32("dgEvents.CurrentRowIndex", -1) != -1)
					dgEvents.CurrentRowIndex = prefs.GetInt32("dgEvents.CurrentRowIndex", 0);
				if (dgEvents.CurrentRowIndex != -1 && !dgEvents.IsSelected(dgEvents.CurrentRowIndex))
					dgEvents.RefreshSelect();
				if ((DateTime.Now - lastUpdateCheck).TotalDays >= 2F)
				{
					lastUpdateCheck = DateTime.Now;
					UpdateChecker.Start(new UpdateExistsCallBack(UpdateExists));
				}
				if (syncOnStartup)
					cbm_Command_Click(cbm.Sync, EventArgs.Empty);
			}
		}

		private void Explorer_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (UpdateChecker.IsAlive)
				UpdateChecker.Abort();
			if (Engine.Sync.IsAlive)
				Engine.Sync.Abort();
			prefs.SetProperty("dgEvents.ColumnSettings", dgEvents.ColumnSettings);
			prefs.SetProperty("dgComments.ColumnSettings", dgComments.ColumnSettings);
			if (tabControl.Dock == DockStyle.Left)
			{
				prefs.SetProperty("tabControl.Dock", "Left");
				prefs.SetProperty("tabControl.Size", tabControl.Width);
				prefs.SetProperty("calendarPanel.Size", calendarPanel.Height);
			}
			else
			{
				prefs.SetProperty("tabControl.Dock", "Top");
				prefs.SetProperty("tabControl.Size", tabControl.Height);
				prefs.SetProperty("calendarPanel.Size", calendarPanel.Width);
			}
			prefs.SetProperty("cbm.Grid.IsChecked", cbm.Grid.IsChecked);
			prefs.SetProperty("cbm.Find.IsChecked", cbm.Find.IsChecked);
			prefs.SetProperty("cbm.Calendar.IsChecked", cbm.Calendar.IsChecked);
			prefs.SetProperty("cbm.HtmlSettings.IsChecked", cbm.HtmlSettings.IsChecked);
			prefs.SetProperty("j.Path", (j == null? "" : j.Path));
			prefs.SetProperty("templatePicker.SelectedItem", templatePicker.SelectedItem);
			if (dgComments.View != null)
				prefs.SetProperty("dgComments.View.Sort", dgComments.View.Sort);
			if (dgEvents.View != null)
				prefs.SetProperty("dgEvents.View.Sort", dgEvents.View.Sort);
			prefs.SetProperty("dgComments.CurrentRowIndex", dgComments.CurrentRowIndex);
			prefs.SetProperty("dgEvents.CurrentRowIndex", dgEvents.CurrentRowIndex);
			prefs.SetProperty("journalWriters.Settings", journalWriters.Settings);
			prefs.SetProperty("plugins.Settings", plugins.Settings);
			prefs.SetProperty("lastUpdateCheck", lastUpdateCheck.ToString());
			prefs.SetProperty("syncOnStartup", syncOnStartup);
		}

		private void UpdateExists()
		{
			if (this.IsHandleCreated)
				this.Invoke(new TS_UpdateExistsDelegate(TS_UpdateExists));
		}

		private void TS_UpdateExists()
		{
			AskUpdate();
		}

		private void cbm_Command_Click(object sender, EventArgs e)
		{
			if (sender == cbm.Find)
			{
				findPanel.Visible = cbm.Find.IsChecked;
				if (cbm.Find.IsChecked)
				{
					if (cbm.Calendar.IsChecked)
					{
						cbm.Calendar.IsChecked = false;
						cbm_Command_Click(cbm.Calendar, EventArgs.Empty);
					}
				}
				else
				{
					DataView dvEvents = dgEvents.View;
					DataView dvComments = dgComments.View;
					if (dvEvents != null)
						dvEvents.RowFilter = string.Empty;
					if (dvComments != null)
						dvComments.RowFilter = string.Empty;
				}
			}
			else if (sender == cbm.Calendar)
			{
				calendarPanel.Visible = cbm.Calendar.IsChecked;
				eventsSplitter.Visible = cbm.Calendar.IsChecked;
				if (cbm.Calendar.IsChecked && !cbm.Grid.IsChecked)
				{
					cbm.Grid.IsChecked = true;
					cbm_Command_Click(cbm.Grid, EventArgs.Empty);
				}
				if (cbm.Calendar.IsChecked)
				{
					if (cbm.Find.IsChecked)
					{
						cbm.Find.IsChecked = false;
						cbm_Command_Click(cbm.Find, EventArgs.Empty);
					}
					calendarPanel_DateClicked(calendarPanel, EventArgs.Empty);
					if (tabControl.SelectedTab != tpEvents)
						tabControl.SelectedTab = tpEvents;
				}
				else
				{
					DataView dvEvents = dgEvents.View;
					DataView dvComments = dgComments.View;
					if (dvEvents != null)
						dvEvents.RowFilter = string.Empty;
					if (dvComments != null)
						dvComments.RowFilter = string.Empty;
				}
			}
			else if (sender == cbm.Grid)
			{
				this.SuspendLayout();
				tabControlSplitter.Visible = cbm.Grid.IsChecked;
				tabControl.Visible = cbm.Grid.IsChecked;
				if (tabControl.Visible && htmlSettingsPane.Visible)
				{
					htmlSettingsPane.BringToFront();
					htmlSettingsPaneSplitter.BringToFront();
					browser.BringToFront();
				}
				if (!cbm.Grid.IsChecked && cbm.Calendar.IsChecked)
				{
					cbm.Calendar.IsChecked = false;
					cbm_Command_Click(cbm.Calendar, EventArgs.Empty);
				}
				this.ResumeLayout(true);
			}
			else if (sender == cbm.HtmlSettings)
			{
				this.SuspendLayout();
				htmlSettingsPaneSplitter.Visible = cbm.HtmlSettings.IsChecked;
				htmlSettingsPane.Visible = cbm.HtmlSettings.IsChecked;
				if (tabControl.Visible && htmlSettingsPane.Visible)
				{
					htmlSettingsPane.BringToFront();
					htmlSettingsPaneSplitter.BringToFront();
					browser.BringToFront();
				}
				this.ResumeLayout(true);
			}
			else if (sender == cbm.GoBack)
			{
				CurrencyManager cm;
				if (dgComments.Visible)
					cm = (CurrencyManager) this.BindingContext[dgComments.DataSource, dgComments.DataMember];
				else
					cm = (CurrencyManager) this.BindingContext[dgEvents.DataSource, dgEvents.DataMember];
				cm.Position -= 1;
				if (!dgEvents.Visible && !dgComments.Visible)
					dgEvents.RefreshSelect();
			}
			else if (sender == cbm.GoForward)
			{
				CurrencyManager cm;
				if (dgComments.Visible)
					cm = (CurrencyManager) this.BindingContext[dgComments.DataSource, dgComments.DataMember];
				else
					cm = (CurrencyManager) this.BindingContext[dgEvents.DataSource, dgEvents.DataMember];
				cm.Position += 1;
				if (!dgEvents.Visible && !dgComments.Visible)
					dgEvents.RefreshSelect();
			}
			else if (sender == cbm.About)
			{
				(new Dialogs.About()).ShowDialog();
			}
			else if (sender == cbm.Open)
			{
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					Open(ofd.FileName, true);
				}
			}
			else if (sender == cbm.Sync)
			{
				cbm.CommandState = EF.ljArchive.WindowsForms.Controls.CommandBarManager.CommandStates.Syncing;
				Engine.Sync.Start(j, new Engine.SyncOperationCallBack(Sync_SyncOperationCallBack));
			}
			else if (sender == cbm.Abort)
			{
				Engine.Sync.Abort();
			}
			else if (sender == cbm.Print)
			{
				browser.Print();
			}
			else if (sender == cbm.Website)
			{
				try
				{
					System.Diagnostics.Process.Start("https://github.com/sharpden/ljarchive");
				}
				catch (System.ComponentModel.Win32Exception) {}
			}
			else if (sender == cbm.GotoEntry)
			{
				BindingManagerBase bm = this.BindingContext[dgEvents.DataSource, dgEvents.DataMember];
				DataRowView drv = (DataRowView) bm.Current;
				Common.Journal.EventsRow er = (Common.Journal.EventsRow) drv.Row;
				Uri uri, baseURI = new Uri(j.Options.ServerURL);
				if (er.IsAnumNull())
					uri = new Uri(baseURI, "/users/" +
					j.Options.UserName + "/" + er.Date.ToString("yyyy\\/MM\\/dd\\/"));
				else
					uri = new Uri(baseURI, string.Format("/{0}/{1}/{2}.html",
				                                         (j.Options.IsUseJournalNull() ? "users" : "community"),
				                                         (j.Options.IsUseJournalNull() ? j.Options.UserName : j.Options.UseJournal),
				                                         er.ID * 256 + er.Anum));
				try
				{
					System.Diagnostics.Process.Start(uri.AbsoluteUri);
				}
				catch (System.ComponentModel.Win32Exception) {}
			}
			else if (sender == cbm.NewArchive)
			{
				New();
			}
			else if (sender == cbm.JournalSettings)
			{
				Dialogs.Properties.Go(j);
			}
			else if (sender == cbm.Exit)
			{
				Close();
			}
			else if (sender == cbm.Options)
			{
				DockStyle d = tabControl.Dock;
				Dialogs.Options.Go(ref d, ref lastUpdateCheck, ref syncOnStartup);
				if (Dialogs.Options.UpdateCheckRequested)
				{
					AskUpdate();
					return;
				}
				if (d != tabControl.Dock)
				{
					this.SuspendLayout();
					tabControl.Size = FlipSize(tabControl.Size, this.ClientRectangle);
					tabControl.Dock = d;
					tabControlSplitter.Dock = d;
					calendarPanel.Size = FlipSize(calendarPanel.Size, this.ClientRectangle);
					calendarPanel.Dock = (tabControl.Dock == DockStyle.Top ? DockStyle.Left : DockStyle.Top);
					eventsSplitter.Dock = calendarPanel.Dock;
					this.ResumeLayout();
				}
			}
		}

		private void Sync_SyncOperationCallBack(Engine.SyncOperationEventArgs soe)
		{
			if (this.IsHandleCreated)
				this.Invoke(new UpdateStatusDelegate(UpdateStatus), new object[] {soe});
		}

		private void templateProperties_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			UpdateBrowser();
		}

		private void dg_CurrentRowChanged(object sender, System.EventArgs e)
		{
			if (sender == dgComments)
			{
				Common.Journal.CommentsRow cr = dgComments.CurrentDataRow as Common.Journal.CommentsRow;
				Common.Journal.EventsRow er = dgEvents.CurrentDataRow as Common.Journal.EventsRow;
				if (cr != null)
					moveToComment = cr.ID;
				if (dgComments.Visible && cr != null && er != null && cr.JItemID != er.ID)
				{
					DataView dv = dgEvents.View;
					for (int i = 0; i < dv.Count; ++i)
					{
						er = (Common.Journal.EventsRow) dv[i].Row;
						if (er.ID == cr.JItemID)
						{
							dgEvents.CurrentRowIndex = i;
							return;
						}
					}
				}
				else
				{
					UpdateBrowser();
				}
			}
			else
			{
				Common.Journal.EventsRow er = (Common.Journal.EventsRow) dgEvents.CurrentDataRow;
				if (er != null)
					plugins.SelectedEventID = er.ID;
				UpdateBrowser();
			}
		}
	
		private void templatePicker_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			hjw.Transform = tc[templatePicker.SelectedIndex];
			UpdateBrowser();
		}

		private void findPanel_FindClicked(object sender, System.EventArgs e)
		{
			DataView dvEvents = dgEvents.View;
			DataView dvComments = dgComments.View;

			if (findPanel.FindText.Trim().Length > 0)
			{
				if (dvComments != null)
				{
					if (findPanel.SearchComments)
					{
						dvComments.RowFilter = string.Format("(Body LIKE '*{0}*') OR (Subject LIKE '*{0}*')",
							findPanel.EscapedFindText);
						hjw.CommentSearchString = findPanel.FindText;
					}
					else
					{
						dvComments.RowFilter = "(1 = 0)";
						hjw.CommentSearchString = string.Empty;
					}
				}
				if (dvEvents != null)
				{
					StringBuilder sb = new StringBuilder("-1");
					string rowFilter;
					if (findPanel.SearchEvents)
					{
						rowFilter = string.Format("(Body LIKE '*{0}*') OR (Subject LIKE '*{0}*') OR" +
							" (CurrentMood LIKE '*{0}*') OR (CurrentMusic LIKE '*{0}*')", findPanel.EscapedFindText);
						hjw.EntrySearchString = findPanel.FindText;
					}
					else
					{
						rowFilter = "(1 = 0)";
						hjw.EntrySearchString = string.Empty;
					}
					foreach (DataRowView drv in dvComments)
					{
						sb.Append(',');
						sb.Append(drv["JItemID"]);
					}
					rowFilter += string.Format(" OR (ID IN ({0}))", sb.ToString());
					dvEvents.RowFilter = rowFilter;
					if (dgComments.Visible)
						dgComments.RefreshSelect();
					else
						dgEvents.RefreshSelect();
				}
			}
			else
			{
				if (dvEvents != null)
					dvEvents.RowFilter = string.Empty;
				if (dvComments != null)
					dvComments.RowFilter = string.Empty;
			}
			if (!tabControl.Visible)
			{
				cbm.Grid.IsChecked = true;
				cbm_Command_Click(cbm.Grid, EventArgs.Empty);
			}
		}

		private void findPanel_CloseClicked(object sender, System.EventArgs e)
		{
			cbm.Find.IsChecked = false;
			cbm_Command_Click(cbm.Find, EventArgs.Empty);
		}

		private void Plugin_Click(object sender, EventArgs e)
		{
			Common.IPlugin plugin = (Common.IPlugin) sender;
			plugin.Go(j);
		}

		private void JournalWriter_Click(object sender, EventArgs e)
		{
			Common.IJournalWriter ijw = (Common.IJournalWriter) sender;
			if (ijw == hjw)
			{
				int selectedCommentID = hjw.SelectedCommentID;
				string entrySearchString = hjw.EntrySearchString;
				string commentSearchString = hjw.CommentSearchString;
				hjw.SelectedCommentID = -1;
				hjw.EntrySearchString = string.Empty;
				hjw.CommentSearchString = string.Empty;
				Dialogs.Export.Go(j, ijw);
				hjw.CommentSearchString = commentSearchString;
				hjw.EntrySearchString = entrySearchString;
				hjw.SelectedCommentID = selectedCommentID;
			}
			else
			{
				Dialogs.Export.Go(j, ijw);
			}
		}

		private void browser_ReadyStateComplete(object sender, System.EventArgs e)
		{
			if (moveToComment != -1)
			{
				Writer.Html.NativeMethods.IHTMLElementCollection collection = browser.HtmlDocument.GetAnchors();

				for (int i = 0; i < collection.GetLength(); ++i)
				{
					object[] pvars = new object[1];
					Writer.Html.NativeMethods.IHTMLElement element = (Writer.Html.NativeMethods.IHTMLElement) collection.Item(i, i);

					element.GetAttribute("name", 0, pvars);
					if (pvars.Length > 0 && pvars[0] != null)
					{
						string tagName = pvars[0] as string;
						if (tagName == null)
							continue;
						if (string.Format("t{0}", moveToComment) == tagName)
						{
							element.ScrollIntoView(true);
							break;
						}
					}
				}
				moveToComment = -1;
			}
		}

		private void calendarPanel_CloseClicked(object sender, System.EventArgs e)
		{
			DataView dvEvents = dgEvents.View;
			DataView dvComments = dgComments.View;
			if (dvEvents != null)
				dvEvents.RowFilter = string.Empty;
			if (dvComments != null)
				dvComments.RowFilter = string.Empty;
			cbm.Calendar.IsChecked = false;
			cbm_Command_Click(cbm.Calendar, EventArgs.Empty);
		}

		private void calendarPanel_DateClicked(object sender, System.EventArgs e)
		{
			DataView dvEvents = dgEvents.View;
			DataView dvComments = dgComments.View;

			if (dvEvents != null)
			{
				string rowFilter = string.Format("Date > #{0:d}# AND Date < #{1:d}#",
					calendarPanel.SelectionRange.Start.Date, calendarPanel.SelectionRange.Start.Date.AddDays(1F));
				dvEvents.RowFilter = rowFilter;
				dgEvents.RefreshSelect();
				if (dvComments != null)
					dvComments.RowFilter = string.Empty;
			}
		}

		private static TemplateCollection FindTemplateCollection() 
		{
			TemplateCollection found = null;
			#if DEBUG
			try
			{
				found = new TemplateCollection(Path.Combine(Application.StartupPath, "templates"));
			}
			catch (ArgumentException)
			{
				// We might be in development, try that.
				string path = Path.GetDirectoryName(Path.GetDirectoryName(Application.StartupPath));
				path = Path.Combine(Path.Combine(path, "etc"), "templates");
				found = new TemplateCollection(path);
			}
			#else
			found = new TemplateCollection(Path.Combine(Application.StartupPath, "templates"));
			#endif
			if (found.Count < 1)
				throw new Exception("No templates found.");

			return found;
		}
		#endregion

		#region Public Instance Properties
		public bool UpdateRequested
		{
			get { return this.updateRequested; }
			set { this.updateRequested = value; }
		}
		#endregion

		#region Private Instance Fields
		private Genghis.Preferences prefs;
		private Controls.CommandBarManager cbm;
		private EF.ljArchive.Engine.Journal j;
		private delegate void UpdateStatusDelegate(Engine.SyncOperationEventArgs soe);
		private Engine.Collections.PluginCollection plugins;
		private Engine.Collections.JournalWriterCollection journalWriters;
		private Engine.HTML.HTMLJournalWriter hjw;
		private TemplateCollection tc;
		private int moveToComment;
		private delegate void TS_UpdateExistsDelegate();
		private DateTime lastUpdateCheck;
		private bool updateRequested;
		private bool syncOnStartup;
		#endregion

	}
}
