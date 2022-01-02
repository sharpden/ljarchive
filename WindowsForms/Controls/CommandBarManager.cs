using System;
using System.Drawing;
using System.Windows.Forms;
using EF.ljArchive.Engine.Collections;
using Reflector.UserInterface;

namespace EF.ljArchive.WindowsForms.Controls
{
	/// <summary>
	/// Summary description for CommandBarManager.
	/// </summary>
	public class CommandBarManager : Reflector.UserInterface.CommandBarManager
	{
		public CommandBarManager(PluginCollection plugins, JournalWriterCollection journalWriters)
		{
			this.plugins = plugins;
			this.journalWriters = journalWriters;
			
			

			// the bars
			menuBar = new CommandBar(CommandBarStyle.Menu);
			toolBar = new CommandBar(CommandBarStyle.ToolBar);

			// commandbarbuttons
			NewArchive = new CommandBarButton(Images.New, Localizer.GetString(this.GetType(), "NewArchive.Text"),
				new EventHandler(CommandButton_Click), Keys.Control | Keys.N);
			Open = new CommandBarButton(Images.Open, Localizer.GetString(this.GetType(), "Open.Text"),
				new EventHandler(CommandButton_Click), Keys.Control | Keys.O);
			Print = new CommandBarButton(Images.Print, Localizer.GetString(this.GetType(), "Print.Text"),
				new EventHandler(CommandButton_Click), Keys.Control | Keys.P);
			Exit = new CommandBarButton(Localizer.GetString(this.GetType(), "Exit.Text"),
				new EventHandler(CommandButton_Click));
			JournalSettings = new CommandBarButton(Images.Edit, Localizer.GetString(this.GetType(),
				"JournalSettings.Text"), new EventHandler(CommandButton_Click));
			GotoEntry = new CommandBarButton(Images.Redo, Localizer.GetString(this.GetType(), "GotoEntry.Text"),
				new EventHandler(CommandButton_Click), Keys.Control | Keys.D);
			GoBack = new CommandBarButton(Images.Back, Localizer.GetString(this.GetType(), "GoBack.Text"),
				new EventHandler(CommandButton_Click), Keys.Control | Keys.Left);
			GoForward = new CommandBarButton(Images.Forward, Localizer.GetString(this.GetType(), "GoForward.Text"),
				new EventHandler(CommandButton_Click), Keys.Control | Keys.Right);
			Find = new CommandBarCheckBox(Images.Search, Localizer.GetString(this.GetType(), "Find.Text"));
			Find.Click += new EventHandler(CommandButton_Click);
			Calendar = new CommandBarCheckBox(Images.Calendar, Localizer.GetString(this.GetType(), "Calendar.Text"));
			Calendar.Click += new EventHandler(CommandButton_Click);
			Grid = new CommandBarCheckBox(Images.Details, Localizer.GetString(this.GetType(), "Grid.Text"));
			Grid.Click += new EventHandler(CommandButton_Click);
			HtmlSettings = new CommandBarCheckBox(Images.Languages, Localizer.GetString(this.GetType(),
				"HtmlSettings.Text"));
			HtmlSettings.Click += new EventHandler(CommandButton_Click);
			Sync = new CommandBarButton(Images.Refresh, Localizer.GetString(this.GetType(), "Sync.Text"),
				new EventHandler(CommandButton_Click));
			Abort = new CommandBarButton(Images.Stop, Localizer.GetString(this.GetType(), "Abort.Text"),
				new EventHandler(CommandButton_Click));
			Options = new CommandBarButton(Images.Tools, Localizer.GetString(this.GetType(), "Options.Text"),
				new EventHandler(CommandButton_Click));
			Website = new CommandBarButton(Images.Help, Localizer.GetString(this.GetType(), "Website.Text"),
				new EventHandler(CommandButton_Click), Keys.F1);
			About = new CommandBarButton(Localizer.GetString(this.GetType(), "About.Text"),
				new EventHandler(CommandButton_Click));

			
			// toolbar
			toolBar.Items.Add(GoBack);
			toolBar.Items.Add(GoForward);
			toolBar.Items.AddSeparator();
			toolBar.Items.Add(Find);
			toolBar.Items.Add(Calendar);
			toolBar.Items.Add(Grid);
			toolBar.Items.Add(HtmlSettings);
			toolBar.Items.AddSeparator();
			toolBar.Items.Add(Sync);
			toolBar.Items.Add(Abort);
			toolBar.Items.AddSeparator();
			pluginsMenu = toolBar.Items.AddMenu(Images.Favorites, Localizer.GetString(this.GetType(), "pluginsMenu.Text"));

			// menus
			fileMenu = menuBar.Items.AddMenu(Localizer.GetString(this.GetType(), "fileMenu.Text"));
			fileMenu.Items.Add(NewArchive);
			fileMenu.Items.Add(Open);
			fileMenu.Items.Add(Print);
			exportMenu = fileMenu.Items.AddMenu(Localizer.GetString(this.GetType(), "exportMenu.Text"));
			fileMenu.Items.AddSeparator();
			fileMenu.Items.Add(Exit);
			editMenu = menuBar.Items.AddMenu(Localizer.GetString(this.GetType(), "editMenu.Text"));
			editMenu.Items.Add(JournalSettings);
			editMenu.Items.AddSeparator();
			editMenu.Items.Add(GotoEntry);
			editMenu.Items.AddSeparator();
			editMenu.Items.Add(GoBack);
			editMenu.Items.Add(GoForward);
			viewMenu = menuBar.Items.AddMenu(Localizer.GetString(this.GetType(), "viewMenu.Text"));
			viewMenu.Items.Add(Find);
			viewMenu.Items.Add(Calendar);
			viewMenu.Items.Add(HtmlSettings);
			viewMenu.Items.Add(Grid);
			toolsMenu = menuBar.Items.AddMenu(Localizer.GetString(this.GetType(), "toolsMenu.Text"));
			toolsMenu.Items.Add(Sync);
			toolsMenu.Items.Add(Abort);
			toolsMenu.Items.AddSeparator();
			toolsMenu.Items.Add(Options);
			toolsMenu.Items.AddSeparator();
			toolsMenu.Items.Add(pluginsMenu);
			helpMenu = menuBar.Items.AddMenu(Localizer.GetString(this.GetType(), "helpMenu.Text"));
			helpMenu.Items.Add(Website);
			helpMenu.Items.Add(About);

			// pluginsMenu
			foreach (Common.IPlugin plugin in plugins)
				pluginsMenu.Items.AddButton(plugin.MenuIcon, plugin.Title, new EventHandler(Plugin_Click));

			// exportMenu
			foreach (Common.IJournalWriter journalWriter in journalWriters)
				exportMenu.Items.AddButton(journalWriter.Name, new EventHandler(JournalWriter_Click));

			this.CommandBars.Add(menuBar);
			this.CommandBars.Add(toolBar);
		}

		public enum CommandStates
		{
			FileClosed,
			Syncing,
			Normal
		}

		public CommandStates CommandState
		{
			get {return commandStates;}
			set
			{
				commandStates = value;
				switch (commandStates)
				{
					case CommandStates.FileClosed:
						NewArchive.IsEnabled = true; Open.IsEnabled = true; Print.IsEnabled = false;
						exportMenu.IsEnabled = false; Exit.IsEnabled = true; JournalSettings.IsEnabled = false;
						GotoEntry.IsEnabled = false; GoBack.IsEnabled = false; GoForward.IsEnabled = false;
						Find.IsEnabled = false; Calendar.IsEnabled = false; Grid.IsEnabled = false;
						HtmlSettings.IsEnabled = false; Sync.IsEnabled = false; Abort.IsEnabled = false;
						Options.IsEnabled = true; pluginsMenu.IsEnabled = false; Website.IsEnabled = true;
						About.IsEnabled = true;
						break;
					case CommandStates.Syncing:
						NewArchive.IsEnabled = false; Open.IsEnabled = false; Print.IsEnabled = false;
						exportMenu.IsEnabled = false; Exit.IsEnabled = true; JournalSettings.IsEnabled = false;
						GotoEntry.IsEnabled = true; GoBack.IsEnabled = true; GoForward.IsEnabled = true;
						Find.IsEnabled = true; Calendar.IsEnabled = true; Grid.IsEnabled = true;
						HtmlSettings.IsEnabled = true; Sync.IsEnabled = false; Abort.IsEnabled = true;
						Options.IsEnabled = false; pluginsMenu.IsEnabled = false; Website.IsEnabled = true;
						About.IsEnabled = false;
						break;
					case CommandStates.Normal:
						NewArchive.IsEnabled = true; Open.IsEnabled = true; Print.IsEnabled = true;
						exportMenu.IsEnabled = true; Exit.IsEnabled = true; JournalSettings.IsEnabled = true;
						GotoEntry.IsEnabled = true; GoBack.IsEnabled = true; GoForward.IsEnabled = true;
						Find.IsEnabled = true; Calendar.IsEnabled = true; Grid.IsEnabled = true;
						HtmlSettings.IsEnabled = true; Sync.IsEnabled = true; Abort.IsEnabled = false;
						Options.IsEnabled = true; pluginsMenu.IsEnabled = true; Website.IsEnabled = true;
						About.IsEnabled = true;
						break;
				}
			}
		}

		public CommandBarButton NewArchive;
		public CommandBarButton Open;
		public CommandBarButton Print;
		public CommandBarButton Exit;
		public CommandBarButton JournalSettings;
		public CommandBarButton GotoEntry;
		public CommandBarButton GoBack;
		public CommandBarButton GoForward;
		public CommandBarCheckBox Find;
		public CommandBarCheckBox Calendar;
		public CommandBarCheckBox Grid;
		public CommandBarCheckBox HtmlSettings;
		public CommandBarButton Sync;
		public CommandBarButton Abort;
		public CommandBarButton Options;
		public CommandBarButton Website;
		public CommandBarButton About;
		private CommandBar menuBar;
		private CommandBar toolBar;
		private CommandBarMenu fileMenu;
		private CommandBarMenu editMenu;
		private CommandBarMenu viewMenu;
		private CommandBarMenu toolsMenu;
		private CommandBarMenu helpMenu;
		private CommandStates commandStates;
		private CommandBarMenu pluginsMenu;
		private CommandBarMenu exportMenu;
		private PluginCollection plugins;
		private JournalWriterCollection journalWriters;

		private void CommandButton_Click(object sender, System.EventArgs e)
		{
			if (CommandClick != null)
				CommandClick(sender, e);
		}

		private void Plugin_Click(object sender, System.EventArgs e)
		{
			int i = pluginsMenu.Items.IndexOf((CommandBarItem) sender);
			if (PluginClick != null)
				PluginClick(plugins[i], EventArgs.Empty);
		}

		private void JournalWriter_Click(object sender, System.EventArgs e)
		{
			int i = exportMenu.Items.IndexOf((CommandBarItem) sender);
			if (JournalWriterClick != null)
				JournalWriterClick(journalWriters[i], EventArgs.Empty);
		}

		public System.EventHandler CommandClick;
		public System.EventHandler PluginClick;
		public System.EventHandler JournalWriterClick;

		private class Images
		{
			private static Image[] images = null;
	
			static Images()
			{
				Bitmap bitmap = new Bitmap(typeof(Explorer), "res.Glyphs16.png");
				int count = (int) (bitmap.Width / bitmap.Height);
				images = new Image[count];
				Rectangle rectangle = new Rectangle(0, 0, bitmap.Height, bitmap.Height);
				for (int i = 0; i < count; i++)
				{
					images[i] = bitmap.Clone(rectangle, bitmap.PixelFormat);
					rectangle.X += bitmap.Height;
				}
			}	

			public static Image New               { get { return images[0];  } }
			public static Image Open              { get { return images[1];  } }
			public static Image Save              { get { return images[2];  } }
			public static Image Cut               { get { return images[3];  } }
			public static Image Copy              { get { return images[4];  } }
			public static Image Paste             { get { return images[5];  } }
			public static Image Delete            { get { return images[6];  } }
			public static Image Properties        { get { return images[7];  } }
			public static Image Undo              { get { return images[8];  } }
			public static Image Redo              { get { return images[9];  } }
			public static Image Preview           { get { return images[10]; } }
			public static Image Print             { get { return images[11]; } }
			public static Image Search            { get { return images[12]; } }
			public static Image ReSearch          { get { return images[13]; } }
			public static Image Help              { get { return images[14]; } }
			public static Image ZoomIn            { get { return images[15]; } }
			public static Image ZoomOut           { get { return images[16]; } }
			public static Image Back              { get { return images[17]; } }
			public static Image Forward           { get { return images[18]; } }
			public static Image Favorites         { get { return images[19]; } }
			public static Image AddToFavorites    { get { return images[20]; } }
			public static Image Stop              { get { return images[21]; } }
			public static Image Refresh           { get { return images[22]; } }
			public static Image Home              { get { return images[23]; } }
			public static Image Edit              { get { return images[24]; } }
			public static Image Tools             { get { return images[25]; } }
			public static Image Tiles             { get { return images[26]; } }
			public static Image Icons             { get { return images[27]; } }
			public static Image List              { get { return images[28]; } }
			public static Image Details           { get { return images[29]; } }
			public static Image Pane              { get { return images[30]; } }
			public static Image Culture           { get { return images[31]; } }
			public static Image Languages         { get { return images[32]; } }
			public static Image History           { get { return images[33]; } }
			public static Image Mail              { get { return images[34]; } }
			public static Image Parent            { get { return images[35]; } }
			public static Image FolderProperties  { get { return images[36]; } }
			public static Image Calendar          { get { return images[37]; } }
		}
	}
}
