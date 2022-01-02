// ------------------------------------------------------------
// Writer, WYSIWYG editor for HTML
// Copyright (c) 2002-2003 Lutz Roeder. All rights reserved.
// http://www.aisto.com/roeder
// ------------------------------------------------------------
// Based on HTML editor control code
// Copyright (c) 2002-2003 Nikhil Kothari. All rights reserved.
// http://www.nikhilk.net
// ------------------------------------------------------------
namespace Writer.Html
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
	using System.Runtime.InteropServices;
	using System.Security;
	using System.Security.Permissions;
	using System.Text;
    using System.Windows;
    using System.Windows.Forms;

    public class HtmlControl : Control
    {
		private static readonly object readyStateCompleteEvent = new object();

		private bool scriptEnabled;

        private bool firstActivation;
        private bool isReady;
        private bool isCreated;

        // These allow a user to load the document before displaying
        private bool desiredLoad;
        private string desiredContent;
        private string desiredUrl;

        private bool focusDesired;

        private string url;
        private object scriptObject;

        private HtmlSite site;

        private static IDictionary urlMap;

		private bool isDesignMode;
		private bool designModeDesired;

		private NativeMethods.IPersistStreamInit persistStream;

        public HtmlControl()
        {
            this.firstActivation = true;
			this.TabStop = true;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.url != null)
				{
					UrlMap[this.url] = null;
				}
			}

			base.Dispose(disposing);
		}

		public event EventHandler ReadyStateComplete
		{
			add { this.Events.AddHandler(readyStateCompleteEvent, value); }
			remove { this.Events.RemoveHandler(readyStateCompleteEvent, value); }
		}

		protected bool IsCreated 
        {
            get { return this.isCreated; }
        }

        public bool IsReady 
        {
            get { return this.isReady; }
        }

		internal NativeMethods.IHTMLDocument2 HtmlDocument
		{
			get { return this.site.Document; }
		}

        internal NativeMethods.IOleCommandTarget CommandTarget 
        {
            get { return this.site.CommandTarget; }
        }

        public bool ScriptEnabled 
        {
            get { return this.scriptEnabled; }
            set { this.scriptEnabled = value; }
        }

        public object ScriptObject 
        {
            get { return this.scriptObject; }
            set { this.scriptObject = value; }
        }

        public virtual string Url 
        {
            get { return this.url; }
        }

        internal static IDictionary UrlMap 
        {
            get 
            {
                if (urlMap == null) 
                {
                    urlMap = new HybridDictionary(true);
                }

				return urlMap;
            }
        }

        /// <summary>Executes the specified command in MSHTML.</summary>
        /// <param name="command"></param>
        internal object Execute(int command) 
        {
            return this.Execute(command, null);
        }

        internal object Execute(int command, object[] arguments) 
        {
			object[] retVal = new object[] { null };
            int hr = CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, command, NativeMethods.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, arguments, retVal);
            if (hr != NativeMethods.S_OK) 
            {
                throw new Exception("Execution of MSHTML command ID \'" + command + "\' failed.");
            }
			return retVal[0];
        }

		internal object ExecuteWithUserInterface(int command, object[] arguments)
		{
			object[] retVal = new object[] { null };
			int hr = CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, command, NativeMethods.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER, arguments, retVal);
			if (hr != NativeMethods.S_OK)
			{
				throw new Exception("Execution of MSHTML command ID \'" + command + "\' failed.");
			}
			return retVal[0];
		}

		internal bool IsEnabled(int commandId)
		{
			return ((this.GetCommandInfo(commandId) & 1) != 0);
		}

		internal bool IsChecked(int commandId)
		{
			return ((this.GetCommandInfo(commandId) & 2) != 0);
		}

		internal int GetCommandInfo(int commandId)
		{
			NativeMethods.tagOLECMD command = new NativeMethods.tagOLECMD();
			command.cmdID = commandId;
			this.CommandTarget.QueryStatus(ref NativeMethods.Guid_MSHTML, 1, command, 0);
			return (command.cmdf >> 1);
		}

        public HtmlElement GetElementByID(string id) 
        {
            NativeMethods.IHTMLElement body = this.site.Document.GetBody();
            NativeMethods.IHTMLElementCollection children = (NativeMethods.IHTMLElementCollection)body.GetAll();
            NativeMethods.IHTMLElement element = (NativeMethods.IHTMLElement)children.Item(id, 0);

            if (element == null) 
            {
                return null;
            }

            return new HtmlElement(element, this);
        }

        /// <summary>Loads HTML content from a stream into this control.</summary>
        /// <param name="stream"></param>
        public void LoadHtml(Stream stream) 
        {
            if (stream == null) 
            {
                throw new ArgumentNullException("LoadHtml : You must specify a non-null stream for content");
            }

            StreamReader reader = new StreamReader(stream);
            this.LoadHtml(reader.ReadToEnd());
        }

        /// <summary>Loads HTML content from a string into this control.</summary>
        /// <param name="content"></param>
        public void LoadHtml(string content) 
        {
            this.LoadHtml(content, null);
        }

		/*
        public void LoadHtml(string content, string url) 
        {
            this.LoadHtml(content, url, null);
        }
        */

        // REVIEW: Add a load method for stream and url

        /// <summary>
        /// Loads HTML content from a string into this control identified by the specified URL.
        /// If MSHTML has not yet been created, the loading is postponed until MSHTML has been created.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="url"></param>
        public void LoadHtml(string content, string url) 
        {
            if (content == null) 
            {
                content = "";
            }

            if (!this.isCreated) 
            {
                this.desiredContent = content;
                this.desiredUrl = url;
                this.desiredLoad = true;
                return;
            }

            NativeMethods.IStream stream = null;

			// added by Erik Frey - UTF preamble
			byte[] preamble = UnicodeEncoding.Unicode.GetPreamble();
			string byteOrderMark = UnicodeEncoding.Unicode.GetString(preamble, 0, preamble.Length);
			// i guess .NET 2.0 fixed up unicode string handling
			if (System.Environment.Version.Major > 1 || !content.StartsWith(byteOrderMark))
				content = byteOrderMark + content;

            //First we create a COM stream
            IntPtr hglobal = Marshal.StringToHGlobalUni(content);
            NativeMethods.CreateStreamOnHGlobal(hglobal, true, out stream);

            // Initialize a new document if there is nothing to load
            if (stream == null) 
            {
                NativeMethods.IPersistStreamInit psi = (NativeMethods.IPersistStreamInit) this.site.Document;
                Debug.Assert(psi != null, "Expected IPersistStreamInit");
                psi.InitNew();
                psi = null;
            }
            else 
            {
                NativeMethods.IHTMLDocument2 document = this.site.Document;

                if (url == null) 
                {
					// If there is no specified URL load the document from the stream.
					NativeMethods.IPersistStreamInit psi = (NativeMethods.IPersistStreamInit)document;
                    Debug.Assert(psi != null, "Expected IPersistStreamInit");
                    psi.Load(stream);
                    psi = null;
                }
                else 
                {
                    // Otherwise we create a moniker and load the stream to that moniker.
					NativeMethods.IPersistMoniker persistMoniker = (NativeMethods.IPersistMoniker) document;

					NativeMethods.IMoniker moniker = null;
					NativeMethods.CreateURLMoniker(null, url, out moniker);

					NativeMethods.IBindCtx bindContext = null;
					NativeMethods.CreateBindCtx(0, out bindContext);

					persistMoniker.Load(1, moniker, bindContext, 0);

					persistMoniker = null;
					moniker = null;
					bindContext = null;
                }
            }

            this.url = url;
        }

        /// <summary>
        /// Allow editors to perform actions when the MSHTML document is created
        /// and before it's activated
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCreated(EventArgs e) 
        {
			if (e == null)
			{
				throw new ArgumentNullException("You must specify a non-null EventArgs for OnCreated");
			}

			object[] mshtmlArgs = new object[1];

			mshtmlArgs[0] = true;
			CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, NativeMethods.IDM_PERSISTDEFAULTVALUES, 0, mshtmlArgs, null);

			mshtmlArgs[0] = true;
			CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, NativeMethods.IDM_PROTECTMETATAGS, 0, mshtmlArgs, null);

			mshtmlArgs[0] = true;
			CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, NativeMethods.IDM_PRESERVEUNDOALWAYS, 0, mshtmlArgs, null);

			mshtmlArgs[0] = true;
			CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, NativeMethods.IDM_NOACTIVATENORMALOLECONTROLS, 0, mshtmlArgs, null);

			mshtmlArgs[0] = true;
			CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, NativeMethods.IDM_NOACTIVATEDESIGNTIMECONTROLS, 0, mshtmlArgs, null);

			mshtmlArgs[0] = true;
			CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, NativeMethods.IDM_NOACTIVATEJAVAAPPLETS, 0, mshtmlArgs, null);

			mshtmlArgs[0] = true;
			CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, NativeMethods.IDM_NOFIXUPURLSONPASTE, 0, mshtmlArgs, null);

			// Set the design mode to the last desired design mode
			if (this.designModeDesired)
			{
				this.IsDesignMode = this.designModeDesired;
				this.designModeDesired = false;
			}
        }

        protected override void OnGotFocus(EventArgs e) 
        {
            base.OnGotFocus(e);

            if (this.IsReady) 
            {
				this.site.SetFocus();
            }
            else 
            {
				this.focusDesired = true;
            }
        }

        /// <summary>We can only activate the MSHTML after our handle has been created, so upon creating the handle, we create and activate NativeMethods. If LoadHtml was called prior to this, we do the loading now.</summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (this.firstActivation)
            {
                this.site = new HtmlSite(this);
                this.site.CreateHtml();

                this.isCreated = true;

                this.OnCreated(EventArgs.Empty);

                this.site.ActivateHtml();
                this.firstActivation = false;

                if (this.desiredLoad)
                {
                    this.LoadHtml(this.desiredContent, this.desiredUrl);
                    this.desiredLoad = false;
                }
            }
        }

		protected override void OnHandleDestroyed(EventArgs e)
		{
			this.site.DeactivateHtml();
			this.site.CloseHtml();
			this.site = null;

			base.OnHandleDestroyed(e);
		}

        /// <summary>Called when the control has just become ready.</summary>
        /// <param name="e"></param>
        protected internal virtual void OnReadyStateComplete(EventArgs e) 
        {
            this.isReady = true;

            EventHandler handler = (EventHandler)this.Events[readyStateCompleteEvent];
            if (handler != null) 
            {
                handler(this, e);
            }

            if (this.focusDesired) 
            {
                this.focusDesired = false;
                this.site.ActivateHtml();
                this.site.SetFocus();
            }

			// HtmlEditor

			this.persistStream = (NativeMethods.IPersistStreamInit)this.HtmlDocument;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)] 
        public override bool PreProcessMessage(ref Message m) 
        {
            bool handled = false;
            if ((m.Msg >= NativeMethods.WM_KEYFIRST) && (m.Msg <= NativeMethods.WM_KEYLAST)) 
            {
                // If it's a key down, first see if the key combo is a command key
                if (m.Msg == NativeMethods.WM_KEYDOWN) 
                {
                    handled = ProcessCmdKey(ref m, (Keys)(int) m.WParam | ModifierKeys);
                }

                if (!handled) 
                {
                    int keyCode = (int)m.WParam;

                    // Don't let Trident eat Ctrl-PgUp/PgDn
					if (((keyCode != (int) Keys.PageUp) && (keyCode != (int) Keys.PageDown)) || ((ModifierKeys & Keys.Control) == 0)) 
                    {
                        NativeMethods.COMMSG cm = new NativeMethods.COMMSG();
                        cm.hwnd = m.HWnd;
                        cm.message = m.Msg;
                        cm.wParam = m.WParam;
                        cm.lParam = m.LParam;

						handled = this.site.TranslateAccelarator(cm);
                    }
                    else 
                    {
                        // WndProc for Ctrl-PgUp/PgDn is never called so call it directly here
                        this.WndProc(ref m);
                        handled = true;
                    }
                }
            }

            if (!handled) 
            {
                handled = base.PreProcessMessage(ref m);
            }

            return handled;
        }

        /// <summary>Saves the HTML contained in control to a string and return it.</summary>
        /// <returns>string - The HTML in the control</returns>
        public string SaveHtml() 
        {
            if (!this.IsCreated) 
            {
                throw new Exception("HtmlControl.SaveHtml : No HTML to save!");
            }

            string content = String.Empty;

            try 
            {
                NativeMethods.IHTMLDocument2 document = this.site.Document;

                // First save the document to a stream
                NativeMethods.IPersistStreamInit psi = (NativeMethods.IPersistStreamInit)document;
                Debug.Assert(psi != null, "Expected IPersistStreamInit");

                NativeMethods.IStream stream = null;
                NativeMethods.CreateStreamOnHGlobal(NativeMethods.NullIntPtr, true, out stream);

                psi.Save(stream, 1);

                // Now copy the stream to the string
                NativeMethods.STATSTG stat = new NativeMethods.STATSTG();
                stream.Stat(stat, 1);
                int length = (int)stat.cbSize;
                byte[] bytes = new byte[length];

                IntPtr hglobal;
                NativeMethods.GetHGlobalFromStream(stream, out hglobal);
                Debug.Assert(hglobal != NativeMethods.NullIntPtr, "Failed in GetHGlobalFromStream");

                // First copy the stream to a byte array
                IntPtr pointer = NativeMethods.GlobalLock(hglobal);
                if (pointer != NativeMethods.NullIntPtr) 
                {
                    Marshal.Copy(pointer, bytes, 0, length);

                    NativeMethods.GlobalUnlock(hglobal);

                    // Then create the string from the byte array (use a StreamReader to eat the preamble in the UTF8 encoding case)
                    StreamReader streamReader = null;
                    try 
                    {
                        streamReader = new StreamReader(new MemoryStream(bytes), Encoding.Default);
                        content = streamReader.ReadToEnd();
                    }
                    finally 
                    {
                        if (streamReader != null) 
                        {
                            streamReader.Close();
                        }
                    }
                }
            }
            catch (Exception exception) 
            {
                Debug.Fail("HtmlControl.SaveHtml" + exception.ToString());
                content = String.Empty;
            }
            finally 
            {
            }

            if (content == null) 
            {
                content = String.Empty;
            }
            
            return content;

			/*
            HtmlFormatter formatter = new HtmlFormatter();
            StringWriter writer = new StringWriter();
            formatter.Format(content, writer);
            return writer.ToString();
			*/
        }

        /// <summary>Saves the HTML contained in the control to a stream.</summary>
        /// <param name="stream"></param>
        public void SaveHtml(Stream stream) 
        {
            if (stream == null) 
            {
                throw new ArgumentNullException("SaveHtml : Must specify a non-null stream to which to save");
            }

            string content = SaveHtml();

            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
            writer.Write(content);
            writer.Flush();
		}

		public bool CanPrint
		{
			get { return this.IsEnabled(NativeMethods.IDM_PRINT); }	
		}
	
		public void Print()
		{
			this.ExecuteWithUserInterface(NativeMethods.IDM_PRINT, null);
		}

		public bool CanPrintPreview
		{
			get { return this.IsEnabled(NativeMethods.IDM_PRINTPREVIEW); }	
		}
		
		public void PrintPreview()
		{
			this.ExecuteWithUserInterface(NativeMethods.IDM_PRINTPREVIEW, null);
		}

		public bool CanCopy
		{
			get { return this.IsEnabled(NativeMethods.IDM_COPY); }
		}

		public void Copy()
		{
			if (!this.CanCopy)
			{
				throw new InvalidOperationException();
			}

			this.Execute(NativeMethods.IDM_COPY);
		}

		public bool CanSelectAll
		{
			get { return this.IsEnabled(NativeMethods.IDM_SELECTALL); }
		}

		public void SelectAll()
		{
			this.Execute(NativeMethods.IDM_SELECTALL);
		}

		public bool IsDesignMode
		{
			get { return this.isDesignMode; }

			set
			{
				// Only execute this if we aren't already in design mode
				if (this.isDesignMode != value)
				{
					// If the control isn't ready to be put into design mode,
					// set a flag and put it in design mode when it is ready
					if (!IsCreated)
					{
						this.designModeDesired = value;
					}
					else
					{
						//Turn design mode on or off depending on the new value
						this.isDesignMode = value;
						this.HtmlDocument.SetDesignMode((this.isDesignMode ? "on" : "off"));
					}
				}
			}
		}
	}
}
