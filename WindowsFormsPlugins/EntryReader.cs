using EF.ljArchive.Common;
using System;
using System.Drawing;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EF.ljArchive.Plugins.WindowsForms
{
	/// <summary>
	/// Summary description for EntryReader.
	/// </summary>
	public class EntryReader : System.Windows.Forms.Form, IPlugin
	{
		private System.Windows.Forms.Label lblCharacter;
		private System.Windows.Forms.ComboBox cmbCharacter;
		private System.Windows.Forms.CheckBox chkShowBalloon;
		private System.Windows.Forms.Button btnUnload;
		private AxAgentObjects.AxAgent agentHost;
		private System.Windows.Forms.CheckBox chkAutoSpeak;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public EntryReader()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(EntryReader));
			this.agentHost = new AxAgentObjects.AxAgent();
			this.btnUnload = new System.Windows.Forms.Button();
			this.cmbCharacter = new System.Windows.Forms.ComboBox();
			this.lblCharacter = new System.Windows.Forms.Label();
			this.chkShowBalloon = new System.Windows.Forms.CheckBox();
			this.chkAutoSpeak = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.agentHost)).BeginInit();
			this.SuspendLayout();
			// 
			// agentHost
			// 
			this.agentHost.Enabled = true;
			this.agentHost.Location = new System.Drawing.Point(8, 48);
			this.agentHost.Name = "agentHost";
			this.agentHost.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("agentHost.OcxState")));
			this.agentHost.Size = new System.Drawing.Size(32, 32);
			this.agentHost.TabIndex = 0;
			this.agentHost.MoveEvent += new AxAgentObjects._AgentEvents_MoveEventHandler(this.agentHost_MoveEvent);
			this.agentHost.ClickEvent += new AxAgentObjects._AgentEvents_ClickEventHandler(this.a_ClickEvent);
			this.agentHost.Command += new AxAgentObjects._AgentEvents_CommandEventHandler(this.a_Command);
			this.agentHost.HideEvent += new AxAgentObjects._AgentEvents_HideEventHandler(this.a_HideEvent);
			// 
			// btnUnload
			// 
			this.btnUnload.Location = new System.Drawing.Point(8, 80);
			this.btnUnload.Name = "btnUnload";
			this.btnUnload.Size = new System.Drawing.Size(88, 23);
			this.btnUnload.TabIndex = 1;
			this.btnUnload.Text = "Unload";
			this.btnUnload.Click += new System.EventHandler(this.btnUnload_Click);
			// 
			// cmbCharacter
			// 
			this.cmbCharacter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCharacter.Location = new System.Drawing.Point(96, 16);
			this.cmbCharacter.Name = "cmbCharacter";
			this.cmbCharacter.Size = new System.Drawing.Size(121, 21);
			this.cmbCharacter.TabIndex = 2;
			this.cmbCharacter.SelectedIndexChanged += new System.EventHandler(this.cmbCharacter_SelectedIndexChanged);
			// 
			// lblCharacter
			// 
			this.lblCharacter.Location = new System.Drawing.Point(16, 18);
			this.lblCharacter.Name = "lblCharacter";
			this.lblCharacter.Size = new System.Drawing.Size(64, 23);
			this.lblCharacter.TabIndex = 3;
			this.lblCharacter.Text = "Character:";
			// 
			// chkShowBalloon
			// 
			this.chkShowBalloon.Checked = true;
			this.chkShowBalloon.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkShowBalloon.Location = new System.Drawing.Point(112, 48);
			this.chkShowBalloon.Name = "chkShowBalloon";
			this.chkShowBalloon.TabIndex = 4;
			this.chkShowBalloon.Text = "Show Balloon";
			this.chkShowBalloon.CheckedChanged += new System.EventHandler(this.chkShowBalloon_CheckedChanged);
			// 
			// chkAutoSpeak
			// 
			this.chkAutoSpeak.Checked = true;
			this.chkAutoSpeak.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAutoSpeak.Location = new System.Drawing.Point(112, 80);
			this.chkAutoSpeak.Name = "chkAutoSpeak";
			this.chkAutoSpeak.TabIndex = 5;
			this.chkAutoSpeak.Text = "Auto-speak";
			this.chkAutoSpeak.CheckedChanged += new System.EventHandler(this.chkAutoSpeak_CheckedChanged);
			// 
			// EntryReader
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(226, 114);
			this.Controls.Add(this.chkAutoSpeak);
			this.Controls.Add(this.chkShowBalloon);
			this.Controls.Add(this.lblCharacter);
			this.Controls.Add(this.cmbCharacter);
			this.Controls.Add(this.btnUnload);
			this.Controls.Add(this.agentHost);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "EntryReader";
			this.ShowInTaskbar = false;
			this.Text = "Entry Reader Options";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.EntryReader_Closing);
			((System.ComponentModel.ISupportInitialize)(this.agentHost)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region IPlugin Members
		public Image MenuIcon
		{
			get { return new System.Drawing.Bitmap(this.GetType(), "res.EntryReader.png"); }
		}

		public string Title
		{
			get { return "Entry Reader"; }
		}

		public object Settings
		{
			get { return settings; }
			set { settings = (EntryReaderSettings) value; }
		}

		public string Description
		{
			get { return "Displays an agent who reads your journal."; }
		}

		public string Author
		{
			get { return "Erik Frey"; }
		}

		public void Go(Journal j)
		{
			this.j = j;
			if (settings.SelectedCharacter == null)
				settings.SelectedCharacter = (string) cmbCharacter.Items[0];
			chkShowBalloon.Checked = settings.ShowBalloon;
			if (cmbCharacter.SelectedIndex < 0 && cmbCharacter.Items.IndexOf(settings.SelectedCharacter) > -1)
				cmbCharacter.SelectedItem = settings.SelectedCharacter;
			else if (cmbCharacter.SelectedIndex < 0)
				cmbCharacter.SelectedIndex = 0;
			else
				cmbCharacter_SelectedIndexChanged(cmbCharacter, EventArgs.Empty);
		}

		public string URL
		{
			get { return "http://ljarchive.sourceforge.net/"; }
		}

		public Version Version
		{
			get { return new Version(0, 9, 4, 1); }
		}

		public int SelectedEventID
		{
			set
			{
				selectedEventID = value;
				if (loaded && settings.AutoSpeak)
					Speak();
			}
		}
		#endregion

		private void InitializeDialog()
		{
			DirectoryInfo diSystem = new DirectoryInfo(Environment.SystemDirectory);
			DirectoryInfo diChars = new DirectoryInfo(Path.Combine(Path.Combine(
				diSystem.Parent.FullName, "msagent"), "chars"));
			foreach (FileInfo fi in diChars.GetFiles("*.acs"))
				cmbCharacter.Items.Add(Path.GetFileNameWithoutExtension(fi.Name));
			loaded = false;
			okToUnload = false;
			settings = new EntryReaderSettings(null, Point.Empty, true, true);
			request = null;
		}

		private EntryReaderSettings settings;
		private bool loaded;
		private bool okToUnload;
		private AgentObjects.IAgentCtlCharacterEx agent;
		private Journal j;
		private ArrayList animations;
		private int selectedEventID = 0;
		private AgentObjects.IAgentCtlRequest request;

		private void cmbCharacter_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (loaded)
				agentHost.Characters.Unload("Character");
			settings.SelectedCharacter = (string) cmbCharacter.SelectedItem;
			agentHost.Characters.Load("Character", settings.SelectedCharacter + ".acs");
			agent = agentHost.Characters["Character"];
			agent.Commands.Add("Show Options", "Show Options", "Show Options", true, true);
			agent.Show(null);
			agent.MoveTo( (short) settings.Location.X, (short) settings.Location.Y, null);
			animations = new ArrayList();
			foreach (string name in agent.AnimationNames)
				if (name.ToLower() != "hide")
					animations.Add(name);
			if (!loaded)
			{
				if (agent.TTSModeID == null || agent.TTSModeID == string.Empty)
				{
					request = agent.Speak("Hello! "
						+ "To hear my voice, you must have the proper speech drivers installed on your system. "
						+ "Please visit the ljArchive web site to read how to set them up.", null);
				}
				else
				{
					request = agent.Speak("Hello! "
						+ "I can read your journal entries to you. "
						+ "Left click on me to have me start or stop reading the current entry. "
						+ "For more options, right click on me and select Show Options.", null);
				}
				loaded = true;
			}
		}

		private void a_Command(object sender, AxAgentObjects._AgentEvents_CommandEvent e)
		{
			this.Show();
			this.BringToFront();
		}

		private void a_ClickEvent(object sender, AxAgentObjects._AgentEvents_ClickEvent e)
		{
			if (e.button == 1 )
			{
				if (request != null && request.Status == 4)
					agent.Stop(null);
				else
					Speak();
			}
		}

		private void Speak()
		{
			string text = string.Empty;
			Journal.EventsRow er = j.Events.FindByID(selectedEventID);
			if (er == null)
				return;
			if (!er.IsSubjectNull())
				text = er.Subject.ToLower() + " ...";
			if (!er.IsBodyNull())
				text += er.Body.ToLower();
			text = Regex.Replace(text, @"<\s*?lj\s*?user\s*?=\s*?\""?(\w*?)\""?\s*?>", "$1");
			text = Regex.Replace(text, "<.+?>", " ");
			text = text.Replace("\"", "'").Replace("&nbsp;", " ");
			if (!chkShowBalloon.Checked)
				text = string.Format("\\map=\"{0}\"=\"\"\\", text);
			agent.Stop(null);
			request = agent.Speak(text, null);
			if (animations.Count > 0)
				agent.Play((string) animations[(new Random()).Next(animations.Count)]);
		}

		private void a_HideEvent(object sender, AxAgentObjects._AgentEvents_HideEvent e)
		{
			okToUnload = true;
			this.Close();
		}

		private void btnUnload_Click(object sender, System.EventArgs e)
		{
			okToUnload = true;
			this.Close();
		}

		private void EntryReader_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
			if (okToUnload)
			{
				agentHost.Characters.Unload("Character");
				okToUnload = false;
				loaded = false;
			}
		}

		private void agentHost_MoveEvent(object sender, AxAgentObjects._AgentEvents_MoveEvent e)
		{
			settings.Location = new Point(e.x, e.y);
		}

		private void chkShowBalloon_CheckedChanged(object sender, System.EventArgs e)
		{
			settings.ShowBalloon = chkShowBalloon.Checked;
		}

		private void chkAutoSpeak_CheckedChanged(object sender, System.EventArgs e)
		{
			settings.AutoSpeak = chkAutoSpeak.Checked;
		}
	}

	#region EntryReaderSettings
	[Serializable()]
	public class EntryReaderSettings
	{
		public EntryReaderSettings(string selectedCharacter, Point location, bool showBalloon, bool autoSpeak)
		{
			this.selectedCharacter = selectedCharacter;
			this.location = location;
			this.showBalloon = showBalloon;
			this.autoSpeak = autoSpeak;
		}

		public string SelectedCharacter
		{
			get { return selectedCharacter; }
			set { selectedCharacter = value; }
		}

		public Point Location
		{
			get { return location; }
			set { location = value; }
		}

		public bool ShowBalloon
		{
			get { return showBalloon; }
			set { showBalloon = value; }
		}

		public bool AutoSpeak
		{
			get { return autoSpeak; }
			set { autoSpeak = value; }
		}

		private string selectedCharacter;
		private Point location;
		private bool showBalloon;
		private bool autoSpeak;
	}
	#endregion
}
