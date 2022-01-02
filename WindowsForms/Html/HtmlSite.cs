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
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
	using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [ClassInterface(ClassInterfaceType.None)]
    internal class HtmlSite : NativeMethods.IOleClientSite, NativeMethods.IOleContainer, NativeMethods.IOleDocumentSite, NativeMethods.IOleInPlaceSite, NativeMethods.IOleInPlaceFrame, NativeMethods.IDocHostUIHandler, NativeMethods.IPropertyNotifySink, NativeMethods.IAdviseSink, NativeMethods.IOleServiceProvider
    {
        private HtmlControl hostControl;

        private NativeMethods.IOleObject oleObject;
        private NativeMethods.IHTMLDocument2 document;
        private NativeMethods.IOleDocumentView documentView;
		private NativeMethods.IOleCommandTarget commandTarget;
        private NativeMethods.IOleInPlaceActiveObject activeObject;

        private NativeMethods.ConnectionPointCookie propertyNotifySinkCookie;
        private int adviseSinkCookie;

		public HtmlSite(HtmlControl hostControl) 
        {
            if ((hostControl == null) || (!hostControl.IsHandleCreated)) 
            {
                throw new ArgumentException("hostControl");
            }

            this.hostControl = hostControl;
            this.hostControl.Resize += new EventHandler(this.HostControl_Resize);
        }

        public NativeMethods.IOleCommandTarget CommandTarget 
        {
            get { return this.commandTarget; }
        }

        public NativeMethods.IHTMLDocument2 Document 
        {
            get { return this.document; }
        }

        public void ActivateHtml() 
        {
            Debug.Assert(this.oleObject != null, "How'd we get here when trident is null!");

            try 
            {
                NativeMethods.COMRECT rect = new NativeMethods.COMRECT();
                NativeMethods.GetClientRect(this.hostControl.Handle, rect);
                this.oleObject.DoVerb(NativeMethods.OLEIVERB_UIACTIVATE, NativeMethods.NullIntPtr, (NativeMethods.IOleClientSite) this, 0, this.hostControl.Handle, rect);
            }
            catch (Exception exception) 
            {
                Debug.Fail(exception.ToString());
            }
        }

        public void CloseHtml() 
        {
            this.hostControl.Resize -= new EventHandler(this.HostControl_Resize);

            try 
            {
                if (this.propertyNotifySinkCookie != null) 
                {
                    this.propertyNotifySinkCookie.Disconnect();
                    this.propertyNotifySinkCookie = null;
                }

                if (this.document != null) 
                {
                    this.documentView = null;
                    this.document = null;
                    this.commandTarget = null;
                    this.activeObject = null;

                    if (adviseSinkCookie != 0) 
                    {
                        this.oleObject.Unadvise(adviseSinkCookie);
                        this.adviseSinkCookie = 0;
                    }

                    this.oleObject.Close(NativeMethods.OLECLOSE_NOSAVE);
                    this.oleObject.SetClientSite(null);
                    this.oleObject = null;
                }
            }
            catch (Exception exception) 
            {
                Debug.Fail(exception.ToString());
            }
        }

        public void CreateHtml() 
        {
            Debug.Assert(this.document == null, "Must call CloseHtml before recreating.");

            bool created = false;
            try 
            {
                // create the trident instance
                this.document = (NativeMethods.IHTMLDocument2) new NativeMethods.HTMLDocument();
                this.oleObject = (NativeMethods.IOleObject) this.document;

                // hand it our NativeMethods.IOleClientSite implementation
                this.oleObject.SetClientSite((NativeMethods.IOleClientSite)this);

                created = true;

                this.propertyNotifySinkCookie = new NativeMethods.ConnectionPointCookie(this.document, this, typeof(NativeMethods.IPropertyNotifySink), false);

                this.oleObject.Advise((NativeMethods.IAdviseSink)this, out adviseSinkCookie);
                Debug.Assert(adviseSinkCookie != 0);

                this.commandTarget = (NativeMethods.IOleCommandTarget) this.document;
            }
            finally 
            {
                if (created == false) 
                {
                    this.document = null;
                    this.oleObject = null;
                    this.commandTarget = null;
                }
            }
        }

        public void DeactivateHtml() 
        {
            // TODO
        }

        private void HostControl_Resize(object src, EventArgs e) 
        {
            if (this.documentView != null) 
            {
                NativeMethods.COMRECT r = new NativeMethods.COMRECT();

                NativeMethods.GetClientRect(hostControl.Handle, r);
                this.documentView.SetRect(r);
            }
        }

        private void OnReadyStateChanged() 
        {
            string readyState = this.document.GetReadyState();
            if (String.Compare(readyState, "complete", true) == 0) 
            {
                OnReadyStateComplete();
            }
        }

        private void OnReadyStateComplete() 
        {
            this.hostControl.OnReadyStateComplete(EventArgs.Empty);
        }

        internal void SetFocus() 
        {
            if (this.activeObject != null) 
            {
                IntPtr hWnd = IntPtr.Zero;
                if (this.activeObject.GetWindow(out hWnd) == NativeMethods.S_OK) 
                {
                    Debug.Assert(hWnd != IntPtr.Zero);
                    NativeMethods.SetFocus(hWnd);
                }
            }
        }

        public bool TranslateAccelarator(NativeMethods.COMMSG msg) 
        {
            if (this.activeObject != null) 
            {
                if (this.activeObject.TranslateAccelerator(msg) != NativeMethods.S_FALSE)
				{
                    return true;
                }
            }
            return false;
        }

        ///////////////////////////////////////////////////////////////////////////
        // NativeMethods.IOleClientSite Implementation

        int NativeMethods.IOleClientSite.SaveObject() 
        {
            return NativeMethods.S_OK;
        }

        int NativeMethods.IOleClientSite.GetMoniker(int dwAssign, int dwWhichMoniker, out object ppmk) 
        {
            ppmk = null;
            return NativeMethods.E_NOTIMPL;
        }

        int NativeMethods.IOleClientSite.GetContainer(out NativeMethods.IOleContainer ppContainer) 
        {
            ppContainer = (NativeMethods.IOleContainer)this;
            return NativeMethods.S_OK;
        }

        int NativeMethods.IOleClientSite.ShowObject() 
        {
            return NativeMethods.S_OK;
        }

        int NativeMethods.IOleClientSite.OnShowWindow(int fShow) 
        {
            return NativeMethods.S_OK;
        }

        int NativeMethods.IOleClientSite.RequestNewObjectLayout() 
        {
            return NativeMethods.S_OK;
        }

        ///////////////////////////////////////////////////////////////////////////
        // NativeMethods.IOleContainer Implementation

        void NativeMethods.IOleContainer.ParseDisplayName(object pbc, string pszDisplayName, int[] pchEaten, object[] ppmkOut) 
        {
            Debug.Fail("ParseDisplayName - " + pszDisplayName);
            throw new COMException(String.Empty, NativeMethods.E_NOTIMPL);
        }

        void NativeMethods.IOleContainer.EnumObjects(int grfFlags, object[] ppenum) 
        {
            throw new COMException(String.Empty, NativeMethods.E_NOTIMPL);
        }

        void NativeMethods.IOleContainer.LockContainer(int fLock) 
        {
        }

        ///////////////////////////////////////////////////////////////////////////
        // NativeMethods.IOleDocumentSite Implementation

        int NativeMethods.IOleDocumentSite.ActivateMe(NativeMethods.IOleDocumentView viewToActivate) 
        {
            Debug.Assert(viewToActivate != null, "Expected the view to be non-null");
			
			if (viewToActivate == null)
			{
				return NativeMethods.E_INVALIDARG;
			}

            NativeMethods.COMRECT rect = new NativeMethods.COMRECT();
            NativeMethods.GetClientRect(hostControl.Handle, rect);

            this.documentView = viewToActivate;
            this.documentView.SetInPlaceSite((NativeMethods.IOleInPlaceSite) this);
            this.documentView.UIActivate(1);
            this.documentView.SetRect(rect);
            this.documentView.Show(1);

            return NativeMethods.S_OK;
        }

        ///////////////////////////////////////////////////////////////////////////
        // NativeMethods.IOleInPlaceSite Implementation

		IntPtr NativeMethods.IOleInPlaceSite.GetWindow()
		{
			return hostControl.Handle;
		}

		void NativeMethods.IOleInPlaceSite.ContextSensitiveHelp(int fEnterMode)
		{
			throw new COMException(String.Empty, NativeMethods.E_NOTIMPL);
		}

        int NativeMethods.IOleInPlaceSite.CanInPlaceActivate() 
        {
            return NativeMethods.S_OK;
        }

        void NativeMethods.IOleInPlaceSite.OnInPlaceActivate() 
        {
        }

        void NativeMethods.IOleInPlaceSite.OnUIActivate() 
        {
        }

        void NativeMethods.IOleInPlaceSite.GetWindowContext(out NativeMethods.IOleInPlaceFrame ppFrame, out NativeMethods.IOleInPlaceUIWindow ppDoc, NativeMethods.COMRECT lprcPosRect, NativeMethods.COMRECT lprcClipRect, NativeMethods.tagOIFI lpFrameInfo) 
        {
            ppFrame = (NativeMethods.IOleInPlaceFrame)this;
            ppDoc = null;

            NativeMethods.GetClientRect(hostControl.Handle, lprcPosRect);
            NativeMethods.GetClientRect(hostControl.Handle, lprcClipRect);

            lpFrameInfo.cb = Marshal.SizeOf(typeof(NativeMethods.tagOIFI));
            lpFrameInfo.fMDIApp = 0;
            lpFrameInfo.hwndFrame = hostControl.Handle;
            lpFrameInfo.hAccel = NativeMethods.NullIntPtr;
            lpFrameInfo.cAccelEntries = 0;
        }

        int NativeMethods.IOleInPlaceSite.Scroll(NativeMethods.tagSIZE scrollExtant) 
        {
            return NativeMethods.E_NOTIMPL;
        }

        void NativeMethods.IOleInPlaceSite.OnUIDeactivate(int fUndoable) 
        {
            // NOTE, nikhilko, 7/99: Don't return E_NOTIMPL. Somehow doing nothing and returning S_OK fixes trident hosting in Win2000.
        }

        void NativeMethods.IOleInPlaceSite.OnInPlaceDeactivate() 
        {
        }

        void NativeMethods.IOleInPlaceSite.DiscardUndoState() 
        {
            throw new COMException(String.Empty, NativeMethods.E_NOTIMPL);
        }

        void NativeMethods.IOleInPlaceSite.DeactivateAndUndo() 
        {
        }

        int NativeMethods.IOleInPlaceSite.OnPosRectChange(NativeMethods.COMRECT lprcPosRect) 
        {
            return NativeMethods.S_OK;
        }

        ///////////////////////////////////////////////////////////////////////////
        // NativeMethods.IOleInPlaceFrame Implementation

		IntPtr NativeMethods.IOleInPlaceFrame.GetWindow()
		{
			return hostControl.Handle;
		}

		void NativeMethods.IOleInPlaceFrame.ContextSensitiveHelp(int fEnterMode)
		{
			throw new COMException(String.Empty, NativeMethods.E_NOTIMPL);
		}

        void NativeMethods.IOleInPlaceFrame.GetBorder(NativeMethods.COMRECT lprectBorder) 
        {
            throw new COMException(String.Empty, NativeMethods.E_NOTIMPL);
        }

        void NativeMethods.IOleInPlaceFrame.RequestBorderSpace(NativeMethods.COMRECT pborderwidths) 
        {
            throw new COMException(String.Empty, NativeMethods.E_NOTIMPL);
        }

        void NativeMethods.IOleInPlaceFrame.SetBorderSpace(NativeMethods.COMRECT pborderwidths) 
        {
            throw new COMException(String.Empty, NativeMethods.E_NOTIMPL);
        }

        void NativeMethods.IOleInPlaceFrame.SetActiveObject(NativeMethods.IOleInPlaceActiveObject pActiveObject, string pszObjName) 
        {
            this.activeObject = pActiveObject;
        }

        void NativeMethods.IOleInPlaceFrame.InsertMenus(IntPtr hmenuShared, NativeMethods.tagOleMenuGroupWidths lpMenuWidths) 
        {
            throw new COMException(String.Empty, NativeMethods.E_NOTIMPL);
        }

        void NativeMethods.IOleInPlaceFrame.SetMenu(IntPtr hmenuShared, IntPtr holemenu, IntPtr hwndActiveObject) 
        {
            throw new COMException(String.Empty, NativeMethods.E_NOTIMPL);
        }

        void NativeMethods.IOleInPlaceFrame.RemoveMenus(IntPtr hmenuShared) 
        {
            throw new COMException(String.Empty, NativeMethods.E_NOTIMPL);
        }

        void NativeMethods.IOleInPlaceFrame.SetStatusText(string pszStatusText) 
        {
        }

        void NativeMethods.IOleInPlaceFrame.EnableModeless(int fEnable) 
        {
        }

        int NativeMethods.IOleInPlaceFrame.TranslateAccelerator(NativeMethods.COMMSG lpmsg, short wID) 
        {
            return NativeMethods.S_FALSE;
        }

        ///////////////////////////////////////////////////////////////////////////
        // IDocHostUIHandler Implementation

        int NativeMethods.IDocHostUIHandler.ShowContextMenu(int dwID, NativeMethods.POINT pt, object pcmdtReserved, object pdispReserved) 
        {
//			if (this.hostControl != null)
//			{
//				Point location = this.hostControl.PointToClient(new Point(pt.x, pt.y));
//				this.hostControl.ContextMenu.Show(this.hostControl, location);
//			}

			return NativeMethods.S_OK;
        }

        int NativeMethods.IDocHostUIHandler.GetHostInfo(NativeMethods.DOCHOSTUIINFO info) 
        {
            info.dwDoubleClick = NativeMethods.DOCHOSTUIDBLCLICK_DEFAULT;

			int flags = 0;

			/*
			if (this.hostControl.AllowInPlaceNavigation) 
            {
                flags |= NativeMethods.DOCHOSTUIFLAG_ENABLE_INPLACE_NAVIGATION;
            }
			*/
            
            if (!this.hostControl.ScriptEnabled) 
            {
                flags |= NativeMethods.DOCHOSTUIFLAG_DISABLE_SCRIPT_INACTIVE;
            }
                       
			info.dwFlags = flags;
            return NativeMethods.S_OK;
        }

        int NativeMethods.IDocHostUIHandler.EnableModeless(bool fEnable) 
        {
            return NativeMethods.S_OK;
        }

        int NativeMethods.IDocHostUIHandler.ShowUI(int dwID, NativeMethods.IOleInPlaceActiveObject activeObject, NativeMethods.IOleCommandTarget commandTarget, NativeMethods.IOleInPlaceFrame frame, NativeMethods.IOleInPlaceUIWindow doc) 
        {
            return NativeMethods.S_OK;
        }

        int NativeMethods.IDocHostUIHandler.HideUI() 
        {
            return NativeMethods.S_OK;
        }

        int NativeMethods.IDocHostUIHandler.UpdateUI() 
        {
            return NativeMethods.S_OK;
        }

        int NativeMethods.IDocHostUIHandler.OnDocWindowActivate(bool fActivate) 
        {
            return NativeMethods.E_NOTIMPL;
        }

        int NativeMethods.IDocHostUIHandler.OnFrameWindowActivate(bool fActivate) 
        {
            return NativeMethods.E_NOTIMPL;
        }

        int NativeMethods.IDocHostUIHandler.ResizeBorder(NativeMethods.COMRECT rect, NativeMethods.IOleInPlaceUIWindow doc, bool fFrameWindow) 
        {
            return NativeMethods.E_NOTIMPL;
        }

        int NativeMethods.IDocHostUIHandler.GetOptionKeyPath(string[] pbstrKey, int dw) 
        {
            pbstrKey[0] = null;
            return NativeMethods.S_OK;
        }

        int NativeMethods.IDocHostUIHandler.GetDropTarget(NativeMethods.IOleDropTarget pDropTarget, out NativeMethods.IOleDropTarget ppDropTarget) 
        {
			ppDropTarget = new DropTarget(new DataObjectConverter(), pDropTarget);
			return (ppDropTarget != null) ? NativeMethods.S_OK : NativeMethods.E_NOTIMPL;
        }

        int NativeMethods.IDocHostUIHandler.GetExternal(out object ppDispatch) 
        {
            ppDispatch = hostControl.ScriptObject;
            if (ppDispatch != null) 
            {
                return NativeMethods.S_OK;
            }
            else 
            {
                return NativeMethods.E_NOTIMPL;
            }
        }

        int NativeMethods.IDocHostUIHandler.TranslateAccelerator(NativeMethods.COMMSG msg, ref Guid group, int nCmdID) 
        {
            return NativeMethods.S_FALSE;
        }

        int NativeMethods.IDocHostUIHandler.TranslateUrl(int dwTranslate, string strURLIn, out string pstrURLOut) 
        {
            pstrURLOut = null;
            return NativeMethods.E_NOTIMPL;
        }

        int NativeMethods.IDocHostUIHandler.FilterDataObject(NativeMethods.IOleDataObject pDO, out NativeMethods.IOleDataObject ppDORet) 
        {
            ppDORet = null;
            return NativeMethods.E_NOTIMPL;
        }

        ///////////////////////////////////////////////////////////////////////////
        // IPropertyNotifySink Implementation

        void NativeMethods.IPropertyNotifySink.OnChanged(int dispID) 
        {
            if (dispID == NativeMethods.DISPID_READYSTATE) 
            {
                this.OnReadyStateChanged();
            }
        }

        void NativeMethods.IPropertyNotifySink.OnRequestEdit(int dispID) 
        {
        }

        ///////////////////////////////////////////////////////////////////////////
        // IAdviseSink Implementation

        void NativeMethods.IAdviseSink.OnDataChange(NativeMethods.FORMATETC pFormat, NativeMethods.STGMEDIUM pStg) 
        {
        }

        void NativeMethods.IAdviseSink.OnViewChange(int dwAspect, int index) 
        {
        }

        void NativeMethods.IAdviseSink.OnRename(object pmk) 
        {
        }

        void NativeMethods.IAdviseSink.OnSave() 
        {
        }

        void NativeMethods.IAdviseSink.OnClose() 
        {
        }

        ///////////////////////////////////////////////////////////////////////////
        // NativeMethods.IOleServiceProvider

        int NativeMethods.IOleServiceProvider.QueryService(ref Guid sid, ref Guid iid, out IntPtr ppvObject) 
        {
            int hr = NativeMethods.E_NOINTERFACE;
            ppvObject = NativeMethods.NullIntPtr;

            //            object service = hostControl.GetService(ref sid);
            //            if (service != null) {
            //                if (iid.Equals(NativeMethods.IID_IUnknown)) {
            //                    ppvObject = Marshal.GetIUnknownForObject(service);
            //                }
            //                else {
            //                    IntPtr pUnk = Marshal.GetIUnknownForObject(service);
            //
            //                    hr = Marshal.QueryInterface(pUnk, ref iid, out ppvObject);
            //                    Marshal.Release(pUnk);
            //                }
            //            }

            return hr;
        }

		// TODO
		internal enum ConverterInfo 
		{
			No = 0,
			Yes = 1,
			Unknown = 2
		}

		// TODO
		internal class DataObjectConverter
		{
			public ConverterInfo CanConvertToHtml(IDataObject dataObject)
			{
				if (dataObject.GetDataPresent("FileDrop"))
				{
					return ConverterInfo.Yes;
				}

				return ConverterInfo.Unknown;
			}

			public bool ConvertToHtml(IDataObject originalDataObject, DataObject newDataObject)
			{
				if (originalDataObject.GetDataPresent("FileDrop"))
				{
					string[] fileNames = (string[]) originalDataObject.GetData("FileDrop");
					if (fileNames.Length == 1)
					{
						StreamReader reader = new StreamReader(fileNames[0]);
						string html = reader.ReadToEnd();
						reader.Close();

						newDataObject.SetData("HTML", html);
					}

					return true;
				}

				return false;
			}
		}
	
		private sealed class DropTarget : NativeMethods.IOleDropTarget
		{
			private DataObject currentDataObj;
			private IntPtr currentDataObjPtr;
			private NativeMethods.IOleDropTarget originalDropTarget;
			private DataObjectConverter converter;
			private ConverterInfo converterInfo;

			public DropTarget(DataObjectConverter converter, NativeMethods.IOleDropTarget originalDropTarget)
			{
				this.converter = converter;
				this.originalDropTarget = originalDropTarget;
			}

			public int OleDragEnter(IntPtr pDataObj, int grfKeyState, long pt, ref int pdwEffect)
			{
				object nativeDataObject = Marshal.GetObjectForIUnknown(pDataObj);
				DataObject dataObject = new DataObject(nativeDataObject);

				this.converterInfo = this.converter.CanConvertToHtml(dataObject);
				if (this.converterInfo == ConverterInfo.Yes)
				{
					this.currentDataObj = new DataObject(DataFormats.Html, String.Empty);

					IntPtr pUnk = Marshal.GetIUnknownForObject(this.currentDataObj);

					// Get the IOleDataObject interface for the object
					Guid guid = new Guid("0000010E-0000-0000-C000-000000000046");
					Marshal.QueryInterface(pUnk, ref guid, out this.currentDataObjPtr);
					Marshal.Release(pUnk);

					return this.originalDropTarget.OleDragEnter(this.currentDataObjPtr, grfKeyState, pt, ref pdwEffect);
				}
				else if (this.converterInfo == ConverterInfo.No)
				{
					// ignore this drag/drop
					pdwEffect = 0;
				}
				else if (this.converterInfo == ConverterInfo.Unknown)
				{
					// If the converter doesn't know anything about this data object, just use the original data object
					return this.originalDropTarget.OleDragEnter(pDataObj, grfKeyState, pt, ref pdwEffect);
				}
				else
				{
					Debug.Fail("Unknown ConverterInfo value!");
				}
				return NativeMethods.S_OK;
			}

			public int OleDragOver(int grfKeyState, long pt, ref int pdwEffect)
			{
				if (this.converterInfo != ConverterInfo.No)
				{
					return this.originalDropTarget.OleDragOver(grfKeyState, pt, ref pdwEffect);
				}
				else
				{
					pdwEffect = 0;
					return NativeMethods.S_OK;
				}
			}

			public int OleDragLeave()
			{
				this.converterInfo = ConverterInfo.No;

				if (this.currentDataObj != null)
				{
					this.currentDataObj = null;
					Marshal.Release(this.currentDataObjPtr);
					this.currentDataObjPtr = IntPtr.Zero;

					return this.originalDropTarget.OleDragLeave();
				}

				// If the DataObject was never set (meaning we cancelled the drag/drop), don't pass on the DragLeave since
				// we never passed in the DragEnter
				return NativeMethods.S_OK;
			}

			public int OleDrop(IntPtr pDataObj, int grfKeyState, long pt, ref int pdwEffect)
			{
				int hr = NativeMethods.S_OK;
				if (this.converterInfo == ConverterInfo.Yes)
				{
					object nativeDataObject = Marshal.GetObjectForIUnknown(pDataObj);
					DataObject dataObject = new DataObject(nativeDataObject);

					bool mapped = this.converter.ConvertToHtml(dataObject, this.currentDataObj);
					if (mapped)
					{
						IntPtr pUnk = Marshal.GetIUnknownForObject(this.currentDataObj);

						// Get the IOleDataObject interface for the object
						Guid guid = new Guid("0000010E-0000-0000-C000-000000000046");
						Marshal.QueryInterface(pUnk, ref guid, out this.currentDataObjPtr);

						hr = this.originalDropTarget.OleDrop(this.currentDataObjPtr, grfKeyState, pt, ref pdwEffect);

						this.currentDataObj = null;
						this.currentDataObjPtr = IntPtr.Zero;
						Marshal.Release(this.currentDataObjPtr);

					}
					else
					{
						pdwEffect = 0;
					}
				}
				else if (this.converterInfo == ConverterInfo.Unknown)
				{
					hr = this.originalDropTarget.OleDrop(pDataObj, grfKeyState, pt, ref pdwEffect);
				}

				this.converterInfo = ConverterInfo.No;

				return hr;
			}
		}
	}
}

