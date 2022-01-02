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
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Text;

    [System.Security.SuppressUnmanagedCodeSecurityAttribute()]
    public sealed class NativeMethods
    {

        private NativeMethods() 
        {
        }

		public const int WM_MOUSEMOVE = 0x200;
		public const int WM_LBUTTONDOWN = 0x201;

        #region Static constants
        public static Guid ElementBehaviorFactory = new Guid("3050f429-98b5-11cf-bb82-00aa00bdce0b");
        public static Guid Guid_MSHTML = new Guid("DE4BA900-59CA-11CF-9592-444553540000");
        public static Guid IID_IUnknown = new Guid("{00000000-0000-0000-C000-000000000046}");
        public static IntPtr NullIntPtr = ((IntPtr)((int)(0)));
        #endregion

        #region Imported constants
        public const int
            BEHAVIOR_EVENT_APPLYSTYLE = 0x2,
            BEHAVIOR_EVENT_CONTENTREADY = 0x0,
            BEHAVIOR_EVENT_CONTENTSAVE = 0x4,
            BEHAVIOR_EVENT_DOCUMENTCONTEXTCHANGE = 0x3,
            BEHAVIOR_EVENT_DOCUMENTREADY = 0x1,

            DISPID_HTMLELEMENTEVENTS_ONDBLCLICK = unchecked((int)0xFFFFFDA7),
            DISPID_HTMLELEMENTEVENTS_ONDRAGSTART = unchecked((int)0x8001000B),
            DISPID_HTMLELEMENTEVENTS_ONDRAG = unchecked((int)0x80010014),
            DISPID_HTMLELEMENTEVENTS_ONMOUSEDOWN = unchecked((int)0xFFFFFDA3),
            DISPID_HTMLELEMENTEVENTS_ONMOUSEUP = unchecked((int)0xFFFFFDA1),
            DISPID_HTMLELEMENTEVENTS_ONMOUSEMOVE = -606,
            DISPID_HTMLELEMENTEVENTS_ONMOVE = unchecked((int)0x40B),
            DISPID_HTMLELEMENTEVENTS_ONMOVESTART = unchecked((int)0x40E),
            DISPID_HTMLELEMENTEVENTS_ONMOVEEND = unchecked((int)0x40F),
            DISPID_HTMLELEMENTEVENTS_ONRESIZESTART = unchecked((int)0x410),
            DISPID_HTMLELEMENTEVENTS_ONRESIZEEND = unchecked((int)0x411),
            DISPID_READYSTATE = unchecked((int)0xFFFFFDF3),
            DISPID_XOBJ_MIN = unchecked((int)0x80010000),
            DISPID_XOBJ_MAX = unchecked((int)0x8001FFFF),
            DISPID_XOBJ_BASE = DISPID_XOBJ_MIN,

            DOCHOSTUIDBLCLICK_DEFAULT = 0x0,
            DOCHOSTUIDBLCLICK_SHOWCODE = 0x2,
            DOCHOSTUIDBLCLICK_SHOWPROPERTIES = 0x1,

            DOCHOSTUIFLAG_ACTIVATE_CLIENTHIT_ONLY = 0x200,
            DOCHOSTUIFLAG_DIALOG = 0x1,
            DOCHOSTUIFLAG_DISABLE_COOKIE = 0x400,
            DOCHOSTUIFLAG_DISABLE_HELP_MENU = 0x2,
            DOCHOSTUIFLAG_DISABLE_OFFSCREEN = 0x40,
            DOCHOSTUIFLAG_DISABLE_SCRIPT_INACTIVE = 0x10,
            DOCHOSTUIFLAG_DIV_BLOCKDEFAULT = 0x100,
            DOCHOSTUIFLAG_FLAT_SCROLLBAR = 0x80,
            DOCHOSTUIFLAG_NO3DBORDER = 0x4,
            DOCHOSTUIFLAG_OPENNEWWIN = 0x20,
            DOCHOSTUIFLAG_SCROLL_NO = 0x8,
            DOCHOSTUIFLAG_ENABLE_INPLACE_NAVIGATION = 0x00010000,

            DROPEFFECT_NONE = 0,
            DROPEFFECT_COPY = 1,
            DROPEFFECT_MOVE = 2,
            DROPEFFECT_LINK = 4,

            E_ABORT = unchecked((int)0x80004004),
            E_ACCESSDENIED = unchecked((int)0x80070005),
            E_FAIL = unchecked((int)0x80004005),
            E_HANDLE = unchecked((int)0x80070006),
            E_INVALIDARG = unchecked((int)0x80070057),
            E_POINTER = unchecked((int)0x80004003),
            E_NOTIMPL = unchecked((int)0x80004001),
            E_NOINTERFACE = unchecked((int)0x80004002),
            E_OUTOFMEMORY = unchecked((int)0x8007000E),
            E_UNEXPECTED = unchecked((int)0x8000FFFF),

            ELEMENTDESCRIPTOR_FLAGS_LITERAL = 1,
            ELEMENTDESCRIPTOR_FLAGS_NESTED_LITERAL = 2,

            ELEMENTNAMESPACE_FLAGS_ALLOWANYTAG = 1,
            ELEMENTNAMESPACE_FLAGS_QUERYFORUNKNOWNTAGS = 2,

            ELEMENT_CORNER_BOTTOM = 3,
            ELEMENT_CORNER_BOTTOMLEFT = 7,
            ELEMENT_CORNER_BOTTOMRIGHT = 8,
            ELEMENT_CORNER_LEFT = 2,
            ELEMENT_CORNER_NONE = 0,
            ELEMENT_CORNER_RIGHT = 4,
            ELEMENT_CORNER_TOP = 1,
            ELEMENT_CORNER_TOPLEFT = 5,
            ELEMENT_CORNER_TOPRIGHT = 6,

            HTMLPAINTER_3DSURFACE = 0x200,
            HTMLPAINTER_ALPHA = 0x4,
            HTMLPAINTER_COMPLEX = 0x8,
            HTMLPAINTER_OPAQUE = 0x1,
            HTMLPAINTER_OVERLAY = 0x10,
            HTMLPAINTER_HITTEST = 0x20,
            HTMLPAINTER_NOBAND = 0x400,
            HTMLPAINTER_NODC = 0x1000,
            HTMLPAINTER_NOPHYSICALCLIP = 0x2000,
            HTMLPAINTER_NOSAVEDC = 0x4000,
            HTMLPAINTER_SURFACE = 0x100,
            HTMLPAINTER_TRANSPARENT = 0x2,

            HTMLPAINT_ZORDER_ABOVE_CONTENT = 7,
            HTMLPAINT_ZORDER_ABOVE_FLOW = 6,
            HTMLPAINT_ZORDER_BELOW_CONTENT = 4,
            HTMLPAINT_ZORDER_BELOW_FLOW = 5,
            HTMLPAINT_ZORDER_NONE = 0,
            HTMLPAINT_ZORDER_REPLACE_ALL = 1,
            HTMLPAINT_ZORDER_REPLACE_CONTENT = 2,
            HTMLPAINT_ZORDER_REPLACE_BACKGROUND = 3,
            HTMLPAINT_ZORDER_WINDOW_TOP = 8,

            IDM_COPY = 15,
            IDM_CUT = 16,
            IDM_DELETE = 17,
            IDM_FONTNAME = 18,
            IDM_FONTSIZE = 19,
            IDM_PASTE = 26,
			IDM_PRINT = 27,
            IDM_REDO = 29,
            IDM_SELECTALL = 31,
            IDM_UNDO = 43,
            IDM_BACKCOLOR = 51,
            IDM_BOLD = 52,
            IDM_ITALIC = 56,
            IDM_JUSTIFYCENTER = 57,
            IDM_JUSTIFYLEFT = 59,
            IDM_JUSTIFYRIGHT = 60,
            IDM_UNDERLINE = 63,
            IDM_STRIKETHROUGH = 91,
			IDM_PRINTPREVIEW = 2003,
            IDM_1D_ELEMENT = 2396,
            IDM_2D_ELEMENT = 2395,
            IDM_2D_POSITION = 2394,
            IDM_ABSOLUTE_POSITION = 2397,
            IDM_ADDTOGLYPHTABLE = 2337,
            IDM_ATOMICSELECTION = 2399,
            IDM_BLOCKFMT = 2234,
			IDM_CHECKBOX = 2163,
            IDM_BUTTON = 2167,
			IDM_CLEARSELECTION = 2007,
            IDM_CSSEDITING_LEVEL = 2406,
            IDM_DROPDOWNBOX = 2165,
            IDM_EMPTYGLYPHTABLE = 2336,
            IDM_FORECOLOR = 55,
            IDM_HYPERLINK = 2124,
            IDM_IMAGE = 2168,
            IDM_INDENT = 2186,
            IDM_LISTBOX = 2166,
            IDM_LIVERESIZE = 2398,
            IDM_MULTIPLESELECTION = 2393,
            IDM_NOACTIVATEDESIGNTIMECONTROLS = 2333,
            IDM_NOACTIVATEJAVAAPPLETS = 2334,
            IDM_NOACTIVATENORMALOLECONTROLS = 2332,
            IDM_NOFIXUPURLSONPASTE = 2335,
            IDM_ORDERLIST = 2184,
            IDM_OUTDENT = 2187,
            IDM_PERSISTDEFAULTVALUES = 7100,
            IDM_PRESERVEUNDOALWAYS = 6049,
            IDM_PROTECTMETATAGS = 7101,
            IDM_RADIOBUTTON = 2164,
            IDM_REMOVEFROMGLYPHTABLE = 2338,
            IDM_REPLACEGLYPHCONTENTS = 2339,
            IDM_RESPECTVISIBILITY_INDESIGN = 2405,
            IDM_SETDIRTY = 2342,
            IDM_SHOWZEROBORDERATDESIGNTIME = 2328,
            IDM_SUBSCRIPT = 2247,
            IDM_SUPERSCRIPT = 2248,
            IDM_TEXTBOX = 2161,
            IDM_TEXTAREA = 2162,
            IDM_UNLINK = 2125,
            IDM_UNORDERLIST = 2185,

            INET_E_DEFAULT_ACTION = INET_E_USE_DEFAULT_PROTOCOLHANDLER,
            INET_E_USE_DEFAULT_PROTOCOLHANDLER = unchecked((int)0x800C0011),

            OLEIVERB_DISCARDUNDOSTATE = -6,
            OLEIVERB_HIDE = -3,
            OLEIVERB_INPLACEACTIVATE = -5,
            OLECLOSE_NOSAVE = 1,
            OLEIVERB_OPEN = -2,
            OLEIVERB_PRIMARY = 0,
            OLEIVERB_PROPERTIES = -7,
            OLEIVERB_SHOW = -1,
            OLEIVERB_UIACTIVATE = -4,

            S_FALSE = 1,
            S_OK = 0;

        // InternetProtocol Data notification flags
        [Flags]
            public enum BSCFlags 
        {
            BSCF_FIRSTDATANOTIFICATION          = 0x00000001,
            BSCF_INTERMEDIATEDATANOTIFICATION   = 0x00000002,
            BSCF_LASTDATANOTIFICATION           = 0x00000004,
            BSCF_DATAFULLYAVAILABLE             = 0x00000008,
            BSCF_AVAILABLEDATASIZEUNKNOWN       = 0x00000010
        }

        // Winmm.dll Imports
        public const int
            BITMAPINFO_MAX_COLORSIZE = 256,
            WM_KEYFIRST = 0x0100,
            WM_KEYLAST = 0x0108,
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101;

        [ComVisible(false)]
		internal sealed class OLECMDEXECOPT 
        {
            public const int OLECMDEXECOPT_DODEFAULT = 0;
            public const int OLECMDEXECOPT_PROMPTUSER = 1;
            public const int OLECMDEXECOPT_DONTPROMPTUSER = 2;
            public const int OLECMDEXECOPT_SHOWHELP = 3;
        }

        [ComVisible(false)]
		internal sealed class OLECMDF 
        {
            public const int OLECMDF_SUPPORTED = 1;
            public const int OLECMDF_ENABLED = 2;
            public const int OLECMDF_LATCHED = 4;
            public const int OLECMDF_NINCHED = 8;
        }
        [ComVisible(false)]
        internal sealed class StreamConsts 
        {
            public const int LOCK_WRITE = 0x1;
            public const int LOCK_EXCLUSIVE = 0x2;
            public const int LOCK_ONLYONCE = 0x4;
            public const int STATFLAG_DEFAULT = 0x0;
            public const int STATFLAG_NONAME = 0x1;
            public const int STATFLAG_NOOPEN = 0x2;
            public const int STGC_DEFAULT = 0x0;
            public const int STGC_OVERWRITE = 0x1;
            public const int STGC_ONLYIFCURRENT = 0x2;
            public const int STGC_DANGEROUSLYCOMMITMERELYTODISKCACHE = 0x4;
            public const int STREAM_SEEK_SET = 0x0;
            public const int STREAM_SEEK_CUR = 0x1;
            public const int STREAM_SEEK_END = 0x2;
        }

        [ComVisible(false), StructLayout(LayoutKind.Sequential)]
        public sealed class tagLOGPALETTE 
        {
            [MarshalAs(UnmanagedType.U2)/*leftover(offset=0, palVersion)*/]
            public short palVersion;

            [MarshalAs(UnmanagedType.U2)/*leftover(offset=2, palNumEntries)*/]
            public short palNumEntries;

            // UNMAPPABLE: palPalEntry: Cannot be used as a structure field.
            //   /** @com.structmap(UNMAPPABLE palPalEntry) */
            //  public UNMAPPABLE palPalEntry;
        }

        [ComVisible(false), StructLayout(LayoutKind.Sequential)]
		public sealed class tagOIFI 
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cb;

            [MarshalAs(UnmanagedType.I4)]
            public int fMDIApp;

            public IntPtr hwndFrame;

            public IntPtr hAccel;

            [MarshalAs(UnmanagedType.U4)]
            public int cAccelEntries;

        }

        [ComVisible(false), StructLayout(LayoutKind.Sequential)]
        public sealed class tagOLECMD 
        {

            [MarshalAs(UnmanagedType.U4)]
            public   int cmdID;
            [MarshalAs(UnmanagedType.U4)]
            public   int cmdf;

        }

        [ComVisible(false), StructLayout(LayoutKind.Sequential)]
		public sealed class tagOleMenuGroupWidths 
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=6)]
            public int[] widths = new int[6];
        }

        [ComVisible(false), StructLayout(LayoutKind.Sequential)]
        public sealed class tagOLEVERB 
        {
            [MarshalAs(UnmanagedType.I4)]
            public int lVerb;

            [MarshalAs(UnmanagedType.LPWStr)]
            public String lpszVerbName;

            [MarshalAs(UnmanagedType.U4)]
            public int fuFlags;

            [MarshalAs(UnmanagedType.U4)]
            public int grfAttribs;

        }

        #endregion

        #region Structs
        [ComVisible(false), StructLayout(LayoutKind.Sequential)]
        public class COMMSG 
        {
            public IntPtr   hwnd;
            public int  message;
            public IntPtr   wParam;
            public IntPtr   lParam;
            public int  time;
            // pt was a by-value POINT structure
            public int  pt_x;
            public int  pt_y;
        }

        [ComVisible(true), StructLayout(LayoutKind.Sequential)]
        public class COMRECT 
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public COMRECT() 
            {
            }

            public COMRECT(int left, int top, int right, int bottom) 
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public static COMRECT FromXYWH(int x, int y, int width, int height) 
            {
                return new COMRECT(x, y, x + width, y + height);
            }
        }

        [ComVisible(false), StructLayout(LayoutKind.Sequential)]
        public sealed class DISPPARAMS 
        {
            public   IntPtr rgvarg;
            public   IntPtr rgdispidNamedArgs;
            [MarshalAs(UnmanagedType.U4)]
            public   int cArgs;
            [MarshalAs(UnmanagedType.U4)]
            public   int cNamedArgs;
        }

        [ComVisible(false), StructLayout(LayoutKind.Sequential)]
        public sealed class EXCEPINFO 
        {

            [MarshalAs(UnmanagedType.U2)]
            public   short wCode;
            [MarshalAs(UnmanagedType.U2)]
            public   short wReserved;
            [MarshalAs(UnmanagedType.BStr)]
            public    string bstrSource;
            [MarshalAs(UnmanagedType.BStr)]
            public    string bstrDescription;
            [MarshalAs(UnmanagedType.BStr)]
            public    string bstrHelpFile;
            [MarshalAs(UnmanagedType.U4)]
            public   int dwHelpContext;
            public   IntPtr dwReserved;
            public   IntPtr dwFillIn;
            [MarshalAs(UnmanagedType.I4)]
            public   int scode;
        }

        [ComVisible(true), StructLayout(LayoutKind.Sequential)]
            public class DOCHOSTUIINFO 
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;

            [MarshalAs(UnmanagedType.I4)]
            public int dwFlags;

            [MarshalAs(UnmanagedType.I4)]
            public int dwDoubleClick;

            [MarshalAs(UnmanagedType.I4)]
            public int dwReserved1;

            [MarshalAs(UnmanagedType.I4)]
            public int dwReserved2;
        }

        [ComVisible(false), StructLayout(LayoutKind.Sequential)]
            public sealed class FORMATETC 
        {

            [MarshalAs(UnmanagedType.I4)]
            public int cfFormat;
            public IntPtr ptd;
            [MarshalAs(UnmanagedType.I4)]
            public int dwAspect;
            [MarshalAs(UnmanagedType.I4)]
            public int lindex;
            [MarshalAs(UnmanagedType.I4)]
            public int tymed;

        }


        [ComVisible(true), StructLayout(LayoutKind.Sequential)]
            public class HTML_PAINTER_INFO 
        {

            [MarshalAs(UnmanagedType.I4)]
            public int lFlags;

            [MarshalAs(UnmanagedType.I4)]
            public int lZOrder;

            [MarshalAs(UnmanagedType.Struct)]
            public Guid iidDrawObject;

            [MarshalAs(UnmanagedType.Struct)]
            public NativeMethods.RECT rcBounds;
        }

        [ComVisible(false), StructLayout(LayoutKind.Sequential)]
            public class NMHDR 
        {
            public IntPtr hwndFrom;
            public int idFrom;
            public int code;
        }

        [ComVisible(false), StructLayout(LayoutKind.Sequential)]
            public class NMCUSTOMDRAW 
        {
            public NMHDR nmcd;
            public int dwDrawStage;
            public IntPtr hdc;
            public RECT rc;
            public int dwItemSpec;
            public int uItemState;
            public IntPtr lItemlParam;
        }

        [ComVisible(true), StructLayout(LayoutKind.Sequential)]
            public class POINT 
        {
            public int x;
            public int y;

            public POINT() 
            {
            }

            public POINT(int x, int y) 
            {
                this.x = x;
                this.y = y;
            }
        }

        [ComVisible(true), StructLayout(LayoutKind.Sequential)]
            public struct RECT 
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public RECT(int left, int top, int right, int bottom) 
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public static RECT FromXYWH(int x, int y, int width, int height) 
            {
                return new RECT(x, y, x + width, y + height);
            }
        }

        [ComVisible(false), StructLayout(LayoutKind.Sequential)]
            public sealed class STATDATA 
        {

            [MarshalAs(UnmanagedType.U4)]
            public   int advf;
            [MarshalAs(UnmanagedType.U4)]
            public   int dwConnection;

        }

        [ComVisible(false), StructLayout(LayoutKind.Sequential)]
        public class STGMEDIUM 
        {
            [MarshalAs(UnmanagedType.I4)]
            public int tymed;
            public IntPtr unionmember;
            public IntPtr pUnkForRelease;
        }

        [ComVisible(false), StructLayout(LayoutKind.Sequential)]
            public class STATSTG 
        {

            [MarshalAs(UnmanagedType.I4)]
            public   int pwcsName;
            [MarshalAs(UnmanagedType.I4)]
            public   int type;
            [MarshalAs(UnmanagedType.I8)]
            public   long cbSize;
            [MarshalAs(UnmanagedType.I8)]
            public   long mtime;
            [MarshalAs(UnmanagedType.I8)]
            public   long ctime;
            [MarshalAs(UnmanagedType.I8)]
            public   long atime;
            [MarshalAs(UnmanagedType.I8)]
            public   long grfMode;
            [MarshalAs(UnmanagedType.I8)]
            public   long grfLocksSupported;
            [MarshalAs(UnmanagedType.I4)]
            public   int clsid_data1;
            [MarshalAs(UnmanagedType.I2)]
            public   short clsid_data2;
            [MarshalAs(UnmanagedType.I2)]
            public   short clsid_data3;
            [MarshalAs(UnmanagedType.U1)]
            public   byte clsid_b0;
            [MarshalAs(UnmanagedType.U1)]
            public   byte clsid_b1;
            [MarshalAs(UnmanagedType.U1)]
            public   byte clsid_b2;
            [MarshalAs(UnmanagedType.U1)]
            public   byte clsid_b3;
            [MarshalAs(UnmanagedType.U1)]
            public   byte clsid_b4;
            [MarshalAs(UnmanagedType.U1)]
            public   byte clsid_b5;
            [MarshalAs(UnmanagedType.U1)]
            public   byte clsid_b6;
            [MarshalAs(UnmanagedType.U1)]
            public   byte clsid_b7;
            [MarshalAs(UnmanagedType.I8)]
            public   long grfStateBits;
            [MarshalAs(UnmanagedType.I8)]
            public   long reserved;
        }

        [ComVisible(false), StructLayout(LayoutKind.Sequential)]
            public sealed class tagSIZE 
        {
            [MarshalAs(UnmanagedType.I4)]
            public int cx;

            [MarshalAs(UnmanagedType.I4)]
            public int cy;

        }

        [ComVisible(true), StructLayout(LayoutKind.Sequential)]
            public sealed class tagSIZEL 
        {
            [MarshalAs(UnmanagedType.I4)]
            public int cx;

            [MarshalAs(UnmanagedType.I4)]
            public int cy;

        }

        #endregion

		[DllImport("urlmon.dll", ExactSpelling=true, CharSet=CharSet.Unicode)]
		internal static extern int CreateURLMoniker(IMoniker pmkContext, string szURL, [Out] out IMoniker ppmk);

        #region  Ole32.dll imports
		[DllImport("ole32.dll", PreserveSig=false)]
		internal static extern void CreateStreamOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease, [Out] out IStream pStream);

		[DllImport("ole32.dll", PreserveSig=false)]
		internal static extern void GetHGlobalFromStream(IStream pStream, [Out] out IntPtr pHGlobal);

		[DllImport("ole32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		internal static extern int CreateBindCtx(int dwReserved, [Out] out IBindCtx ppbc);

		#endregion

        #region user32.dll imports
        [DllImport("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
        internal static extern bool GetClientRect(IntPtr hWnd, [In, Out] COMRECT rect);

        [DllImport("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
        internal static extern IntPtr SetFocus(IntPtr hWnd);
        #endregion

        #region kernel32.dll Imports
        [DllImport("kernel32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
        internal static extern IntPtr GlobalLock(IntPtr handle);

        [DllImport("kernel32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
        internal static extern bool GlobalUnlock(IntPtr handle);
        #endregion

        #region Interfaces and classes
        [ComVisible(false)]
		internal class ConnectionPointCookie 
        {
			private NativeMethods.IConnectionPoint connectionPoint;
            private int cookie;

            // UNDONE: Should cookie be IntPtr?

            public ConnectionPointCookie(object source, object sink, Type eventInterface) : this(source, sink, eventInterface, true) 
            {
            }

            public ConnectionPointCookie(object source, object sink, Type eventInterface, bool throwException)
            {
                Exception ex = null;
				if (source is NativeMethods.IConnectionPointContainer) 
                {
					NativeMethods.IConnectionPointContainer cpc = (NativeMethods.IConnectionPointContainer)source;

                    try
                    {
                        Guid tmp = eventInterface.GUID;
                        cpc.FindConnectionPoint(ref tmp, out connectionPoint);
                    } 
                    catch(Exception) 
                    {
                        connectionPoint = null;
                    }

                    if (connectionPoint == null)
                    {
                        ex = new ArgumentException("The source object does not expose the " + eventInterface.Name + " event inteface");
                    }
                    else if (!eventInterface.IsInstanceOfType(sink)) 
                    {
                        ex = new InvalidCastException("The sink object does not implement the eventInterface");
                    }
                    else 
                    {
                        try 
                        {
                            connectionPoint.Advise(sink, out cookie);
                        }
                        catch 
                        {
                            cookie = 0;
                            connectionPoint = null;
                            ex = new Exception("IConnectionPoint::Advise failed for event interface '" + eventInterface.Name + "'");
                        }
                    }
                }
                else 
                {
                    ex = new InvalidCastException("The source object does not expost IConnectionPointContainer");
                }

                if (throwException && (connectionPoint == null || cookie == 0))
                {
                    if (ex == null) 
                    {
                        throw new ArgumentException("Could not create connection point for event interface '" + eventInterface.Name + "'");
                    }
                    else 
                    {
                        throw ex;
                    }
                }
            }

            public void Disconnect()
            {
                if (this.connectionPoint != null && this.cookie != 0)
                {
                    this.connectionPoint.Unadvise(cookie);
                    this.cookie = 0;
                    this.connectionPoint = null;
                }
            }

            ~ConnectionPointCookie() 
            {
                this.Disconnect();
            }
        }

        [ComVisible(true), Guid("0000010F-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IAdviseSink 
        {

            //C#r: UNDONE (Field in interface) public static readonly    Guid iid;
            void OnDataChange(
                [In]
                FORMATETC pFormatetc,
                [In]
                STGMEDIUM pStgmed);

            void OnViewChange(
                [In, MarshalAs(UnmanagedType.U4)]
                int dwAspect,
                [In, MarshalAs(UnmanagedType.I4)]
                int lindex);

            void OnRename(
                [In, MarshalAs(UnmanagedType.Interface)]
                object pmk);

            void OnSave();


            void OnClose();
        }

        [ComVisible(true), Guid("0000000e-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IBindCtx 
        {

            // UNDONE: Methods not defined yet
        }


        [ComVisible(true), ComImport(), Guid("00000001-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IClassFactory 
        {
            [PreserveSig]
            int CreateInstance(
                [In, MarshalAs(UnmanagedType.Interface)]
                object pUnkOuter,
                ref Guid riid,
                [Out, MarshalAs(UnmanagedType.Interface)]
                out object obj);

            [PreserveSig]
            int LockServer(
                [In]
                bool fLock);
        }

        [ComVisible(true), ComImport(), Guid("BD3F23C0-D43E-11CF-893B-00AA00BDCE1A"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IDocHostUIHandler 
        {

            [PreserveSig]
            int ShowContextMenu(
                [In, MarshalAs(UnmanagedType.U4)]        int dwID,
                [In]                                      NativeMethods.POINT pt,
                [In, MarshalAs(UnmanagedType.Interface)] object pcmdtReserved,
                [In, MarshalAs(UnmanagedType.Interface)] object pdispReserved);

            [PreserveSig]
            int GetHostInfo(
                [In, Out] DOCHOSTUIINFO info);

            [PreserveSig]
            int ShowUI(
                [In, MarshalAs(UnmanagedType.I4)]        int dwID,
                [In, MarshalAs(UnmanagedType.Interface)] NativeMethods.IOleInPlaceActiveObject activeObject,
                [In, MarshalAs(UnmanagedType.Interface)] NativeMethods.IOleCommandTarget commandTarget,
                [In, MarshalAs(UnmanagedType.Interface)] NativeMethods.IOleInPlaceFrame frame,
                [In, MarshalAs(UnmanagedType.Interface)] NativeMethods.IOleInPlaceUIWindow doc);

            [PreserveSig]
            int HideUI();

            [PreserveSig]
            int UpdateUI();

            [PreserveSig]
            int EnableModeless(
                [In, MarshalAs(UnmanagedType.Bool)] bool fEnable);

            [PreserveSig]
            int OnDocWindowActivate(
                [In, MarshalAs(UnmanagedType.Bool)] bool fActivate);

            [PreserveSig]
            int OnFrameWindowActivate(
                [In, MarshalAs(UnmanagedType.Bool)] bool fActivate);

            [PreserveSig]
            int ResizeBorder(
                [In] NativeMethods.COMRECT rect,
                [In] NativeMethods.IOleInPlaceUIWindow doc,
                [In] bool fFrameWindow);

            [PreserveSig]
            int TranslateAccelerator(
                [In]                               NativeMethods.COMMSG msg,
                [In]                               ref Guid group,
                [In, MarshalAs(UnmanagedType.I4)] int nCmdID);

            [PreserveSig]
            int GetOptionKeyPath(
                [Out, MarshalAs(UnmanagedType.LPArray)] String[] pbstrKey,
                [In, MarshalAs(UnmanagedType.U4)]        int dw);

            [PreserveSig]
            int GetDropTarget(
                [In, MarshalAs(UnmanagedType.Interface)]   NativeMethods.IOleDropTarget pDropTarget,
                [Out, MarshalAs(UnmanagedType.Interface)] out NativeMethods.IOleDropTarget ppDropTarget);

            [PreserveSig]
            int GetExternal(
                [Out, MarshalAs(UnmanagedType.Interface)] out object ppDispatch);

            [PreserveSig]
            int TranslateUrl(
                [In, MarshalAs(UnmanagedType.U4)]       int dwTranslate,
                [In, MarshalAs(UnmanagedType.LPWStr)]   string strURLIn,
                [Out, MarshalAs(UnmanagedType.LPWStr)] out string pstrURLOut);

            [PreserveSig]
            int FilterDataObject(
                [In, MarshalAs(UnmanagedType.Interface)]   NativeMethods.IOleDataObject pDO,
                [Out, MarshalAs(UnmanagedType.Interface)] out NativeMethods.IOleDataObject ppDORet);
        }

        [ComVisible(true), Guid("3050F425-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IElementBehavior 
        {

            void Init(
                [In, MarshalAs(UnmanagedType.Interface)]
                IElementBehaviorSite pBehaviorSite);

            void Notify(
                [In, MarshalAs(UnmanagedType.U4)]
                int dwEvent,
                [In]
                IntPtr pVar);

            void Detach();
        }

        [ComVisible(true), Guid("3050F429-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IElementBehaviorFactory 
        {

            [return: MarshalAs(UnmanagedType.Interface)]
            IElementBehavior FindBehavior(
                [In, MarshalAs(UnmanagedType.BStr)]
                string bstrBehavior,
                [In, MarshalAs(UnmanagedType.BStr)]
                string bstrBehaviorUrl,
                [In, MarshalAs(UnmanagedType.Interface)]
                IElementBehaviorSite pSite);
        }

        [ComVisible(true), Guid("3050F427-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IElementBehaviorSite 
        {

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement GetElement();


            void RegisterNotification(
                [In, MarshalAs(UnmanagedType.I4)]
                int lEvent);
        }

        [ComVisible(true), Guid("3050F659-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IElementBehaviorSiteOM2 
        {

            [return: MarshalAs(UnmanagedType.I4)]
            int RegisterEvent(
                [In, MarshalAs(UnmanagedType.BStr)]
                string pchEvent,
                [In, MarshalAs(UnmanagedType.I4)]
                int lFlags);

            [return: MarshalAs(UnmanagedType.I4)]
            int GetEventCookie(
                [In, MarshalAs(UnmanagedType.BStr)]
                string pchEvent);


            void FireEvent(
                [In, MarshalAs(UnmanagedType.I4)]
                int lCookie,
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLEventObj pEventObject);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLEventObj CreateEventObject();


            void RegisterName(
                [In, MarshalAs(UnmanagedType.BStr)]
                string pchName);


            void RegisterUrn(
                [In, MarshalAs(UnmanagedType.BStr)]
                string pchUrn);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElementDefaults GetDefaults();
        }

        [ComVisible(true), Guid("3050F671-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IElementNamespace 
        {

            void AddTag(
                [In, MarshalAs(UnmanagedType.BStr)]
                string tagName,
                [In, MarshalAs(UnmanagedType.I4)]
                int lFlags);
        }

        [ComVisible(true), Guid("3050F672-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IElementNamespaceFactory 
        {

            void Create(
                [In, MarshalAs(UnmanagedType.Interface)]
                IElementNamespace pNamespace);
        }

        [ComVisible(true), Guid("3050F7FD-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IElementNamespaceFactoryCallback 
        {

            void Resolve(
                [In, MarshalAs(UnmanagedType.BStr)]
                string nameSpace,
                [In, MarshalAs(UnmanagedType.BStr)]
                string tagName,
                [In, MarshalAs(UnmanagedType.BStr)]
                string attributes,
                [In, MarshalAs(UnmanagedType.Interface)]
                IElementNamespace pNamespace);
        }

        [ComVisible(true), Guid("3050F670-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IElementNamespaceTable 
        {

            void AddNamespace(
                [In, MarshalAs(UnmanagedType.BStr)]
                string nameSpace,
                [In, MarshalAs(UnmanagedType.BStr)]
                string urn,
                [In, MarshalAs(UnmanagedType.I4)]
                int lFlags,
                [In]
                ref Object factory);
        }

        [ComVisible(true), ComImport(), Guid("00000103-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IEnumFORMATETC 
        {

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Next(
                [In, MarshalAs(UnmanagedType.U4)]
                int celt,
                [Out]
                FORMATETC rgelt,
                [In, Out, MarshalAs(UnmanagedType.LPArray)]
                int[] pceltFetched);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Skip(
                [In, MarshalAs(UnmanagedType.U4)]
                int celt);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Reset();

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Clone(
                [Out, MarshalAs(UnmanagedType.LPArray)]
                IEnumFORMATETC[] ppenum);
        }

        [ComVisible(true), Guid("B3E7C340-EF97-11CE-9BC9-00AA00608E01"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IEnumOleUndoUnits  
        {

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Next([In, MarshalAs(UnmanagedType.U4)] int numDesired, [Out] out IntPtr unit, [Out, MarshalAs(UnmanagedType.U4)] out int numReceived);

            void Bogus();

            [PreserveSig]
            int Skip([In, MarshalAs(UnmanagedType.I4)] int numToSkip);

            [PreserveSig]
            int Reset();

            [PreserveSig]
            int Clone([Out, MarshalAs(UnmanagedType.Interface)] IEnumOleUndoUnits enumerator);
        };
        [ComVisible(true), ComImport(), Guid("00000104-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IEnumOLEVERB 
        {

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Next(
                [MarshalAs(UnmanagedType.U4)]
                int celt,
                [Out]
                tagOLEVERB rgelt,
                [Out, MarshalAs(UnmanagedType.LPArray)]
                int[] pceltFetched);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Skip(
                [In, MarshalAs(UnmanagedType.U4)]
                int celt);


            void Reset();


            void Clone(
                out IEnumOLEVERB ppenum);


        }

        [ComVisible(true), Guid("00000105-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IEnumSTATDATA 
        {

            //C#r: UNDONE (Field in interface) public static readonly    Guid iid;

            void Next(
                [In, MarshalAs(UnmanagedType.U4)]
                int celt,
                [Out]
                STATDATA rgelt,
                [Out, MarshalAs(UnmanagedType.LPArray)]
                int[] pceltFetched);


            void Skip(
                [In, MarshalAs(UnmanagedType.U4)]
                int celt);


            void Reset();


            void Clone(
                [Out, MarshalAs(UnmanagedType.LPArray)]
                IEnumSTATDATA[] ppenum);


        }

        [ComVisible(true), Guid("3050f1d8-98b5-11cf-bb82-00aa00bdce0b"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch)]
		internal interface IHtmlBodyElement 
        {
            void put_background(
                [In, MarshalAs(UnmanagedType.BStr)]
                string v);

            [return: MarshalAs(UnmanagedType.BStr)]
            string get_background();

            void put_bgProperties(
                [In, MarshalAs(UnmanagedType.BStr)]
                string v);

            [return: MarshalAs(UnmanagedType.BStr)]
            string get_bgProperties();

            void put_leftMargin(
                [In, MarshalAs(UnmanagedType.Interface)]
                object o);

            [return: MarshalAs(UnmanagedType.Interface)]
            object get_leftMargin();

            void put_topMargin(
                [In, MarshalAs(UnmanagedType.Interface)]
                object o);

            [return: MarshalAs(UnmanagedType.Interface)]
            object get_topMargin();

            void put_rightMargin(
                [In, MarshalAs(UnmanagedType.Interface)]
                object o);

            [return: MarshalAs(UnmanagedType.Interface)]
            object get_rightMargin();

            void put_bottomMargin(
                [In, MarshalAs(UnmanagedType.Interface)]
                object o);

            [return: MarshalAs(UnmanagedType.Interface)]
            object get_bottomMargin();

            void put_noWrap(
                [In, MarshalAs(UnmanagedType.Interface)]
                object o);

            [return: MarshalAs(UnmanagedType.Interface)]
            object get_noWrap();

            void put_bgColor(
                [In, MarshalAs(UnmanagedType.Interface)]
                object o);

            [return: MarshalAs(UnmanagedType.Interface)]
            object get_bgColor();

            void put_text(
                [In, MarshalAs(UnmanagedType.Interface)]
                object o);
            [return: MarshalAs(UnmanagedType.Interface)]
            object get_text();

            void put_link(
                [In, MarshalAs(UnmanagedType.Interface)]
                object o);

            [return: MarshalAs(UnmanagedType.Interface)]
            object get_link();

            void put_vLink(
                [In, MarshalAs(UnmanagedType.Interface)]
                object o);

            [return: MarshalAs(UnmanagedType.Interface)]
            object get_vLink();

            void put_aLink(
                [In, MarshalAs(UnmanagedType.Interface)]
                object o);

            [return: MarshalAs(UnmanagedType.Interface)]
            object get_aLink();

            void put_onload(
                [In, MarshalAs(UnmanagedType.Interface)]
                object o);

            [return: MarshalAs(UnmanagedType.Interface)]
            object get_onload();

            void put_onunload(
                [In, MarshalAs(UnmanagedType.Interface)]
                object o);

            [return: MarshalAs(UnmanagedType.Interface)]
            object get_onunload();

            void put_scroll(
                [In, MarshalAs(UnmanagedType.BStr)]
                string s);

            [return: MarshalAs(UnmanagedType.BStr)]
            string get_scroll();

            void put_onselect(
                [In, MarshalAs(UnmanagedType.Interface)]
                object o);

            [return: MarshalAs(UnmanagedType.Interface)]
            object get_onselect();

            void put_onbeforeunload(
                [In, MarshalAs(UnmanagedType.Interface)]
                object o);

            [return: MarshalAs(UnmanagedType.Interface)]
            object get_onbeforeunload();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLTxtRange createTextRange();

        }

        [ComVisible(true), Guid("3050F4E9-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHtmlControlElement 
        {

            void SetTabIndex(
                [In, MarshalAs(UnmanagedType.I2)]
                short p);

            [return: MarshalAs(UnmanagedType.I2)]
            short GetTabIndex();


            void Focus();


            void SetAccessKey(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetAccessKey();


            void SetOnblur(
                [In, MarshalAs(UnmanagedType.Struct)]
                object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            object GetOnblur();


            void SetOnfocus(
                [In, MarshalAs(UnmanagedType.Struct)]
                object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            object GetOnfocus();


            void SetOnresize(
                [In, MarshalAs(UnmanagedType.Struct)]
                object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            object GetOnresize();


            void Blur();


            void AddFilter(
                [In, MarshalAs(UnmanagedType.Interface)]
                object pUnk);


            void RemoveFilter(
                [In, MarshalAs(UnmanagedType.Interface)]
                object pUnk);

            [return: MarshalAs(UnmanagedType.I4)]
            int GetClientHeight();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetClientWidth();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetClientTop();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetClientLeft();
        }

        #region HTMLControlRange interfaces
        [ComVisible(true), Guid("3050F29C-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHtmlControlRange 
        {


            void Select();


            void Add(
                [In, MarshalAs(UnmanagedType.Interface)]
                IHtmlControlElement item);


            void Remove(
                [In, MarshalAs(UnmanagedType.I4)]
                int index);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement Item(
                [In, MarshalAs(UnmanagedType.I4)]
                int index);


            void ScrollIntoView(
                [In, MarshalAs(UnmanagedType.Struct)]
                object varargStart);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool QueryCommandSupported(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool QueryCommandEnabled(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool QueryCommandState(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool QueryCommandIndeterm(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.BStr)]
            string QueryCommandText(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.Struct)]
            object QueryCommandValue(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool ExecCommand(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID,
                [In, MarshalAs(UnmanagedType.Bool)]
                bool showUI,
                [In, MarshalAs(UnmanagedType.Struct)]
                object value);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool ExecCommandShowHelp(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement CommonParentElement();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetLength();
        }

        [ComVisible(true), Guid("3050f65e-98b5-11cf-bb82-00aa00bdce0b"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHtmlControlRange2 
        {
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int addElement(
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLElement element);
        }

        #endregion
        [ComVisible(true), Guid("3050F3DB-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLCurrentStyle 
        {

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetPosition();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetStyleFloat();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetColor();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBackgroundColor();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFontFamily();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFontStyle();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFontObject();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetFontWeight();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetFontSize();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBackgroundImage();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBackgroundPositionX();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBackgroundPositionY();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBackgroundRepeat();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderLeftColor();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderTopColor();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderRightColor();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderBottomColor();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderTopStyle();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderRightStyle();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderBottomStyle();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderLeftStyle();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderTopWidth();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderRightWidth();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderBottomWidth();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderLeftWidth();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetLeft();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetTop();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetWidth();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetHeight();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetPaddingLeft();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetPaddingTop();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetPaddingRight();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetPaddingBottom();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetTextAlign();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetTextDecoration();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetDisplay();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetVisibility();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetZIndex();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetLetterSpacing();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetLineHeight();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetTextIndent();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetVerticalAlign();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBackgroundAttachment();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetMarginTop();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetMarginRight();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetMarginBottom();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetMarginLeft();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetClear();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetListStyleType();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetListStylePosition();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetListStyleImage();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetClipTop();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetClipRight();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetClipBottom();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetClipLeft();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetOverflow();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetPageBreakBefore();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetPageBreakAfter();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetCursor();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetTableLayout();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderCollapse();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetDirection();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBehavior();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetAttribute(
                [In, MarshalAs(UnmanagedType.BStr)]
                string strAttributeName,
                [In, MarshalAs(UnmanagedType.I4)]
                int lFlags);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetUnicodeBidi();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetRight();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBottom();
        }

        #region HTMLDocument interfaces and classes
        [ComVisible(true), ComImport(), Guid("25336920-03F9-11CF-8FD0-00AA00686F13")]
            public class HTMLDocument 
        {
        }


        [ComVisible(true), Guid("626FC520-A41E-11CF-A731-00A0C9082637"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLDocument 
        {

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetScript();
        }

        [ComVisible(true), Guid("332C4425-26CB-11D0-B483-00C04FD90119"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLDocument2 
        {

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetScript();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElementCollection GetAll();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement GetBody();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement GetActiveElement();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElementCollection GetImages();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElementCollection GetApplets();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElementCollection GetLinks();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElementCollection GetForms();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElementCollection GetAnchors();


            void SetTitle(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetTitle();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElementCollection GetScripts();


            void SetDesignMode(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetDesignMode();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLSelectionObject GetSelection();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetReadyState();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetFrames();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElementCollection GetEmbeds();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElementCollection GetPlugins();

            void SetAlinkColor(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetAlinkColor();

            void SetBgColor(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBgColor();

            void SetFgColor(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetFgColor();

            void SetLinkColor(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetLinkColor();

            void SetVlinkColor(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetVlinkColor();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetReferrer();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetLocation();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetLastModified();

            void SetURL(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetURL();

            void SetDomain(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetDomain();

            void SetCookie(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetCookie();

            void SetExpando(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool p);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetExpando();

            void SetCharset(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetCharset();

            void SetDefaultCharset(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetDefaultCharset();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetMimeType();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFileSize();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFileCreatedDate();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFileModifiedDate();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFileUpdatedDate();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetSecurity();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetProtocol();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetNameProp();

            void DummyWrite(
                [In, MarshalAs(UnmanagedType.I4)]
                int psarray);

            void DummyWriteln(
                [In, MarshalAs(UnmanagedType.I4)]
                int psarray);

            [return: MarshalAs(UnmanagedType.Interface)]
            object Open(
                [In, MarshalAs(UnmanagedType.BStr)]
                string URL,
                [In, MarshalAs(UnmanagedType.Struct)]
                Object name,
                [In, MarshalAs(UnmanagedType.Struct)]
                Object features,
                [In, MarshalAs(UnmanagedType.Struct)]
                Object replace);

            void Close();

            void Clear();

            [return: MarshalAs(UnmanagedType.Bool)]
            bool QueryCommandSupported(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool QueryCommandEnabled(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool QueryCommandState(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool QueryCommandIndeterm(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.BStr)]
            string QueryCommandText(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object QueryCommandValue(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool ExecCommand(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID,
                [In, MarshalAs(UnmanagedType.Bool)]
                bool showUI,
                [In, MarshalAs(UnmanagedType.Struct)]
                Object value);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool ExecCommandShowHelp(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement CreateElement(
                [In, MarshalAs(UnmanagedType.BStr)]
                string eTag);

            void SetOnhelp(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnhelp();

            void SetOnclick(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnclick();

            void SetOndblclick(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOndblclick();


            void SetOnkeyup(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnkeyup();

            void SetOnkeydown(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnkeydown();

            void SetOnkeypress(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnkeypress();

            void SetOnmouseup(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnmouseup();

            void SetOnmousedown(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnmousedown();

            void SetOnmousemove(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnmousemove();

            void SetOnmouseout(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnmouseout();

            void SetOnmouseover(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnmouseover();

            void SetOnreadystatechange(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnreadystatechange();

            void SetOnafterupdate(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnafterupdate();

            void SetOnrowexit(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnrowexit();

            void SetOnrowenter(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnrowenter();

            int SetOndragstart(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOndragstart();

            void SetOnselectstart(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnselectstart();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement ElementFromPoint(
                [In, MarshalAs(UnmanagedType.I4)]
                int x,
                [In, MarshalAs(UnmanagedType.I4)]
                int y);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLWindow2 GetParentWindow();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLStyleSheetsCollection GetStyleSheets();

            void SetOnbeforeupdate(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnbeforeupdate();

            void SetOnerrorupdate(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnerrorupdate();

            [return: MarshalAs(UnmanagedType.BStr)]
            string toString();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLStyleSheet CreateStyleSheet(
                [In, MarshalAs(UnmanagedType.BStr)]
                string bstrHref,
                [In, MarshalAs(UnmanagedType.I4)]
                int lIndex);
        }
        #endregion
        [ComVisible(true), Guid("3050f662-98b5-11cf-bb82-00aa00bdce0b"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IHTMLEditDesigner 
        {
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int PreHandleEvent(
                [In] int dispId,
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLEventObj eventObj
                );

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int PostHandleEvent(
                [In] int dispId,
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLEventObj eventObj
                );

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int TranslateAccelerator(
                [In] int dispId,
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLEventObj eventObj
                );

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int PostEditorEventNotify(
                [In] int dispId,
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLEventObj eventObj
                );

        }

        [ComVisible(true), Guid("3050f6a0-98b5-11cf-bb82-00aa00bdce0b"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IHTMLEditHost 
        {

            void SnapRect(
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLElement pElement,
                [In, Out]
                NativeMethods.COMRECT rcNew,
                [In, MarshalAs(UnmanagedType.I4)]
                int nHandle);
        }

        [ComVisible(true), Guid("3050f663-98b5-11cf-bb82-00aa00bdce0b"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IHTMLEditServices 
        {

            [return: MarshalAs(UnmanagedType.I4)]
            int AddDesigner(
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLEditDesigner designer);

            [return: MarshalAs(UnmanagedType.Interface)]
            object /*ISelectionServices*/ GetSelectionServices(
                [In, MarshalAs(UnmanagedType.Interface)]
                object /*IMarkupContainer*/ markupContainer);

            [return: MarshalAs(UnmanagedType.I4)]
            int MoveToSelectionAnchor (
                [In, MarshalAs(UnmanagedType.Interface)]
                object markupPointer);

            [return: MarshalAs(UnmanagedType.I4)]
            int MoveToSelectionEnd (
                [In, MarshalAs(UnmanagedType.Interface)]
                object markupPointer);

            [return: MarshalAs(UnmanagedType.I4)]
            int RemoveDesigner(
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLEditDesigner designer);
        }

        #region HTMLElement interfaces
        [ComVisible(true), Guid("3050F1FF-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLElement 
        {


            void SetAttribute(
                [In, MarshalAs(UnmanagedType.BStr)]
                string strAttributeName,
                [In, MarshalAs(UnmanagedType.Struct)]
                Object AttributeValue,
                [In, MarshalAs(UnmanagedType.I4)]
                int lFlags);


            void GetAttribute(
                [In, MarshalAs(UnmanagedType.BStr)]
                string strAttributeName,
                [In, MarshalAs(UnmanagedType.I4)]
                int lFlags,
                [Out, MarshalAs(UnmanagedType.LPArray)]
                Object[] pvars);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool RemoveAttribute(
                [In, MarshalAs(UnmanagedType.BStr)]
                string strAttributeName,
                [In, MarshalAs(UnmanagedType.I4)]
                int lFlags);


            void SetClassName(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetClassName();


            void SetId(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetId();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetTagName();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement GetParentElement();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLStyle GetStyle();


            void SetOnhelp(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnhelp();


            void SetOnclick(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnclick();


            void SetOndblclick(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOndblclick();


            void SetOnkeydown(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnkeydown();


            void SetOnkeyup(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnkeyup();


            void SetOnkeypress(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnkeypress();


            void SetOnmouseout(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnmouseout();


            void SetOnmouseover(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnmouseover();


            void SetOnmousemove(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnmousemove();


            void SetOnmousedown(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnmousedown();


            void SetOnmouseup(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnmouseup();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetDocument();


            void SetTitle(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetTitle();


            void SetLanguage(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetLanguage();


            void SetOnselectstart(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnselectstart();


            void ScrollIntoView(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object varargStart);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool Contains(
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLElement pChild);

            [return: MarshalAs(UnmanagedType.I4)]
            int GetSourceIndex();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetRecordNumber();


            void SetLang(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetLang();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetOffsetLeft();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetOffsetTop();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetOffsetWidth();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetOffsetHeight();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement GetOffsetParent();


            void SetInnerHTML(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetInnerHTML();


            void SetInnerText(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetInnerText();


            void SetOuterHTML(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetOuterHTML();


            void SetOuterText(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetOuterText();


            void InsertAdjacentHTML(
                [In, MarshalAs(UnmanagedType.BStr)]
                string whereText,
                [In, MarshalAs(UnmanagedType.BStr)]
                string html);


            void InsertAdjacentText(
                [In, MarshalAs(UnmanagedType.BStr)]
                string whereText,
                [In, MarshalAs(UnmanagedType.BStr)]
                string text);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement GetParentTextEdit();

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetIsTextEdit();


            void Click();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetFilters();


            void SetOndragstart(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOndragstart();

            [return: MarshalAs(UnmanagedType.BStr)]
            string toString();


            void SetOnbeforeupdate(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnbeforeupdate();


            void SetOnafterupdate(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnafterupdate();


            void SetOnerrorupdate(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnerrorupdate();


            void SetOnrowexit(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnrowexit();


            void SetOnrowenter(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnrowenter();


            void SetOndatasetchanged(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOndatasetchanged();


            void SetOndataavailable(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOndataavailable();


            void SetOndatasetcomplete(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOndatasetcomplete();


            void SetOnfilterchange(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnfilterchange();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetChildren();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetAll();

        }

        [ComVisible(true), Guid("3050F434-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLElement2 
        {

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetScopeName();


            void SetCapture(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool containerCapture);


            void ReleaseCapture();


            void SetOnlosecapture(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnlosecapture();

            [return: MarshalAs(UnmanagedType.BStr)]
            string ComponentFromPoint(
                [In, MarshalAs(UnmanagedType.I4)]
                int x,
                [In, MarshalAs(UnmanagedType.I4)]
                int y);


            void DoScroll(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object component);


            void SetOnscroll(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnscroll();


            void SetOndrag(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOndrag();


            void SetOndragend(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOndragend();


            void SetOndragenter(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOndragenter();


            void SetOndragover(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOndragover();


            void SetOndragleave(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOndragleave();


            void SetOndrop(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOndrop();


            void SetOnbeforecut(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnbeforecut();


            void SetOncut(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOncut();


            void SetOnbeforecopy(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnbeforecopy();


            void SetOncopy(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOncopy();


            void SetOnbeforepaste(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnbeforepaste();


            void SetOnpaste(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnpaste();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLCurrentStyle GetCurrentStyle();


            void SetOnpropertychange(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnpropertychange();

            [return: MarshalAs(UnmanagedType.Interface)]
            object /*IHTMLRectCollection*/ GetClientRects();

            [return: MarshalAs(UnmanagedType.Interface)]
            object /*IHTMLRect*/ GetBoundingClientRect();


            void SetExpression(
                [In, MarshalAs(UnmanagedType.BStr)]
                string propname,
                [In, MarshalAs(UnmanagedType.BStr)]
                string expression,
                [In, MarshalAs(UnmanagedType.BStr)]
                string language);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetExpression(
                [In, MarshalAs(UnmanagedType.BStr)]
                Object propname);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool RemoveExpression(
                [In, MarshalAs(UnmanagedType.BStr)]
                string propname);


            void SetTabIndex(
                [In, MarshalAs(UnmanagedType.I2)]
                short p);

            [return: MarshalAs(UnmanagedType.I2)]
            short GetTabIndex();


            void Focus();


            void SetAccessKey(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetAccessKey();


            void SetOnblur(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnblur();


            void SetOnfocus(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnfocus();


            void SetOnresize(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnresize();


            void Blur();


            void AddFilter(
                [In, MarshalAs(UnmanagedType.Interface)]
                object pUnk);


            void RemoveFilter(
                [In, MarshalAs(UnmanagedType.Interface)]
                object pUnk);

            [return: MarshalAs(UnmanagedType.I4)]
            int GetClientHeight();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetClientWidth();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetClientTop();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetClientLeft();

            [return: MarshalAs(UnmanagedType.Bool)]
            bool AttachEvent(
                [In, MarshalAs(UnmanagedType.BStr)]
                string ev,
                [In, MarshalAs(UnmanagedType.Interface)]
                object pdisp);


            void DetachEvent(
                [In, MarshalAs(UnmanagedType.BStr)]
                string ev,
                [In, MarshalAs(UnmanagedType.Interface)]
                object pdisp);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetReadyState();


            void SetOnreadystatechange(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnreadystatechange();


            void SetOnrowsdelete(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnrowsdelete();


            void SetOnrowsinserted(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnrowsinserted();


            void SetOncellchange(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOncellchange();


            void SetDir(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetDir();

            [return: MarshalAs(UnmanagedType.Interface)]
            object CreateControlRange();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetScrollHeight();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetScrollWidth();


            void SetScrollTop(
                [In, MarshalAs(UnmanagedType.I4)]
                int p);

            [return: MarshalAs(UnmanagedType.I4)]
            int GetScrollTop();


            void SetScrollLeft(
                [In, MarshalAs(UnmanagedType.I4)]
                int p);

            [return: MarshalAs(UnmanagedType.I4)]
            int GetScrollLeft();


            void ClearAttributes();


            void MergeAttributes(
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLElement mergeThis);


            void SetOncontextmenu(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOncontextmenu();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement InsertAdjacentElement(
                [In, MarshalAs(UnmanagedType.BStr)]
                string whereText,
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLElement insertedElement);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement ApplyElement(
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLElement apply,
                [In, MarshalAs(UnmanagedType.BStr)]
                string whereText);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetAdjacentText(
                [In, MarshalAs(UnmanagedType.BStr)]
                string whereText);

            [return: MarshalAs(UnmanagedType.BStr)]
            string ReplaceAdjacentText(
                [In, MarshalAs(UnmanagedType.BStr)]
                string whereText,
                [In, MarshalAs(UnmanagedType.BStr)]
                string newText);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetCanHaveChildren();

            [return: MarshalAs(UnmanagedType.I4)]
            int AddBehavior(
                [In, MarshalAs(UnmanagedType.BStr)]
                string bstrUrl,
                [In]
                ref Object pvarFactory);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool RemoveBehavior(
                [In, MarshalAs(UnmanagedType.I4)]
                int cookie);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLStyle GetRuntimeStyle();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetBehaviorUrns();


            void SetTagUrn(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetTagUrn();


            void SetOnbeforeeditfocus(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnbeforeeditfocus();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetReadyStateValue();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElementCollection GetElementsByTagName(
                [In, MarshalAs(UnmanagedType.BStr)]
                string v);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLStyle GetBaseStyle();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLCurrentStyle GetBaseCurrentStyle();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLStyle GetBaseRuntimeStyle();


            void SetOnmousehover(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnmousehover();


            void SetOnkeydownpreview(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnkeydownpreview();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetBehavior(
                [In, MarshalAs(UnmanagedType.BStr)]
                string bstrName,
                [In, MarshalAs(UnmanagedType.BStr)]
                string bstrUrn);
        }

        #endregion
        [ComVisible(true), Guid("3050F21F-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLElementCollection 
        {

            [return: MarshalAs(UnmanagedType.BStr)]
            string toString();


            void SetLength(
                [In, MarshalAs(UnmanagedType.I4)]
                int p);

            [return: MarshalAs(UnmanagedType.I4)]
            int GetLength();

            [return: MarshalAs(UnmanagedType.Interface)]
            object Get_newEnum();

            [return: MarshalAs(UnmanagedType.Interface)]
            object Item(
                [In, MarshalAs(UnmanagedType.Struct)]
                object name,
                [In, MarshalAs(UnmanagedType.Struct)]
                object index);

            [return: MarshalAs(UnmanagedType.Interface)]
            object Tags(
                [In, MarshalAs(UnmanagedType.Struct)]
                object tagName);
        }



        [ComVisible(true), Guid("3050F6C9-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLElementDefaults 
        {

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLStyle GetStyle();

            void SetTabStop(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool v);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetTabStop();


            void SetViewInheritStyle(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool v);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetViewInheritStyle();


            void SetViewMasterTab(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool v);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetViewMasterTab();


            void SetScrollSegmentX(
                [In, MarshalAs(UnmanagedType.I4)]
                int v);

            [return: MarshalAs(UnmanagedType.I4)]
            int GetScrollSegmentX();


            void SetScrollSegmentY(
                [In, MarshalAs(UnmanagedType.I4)]
                object p);

            [return: MarshalAs(UnmanagedType.I4)]
            int GetScrollSegmentY();


            void SetIsMultiLine(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool v);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetIsMultiLine();


            void SetContentEditable(
                [In, MarshalAs(UnmanagedType.BStr)]
                string v);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetContentEditable();


            void SetCanHaveHTML(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool v);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetCanHaveHTML();


            void SetViewLink(
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLDocument viewLink);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLDocument GetViewLink();

            void SetFrozen(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool v);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetFrozen();
        }

        [ComVisible(true), Guid("3050F33C-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IHTMLElementEvents 
        {

            void Bogus1();


            void Bogus2();


            void Bogus3();


            void Invoke(
                [In, MarshalAs(UnmanagedType.U4)]
                int id,
                [In]
                ref Guid g,
                [In, MarshalAs(UnmanagedType.U4)]
                int lcid,
                [In, MarshalAs(UnmanagedType.U4)]
                int dwFlags,
                [In]
                DISPPARAMS pdp,
                [Out, MarshalAs(UnmanagedType.LPArray)]
                Object[] pvarRes,
                [Out]
                EXCEPINFO pei,
                [Out, MarshalAs(UnmanagedType.LPArray)]
                int[] nArgError);
        }

        [ComVisible(true), Guid("3050F32D-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLEventObj 
        {

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement GetSrcElement();

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetAltKey();

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetCtrlKey();

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetShiftKey();


            void SetReturnValue(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetReturnValue();


            void SetCancelBubble(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool p);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetCancelBubble();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement GetFromElement();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement GetToElement();


            void SetKeyCode(
                [In, MarshalAs(UnmanagedType.I4)]
                int p);

            [return: MarshalAs(UnmanagedType.I4)]
            int GetKeyCode();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetButton();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetEventType();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetQualifier();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetReason();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetX();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetY();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetClientX();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetClientY();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetOffsetX();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetOffsetY();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetScreenX();

            [return: MarshalAs(UnmanagedType.I4)]
            int GetScreenY();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetSrcFilter();

        }
        [ComVisible(true), Guid("3050F6A6-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IHTMLPainter 
        {


            void Draw(
                [In, MarshalAs(UnmanagedType.I4)]
                int leftBounds,
                [In, MarshalAs(UnmanagedType.I4)]
                int topBounds,
                [In, MarshalAs(UnmanagedType.I4)]
                int rightBounds,
                [In, MarshalAs(UnmanagedType.I4)]
                int bottomBounds,
                [In, MarshalAs(UnmanagedType.I4)]
                int leftUpdate,
                [In, MarshalAs(UnmanagedType.I4)]
                int topUpdate,
                [In, MarshalAs(UnmanagedType.I4)]
                int rightUpdate,
                [In, MarshalAs(UnmanagedType.I4)]
                int bottomUpdate,
                [In, MarshalAs(UnmanagedType.U4)]
                int lDrawFlags,
                [In]
                IntPtr hdc,
                [In]
                IntPtr pvDrawObject);


            void OnResize(
                [In, MarshalAs(UnmanagedType.I4)]
                int cx,
                [In, MarshalAs(UnmanagedType.I4)]
                int cy);


            void GetPainterInfo(
                [Out]
                HTML_PAINTER_INFO htmlPainterInfo);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool HitTestPoint(
                [In, MarshalAs(UnmanagedType.I4)]
                int ptx,
                [In, MarshalAs(UnmanagedType.I4)]
                int pty,
                [Out, MarshalAs(UnmanagedType.LPArray)]
                int[] pbHit,
                [Out, MarshalAs(UnmanagedType.LPArray)]
                int[] plPartID);
        }

        [ComVisible(true), Guid("3050f6a7-98b5-11cf-bb82-00aa00bdce0b"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IHTMLPaintSite 
        {


            void InvalidatePainterInfo();


            void InvalidateRect(
                [In]
                IntPtr pRect);
        }

        [ComVisible(true), Guid("3050F3CF-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLRuleStyle 
        {


            void SetFontFamily(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFontFamily();


            void SetFontStyle(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFontStyle();


            void SetFontObject(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFontObject();


            void SetFontWeight(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFontWeight();


            void SetFontSize(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetFontSize();


            void SetFont(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFont();


            void SetColor(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetColor();


            void SetBackground(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBackground();


            void SetBackgroundColor(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBackgroundColor();


            void SetBackgroundImage(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBackgroundImage();


            void SetBackgroundRepeat(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBackgroundRepeat();


            void SetBackgroundAttachment(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBackgroundAttachment();


            void SetBackgroundPosition(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBackgroundPosition();


            void SetBackgroundPositionX(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBackgroundPositionX();


            void SetBackgroundPositionY(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBackgroundPositionY();


            void SetWordSpacing(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetWordSpacing();


            void SetLetterSpacing(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetLetterSpacing();


            void SetTextDecoration(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetTextDecoration();


            void SetTextDecorationNone(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool p);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetTextDecorationNone();


            void SetTextDecorationUnderline(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool p);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetTextDecorationUnderline();


            void SetTextDecorationOverline(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool p);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetTextDecorationOverline();


            void SetTextDecorationLineThrough(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool p);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetTextDecorationLineThrough();


            void SetTextDecorationBlink(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool p);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetTextDecorationBlink();


            void SetVerticalAlign(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetVerticalAlign();


            void SetTextTransform(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetTextTransform();


            void SetTextAlign(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetTextAlign();


            void SetTextIndent(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetTextIndent();


            void SetLineHeight(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetLineHeight();


            void SetMarginTop(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetMarginTop();


            void SetMarginRight(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetMarginRight();


            void SetMarginBottom(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetMarginBottom();


            void SetMarginLeft(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetMarginLeft();


            void SetMargin(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetMargin();


            void SetPaddingTop(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetPaddingTop();


            void SetPaddingRight(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetPaddingRight();


            void SetPaddingBottom(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetPaddingBottom();


            void SetPaddingLeft(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetPaddingLeft();


            void SetPadding(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetPadding();


            void SetBorder(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorder();


            void SetBorderTop(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderTop();


            void SetBorderRight(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderRight();


            void SetBorderBottom(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderBottom();


            void SetBorderLeft(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderLeft();


            void SetBorderColor(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderColor();


            void SetBorderTopColor(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderTopColor();


            void SetBorderRightColor(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderRightColor();


            void SetBorderBottomColor(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderBottomColor();


            void SetBorderLeftColor(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderLeftColor();


            void SetBorderWidth(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderWidth();


            void SetBorderTopWidth(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderTopWidth();


            void SetBorderRightWidth(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderRightWidth();


            void SetBorderBottomWidth(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderBottomWidth();


            void SetBorderLeftWidth(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderLeftWidth();


            void SetBorderStyle(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderStyle();


            void SetBorderTopStyle(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderTopStyle();


            void SetBorderRightStyle(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderRightStyle();


            void SetBorderBottomStyle(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderBottomStyle();


            void SetBorderLeftStyle(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderLeftStyle();


            void SetWidth(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetWidth();


            void SetHeight(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetHeight();


            void SetStyleFloat(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetStyleFloat();


            void SetClear(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetClear();


            void SetDisplay(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetDisplay();


            void SetVisibility(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetVisibility();


            void SetListStyleType(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetListStyleType();


            void SetListStylePosition(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetListStylePosition();


            void SetListStyleImage(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetListStyleImage();


            void SetListStyle(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetListStyle();


            void SetWhiteSpace(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetWhiteSpace();


            void SetTop(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetTop();


            void SetLeft(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetLeft();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetPosition();


            void SetZIndex(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetZIndex();


            void SetOverflow(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetOverflow();


            void SetPageBreakBefore(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetPageBreakBefore();


            void SetPageBreakAfter(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetPageBreakAfter();


            void SetCssText(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetCssText();


            void SetCursor(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetCursor();


            void SetClip(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetClip();


            void SetFilter(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFilter();


            void SetAttribute(
                [In, MarshalAs(UnmanagedType.BStr)]
                string strAttributeName,
                [In, MarshalAs(UnmanagedType.Struct)]
                Object AttributeValue,
                [In, MarshalAs(UnmanagedType.I4)]
                int lFlags);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetAttribute(
                [In, MarshalAs(UnmanagedType.BStr)]
                string strAttributeName,
                [In, MarshalAs(UnmanagedType.I4)]
                int lFlags);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool RemoveAttribute(
                [In, MarshalAs(UnmanagedType.BStr)]
                string strAttributeName,
                [In, MarshalAs(UnmanagedType.I4)]
                int lFlags);

        }

        [ComVisible(true), Guid("3050F25A-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		public interface IHTMLSelectionObject 
        {
            [return: MarshalAs(UnmanagedType.Interface)]
            object CreateRange();
            void Empty();
            void Clear();
            [return: MarshalAs(UnmanagedType.BStr)]
            string GetSelectionType();
        }

        [ComVisible(true), Guid("3050f230-98b5-11cf-bb82-00aa00bdce0b"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch)]
		public interface IHTMLTextContainer 
        {
            [return: MarshalAs(UnmanagedType.IDispatch)]
            object createControlRange();
            int get_ScrollHeight();
            int get_ScrollWidth();
            int get_ScrollTop();
            int get_ScrollLeft();
            void put_ScrollHeight(int i);
            void put_ScrollWidth(int i);
            void put_ScrollTop(int i);
            void put_ScrollLeft(int i);
        }

        [ComVisible(true), Guid("3050F220-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLTxtRange 
        {

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetHtmlText();


            void SetText(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetText();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement ParentElement();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLTxtRange Duplicate();

            [return: MarshalAs(UnmanagedType.Bool)]
            bool InRange(
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLTxtRange range);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool IsEqual(
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLTxtRange range);


            void ScrollIntoView(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool fStart);


            void Collapse(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool Start);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool Expand(
                [In, MarshalAs(UnmanagedType.BStr)]
                string Unit);

            [return: MarshalAs(UnmanagedType.I4)]
            int Move(
                [In, MarshalAs(UnmanagedType.BStr)]
                string Unit,
                [In, MarshalAs(UnmanagedType.I4)]
                int Count);

            [return: MarshalAs(UnmanagedType.I4)]
            int MoveStart(
                [In, MarshalAs(UnmanagedType.BStr)]
                string Unit,
                [In, MarshalAs(UnmanagedType.I4)]
                int Count);

            [return: MarshalAs(UnmanagedType.I4)]
            int MoveEnd(
                [In, MarshalAs(UnmanagedType.BStr)]
                string Unit,
                [In, MarshalAs(UnmanagedType.I4)]
                int Count);


            void Select();


            void PasteHTML(
                [In, MarshalAs(UnmanagedType.BStr)]
                string html);


            void MoveToElementText(
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLElement element);


            void SetEndPoint(
                [In, MarshalAs(UnmanagedType.BStr)]
                string how,
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLTxtRange SourceRange);

            [return: MarshalAs(UnmanagedType.I4)]
            int CompareEndPoints(
                [In, MarshalAs(UnmanagedType.BStr)]
                string how,
                [In, MarshalAs(UnmanagedType.Interface)]
                IHTMLTxtRange SourceRange);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool FindText(
                [In, MarshalAs(UnmanagedType.BStr)]
                string String,
                [In, MarshalAs(UnmanagedType.I4)]
                int Count,
                [In, MarshalAs(UnmanagedType.I4)]
                int Flags);


            void MoveToPoint(
                [In, MarshalAs(UnmanagedType.I4)]
                int x,
                [In, MarshalAs(UnmanagedType.I4)]
                int y);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBookmark();

            [return: MarshalAs(UnmanagedType.Bool)]
            bool MoveToBookmark(
                [In, MarshalAs(UnmanagedType.BStr)]
                string Bookmark);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool QueryCommandSupported(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool QueryCommandEnabled(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool QueryCommandState(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool QueryCommandIndeterm(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.BStr)]
            string QueryCommandText(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.Struct)]
            object QueryCommandValue(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool ExecCommand(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID,
                [In, MarshalAs(UnmanagedType.Bool)]
                bool showUI,
                [In, MarshalAs(UnmanagedType.Struct)]
                object value);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool ExecCommandShowHelp(
                [In, MarshalAs(UnmanagedType.BStr)]
                string cmdID);
        }

        #region HTMLStyle and HTMLStyleSheet interfaces
        [ComVisible(true), Guid("3050F25E-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLStyle 
        {


            void SetFontFamily(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFontFamily();


            void SetFontStyle(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFontStyle();


            void SetFontObject(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFontObject();


            void SetFontWeight(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFontWeight();


            void SetFontSize(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetFontSize();


            void SetFont(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFont();


            void SetColor(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetColor();


            void SetBackground(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBackground();


            void SetBackgroundColor(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBackgroundColor();


            void SetBackgroundImage(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBackgroundImage();


            void SetBackgroundRepeat(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBackgroundRepeat();


            void SetBackgroundAttachment(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBackgroundAttachment();


            void SetBackgroundPosition(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBackgroundPosition();


            void SetBackgroundPositionX(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBackgroundPositionX();


            void SetBackgroundPositionY(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBackgroundPositionY();


            void SetWordSpacing(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetWordSpacing();


            void SetLetterSpacing(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetLetterSpacing();


            void SetTextDecoration(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetTextDecoration();


            void SetTextDecorationNone(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool p);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetTextDecorationNone();


            void SetTextDecorationUnderline(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool p);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetTextDecorationUnderline();


            void SetTextDecorationOverline(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool p);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetTextDecorationOverline();


            void SetTextDecorationLineThrough(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool p);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetTextDecorationLineThrough();


            void SetTextDecorationBlink(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool p);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetTextDecorationBlink();


            void SetVerticalAlign(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetVerticalAlign();


            void SetTextTransform(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetTextTransform();


            void SetTextAlign(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetTextAlign();


            void SetTextIndent(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetTextIndent();


            void SetLineHeight(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetLineHeight();


            void SetMarginTop(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetMarginTop();


            void SetMarginRight(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetMarginRight();


            void SetMarginBottom(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetMarginBottom();


            void SetMarginLeft(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetMarginLeft();


            void SetMargin(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetMargin();


            void SetPaddingTop(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetPaddingTop();


            void SetPaddingRight(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetPaddingRight();


            void SetPaddingBottom(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetPaddingBottom();


            void SetPaddingLeft(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetPaddingLeft();


            void SetPadding(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetPadding();


            void SetBorder(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorder();


            void SetBorderTop(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderTop();


            void SetBorderRight(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderRight();


            void SetBorderBottom(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderBottom();


            void SetBorderLeft(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderLeft();


            void SetBorderColor(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderColor();


            void SetBorderTopColor(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderTopColor();


            void SetBorderRightColor(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderRightColor();


            void SetBorderBottomColor(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderBottomColor();


            void SetBorderLeftColor(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderLeftColor();


            void SetBorderWidth(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderWidth();


            void SetBorderTopWidth(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderTopWidth();


            void SetBorderRightWidth(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderRightWidth();


            void SetBorderBottomWidth(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderBottomWidth();


            void SetBorderLeftWidth(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetBorderLeftWidth();


            void SetBorderStyle(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderStyle();


            void SetBorderTopStyle(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderTopStyle();


            void SetBorderRightStyle(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderRightStyle();


            void SetBorderBottomStyle(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderBottomStyle();


            void SetBorderLeftStyle(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetBorderLeftStyle();


            void SetWidth(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetWidth();


            void SetHeight(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetHeight();


            void SetStyleFloat(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetStyleFloat();


            void SetClear(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetClear();


            void SetDisplay(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetDisplay();


            void SetVisibility(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetVisibility();


            void SetListStyleType(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetListStyleType();


            void SetListStylePosition(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetListStylePosition();


            void SetListStyleImage(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetListStyleImage();


            void SetListStyle(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetListStyle();


            void SetWhiteSpace(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetWhiteSpace();


            void SetTop(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetTop();


            void SetLeft(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetLeft();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetPosition();


            void SetZIndex(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetZIndex();


            void SetOverflow(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetOverflow();


            void SetPageBreakBefore(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetPageBreakBefore();


            void SetPageBreakAfter(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetPageBreakAfter();


            void SetCssText(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetCssText();


            void SetPixelTop(
                [In, MarshalAs(UnmanagedType.I4)]
                int p);

            [return: MarshalAs(UnmanagedType.I4)]
            int GetPixelTop();


            void SetPixelLeft(
                [In, MarshalAs(UnmanagedType.I4)]
                int p);

            [return: MarshalAs(UnmanagedType.I4)]
            int GetPixelLeft();


            void SetPixelWidth(
                [In, MarshalAs(UnmanagedType.I4)]
                int p);

            [return: MarshalAs(UnmanagedType.I4)]
            int GetPixelWidth();


            void SetPixelHeight(
                [In, MarshalAs(UnmanagedType.I4)]
                int p);

            [return: MarshalAs(UnmanagedType.I4)]
            int GetPixelHeight();


            void SetPosTop(
                [In, MarshalAs(UnmanagedType.R4)]
                float p);

            [return: MarshalAs(UnmanagedType.R4)]
            float GetPosTop();


            void SetPosLeft(
                [In, MarshalAs(UnmanagedType.R4)]
                float p);

            [return: MarshalAs(UnmanagedType.R4)]
            float GetPosLeft();


            void SetPosWidth(
                [In, MarshalAs(UnmanagedType.R4)]
                float p);

            [return: MarshalAs(UnmanagedType.R4)]
            float GetPosWidth();


            void SetPosHeight(
                [In, MarshalAs(UnmanagedType.R4)]
                float p);

            [return: MarshalAs(UnmanagedType.R4)]
            float GetPosHeight();


            void SetCursor(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetCursor();


            void SetClip(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetClip();


            void SetFilter(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetFilter();


            void SetAttribute(
                [In, MarshalAs(UnmanagedType.BStr)]
                string strAttributeName,
                [In, MarshalAs(UnmanagedType.Struct)]
                Object AttributeValue,
                [In, MarshalAs(UnmanagedType.I4)]
                int lFlags);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetAttribute(
                [In, MarshalAs(UnmanagedType.BStr)]
                string strAttributeName,
                [In, MarshalAs(UnmanagedType.I4)]
                int lFlags);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool RemoveAttribute(
                [In, MarshalAs(UnmanagedType.BStr)]
                string strAttributeName,
                [In, MarshalAs(UnmanagedType.I4)]
                int lFlags);

        }

        [ComVisible(true), Guid("3050F2E3-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLStyleSheet 
        {


            void SetTitle(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetTitle();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLStyleSheet GetParentStyleSheet();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement GetOwningElement();


            void SetDisabled(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool p);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetDisabled();

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetReadOnly();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLStyleSheetsCollection GetImports();


            void SetHref(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetHref();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetStyleSheetType();

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetId();

            [return: MarshalAs(UnmanagedType.I4)]
            int AddImport(
                [In, MarshalAs(UnmanagedType.BStr)]
                string bstrURL,
                [In, MarshalAs(UnmanagedType.I4)]
                int lIndex);

            [return: MarshalAs(UnmanagedType.I4)]
            int AddRule(
                [In, MarshalAs(UnmanagedType.BStr)]
                string bstrSelector,
                [In, MarshalAs(UnmanagedType.BStr)]
                string bstrStyle,
                [In, MarshalAs(UnmanagedType.I4)]
                int lIndex);


            void RemoveImport(
                [In, MarshalAs(UnmanagedType.I4)]
                int lIndex);


            void RemoveRule(
                [In, MarshalAs(UnmanagedType.I4)]
                int lIndex);


            void SetMedia(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetMedia();


            void SetCssText(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetCssText();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLStyleSheetRulesCollection GetRules();

        }

        [ComVisible(true), Guid("3050F37E-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLStyleSheetsCollection 
        {

            [return: MarshalAs(UnmanagedType.I4)]
            int GetLength();

            [return: MarshalAs(UnmanagedType.Interface)]
            object Get_newEnum();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object Item(
                [In]
                ref Object pvarIndex);

        }

        [ComVisible(true), Guid("3050F357-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLStyleSheetRule 
        {


            void SetSelectorText(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetSelectorText();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLRuleStyle GetStyle();

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetReadOnly();

        }


        [ComVisible(true), Guid("3050F2E5-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLStyleSheetRulesCollection 
        {

            [return: MarshalAs(UnmanagedType.I4)]
            int GetLength();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLStyleSheetRule Item(
                [In, MarshalAs(UnmanagedType.I4)]
                int index);

        }

        #endregion
        [ComVisible(true), Guid("332C4427-26CB-11D0-B483-00C04FD90119"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLWindow2 
        {

            [return: MarshalAs(UnmanagedType.Struct)]
            Object Item(
                [In]
                ref Object pvarIndex);

            [return: MarshalAs(UnmanagedType.I4)]
            int GetLength();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetFrames();


            void SetDefaultStatus(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetDefaultStatus();


            void SetStatus(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetStatus();

            [return: MarshalAs(UnmanagedType.I4)]
            int SetTimeout(
                [In, MarshalAs(UnmanagedType.BStr)]
                string expression,
                [In, MarshalAs(UnmanagedType.I4)]
                int msec,
                [In]
                ref Object language);


            void ClearTimeout(
                [In, MarshalAs(UnmanagedType.I4)]
                int timerID);


            void Alert(
                [In, MarshalAs(UnmanagedType.BStr)]
                string message);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool Confirm(
                [In, MarshalAs(UnmanagedType.BStr)]
                string message);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object Prompt(
                [In, MarshalAs(UnmanagedType.BStr)]
                string message,
                [In, MarshalAs(UnmanagedType.BStr)]
                string defstr);

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetImage();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetLocation();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetHistory();


            void Close();


            void SetOpener(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOpener();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetNavigator();


            void SetName(
                [In, MarshalAs(UnmanagedType.BStr)]
                string p);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetName();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLWindow2 GetParent();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLWindow2 Open(
                [In, MarshalAs(UnmanagedType.BStr)]
                string URL,
                [In, MarshalAs(UnmanagedType.BStr)]
                string name,
                [In, MarshalAs(UnmanagedType.BStr)]
                string features,
                [In, MarshalAs(UnmanagedType.Bool)]
                bool replace);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLWindow2 GetSelf();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLWindow2 GetTop();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLWindow2 GetWindow();


            void Navigate(
                [In, MarshalAs(UnmanagedType.BStr)]
                string URL);


            void SetOnfocus(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnfocus();


            void SetOnblur(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnblur();


            void SetOnload(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnload();


            void SetOnbeforeunload(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnbeforeunload();


            void SetOnunload(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnunload();

            void SetOnhelp(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnhelp();

            void SetOnerror(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnerror();

            void SetOnresize(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnresize();

            void SetOnscroll(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOnscroll();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLDocument2 GetDocument();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLEventObj GetEvent();

            [return: MarshalAs(UnmanagedType.Interface)]
            object Get_newEnum();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object ShowModalDialog(
                [In, MarshalAs(UnmanagedType.BStr)]
                string dialog,
                [In]
                ref Object varArgIn,
                [In]
                ref Object varOptions);

            void ShowHelp(
                [In, MarshalAs(UnmanagedType.BStr)]
                string helpURL,
                [In, MarshalAs(UnmanagedType.Struct)]
                Object helpArg,
                [In, MarshalAs(UnmanagedType.BStr)]
                string features);

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetScreen();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetOption();

            void Focus();

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetClosed();

            void Blur();

            void Scroll(
                [In, MarshalAs(UnmanagedType.I4)]
                int x,
                [In, MarshalAs(UnmanagedType.I4)]
                int y);

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetClientInformation();

            [return: MarshalAs(UnmanagedType.I4)]
            int SetInterval(
                [In, MarshalAs(UnmanagedType.BStr)]
                string expression,
                [In, MarshalAs(UnmanagedType.I4)]
                int msec,
                [In]
                ref Object language);

            void ClearInterval(
                [In, MarshalAs(UnmanagedType.I4)]
                int timerID);

            void SetOffscreenBuffering(
                [In, MarshalAs(UnmanagedType.Struct)]
                Object p);

            [return: MarshalAs(UnmanagedType.Struct)]
            Object GetOffscreenBuffering();

            [return: MarshalAs(UnmanagedType.Struct)]
            Object ExecScript(
                [In, MarshalAs(UnmanagedType.BStr)]
                string code,
                [In, MarshalAs(UnmanagedType.BStr)]
                string language);

            void ScrollBy(
                [In, MarshalAs(UnmanagedType.I4)]
                int x,
                [In, MarshalAs(UnmanagedType.I4)]
                int y);

            void ScrollTo(
                [In, MarshalAs(UnmanagedType.I4)]
                int x,
                [In, MarshalAs(UnmanagedType.I4)]
                int y);

            void MoveTo(
                [In, MarshalAs(UnmanagedType.I4)]
                int x,
                [In, MarshalAs(UnmanagedType.I4)]
                int y);

            void MoveBy(
                [In, MarshalAs(UnmanagedType.I4)]
                int x,
                [In, MarshalAs(UnmanagedType.I4)]
                int y);

            void ResizeTo(
                [In, MarshalAs(UnmanagedType.I4)]
                int x,
                [In, MarshalAs(UnmanagedType.I4)]
                int y);

            void ResizeBy(
                [In, MarshalAs(UnmanagedType.I4)]
                int x,
                [In, MarshalAs(UnmanagedType.I4)]
                int y);

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetExternal();
        }

        [ComVisible(true), Guid("0000000F-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IMoniker 
        {

            // IPersistStream methods
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int IsDirty();

            void Load(
                [In, MarshalAs(UnmanagedType.Interface)]
                IStream pstm);

            void Save(
                [In, MarshalAs(UnmanagedType.Interface)]
                IStream pstm,
                [In, MarshalAs(UnmanagedType.Bool)]
                bool fClearDirty);

            [return: MarshalAs(UnmanagedType.I8)]
            long GetSizeMax();
            // End IPersistStream methods

            [return: MarshalAs(UnmanagedType.Interface)]
            object BindToObject(
                [In, MarshalAs(UnmanagedType.Interface)]
                object pbc,
                [In, MarshalAs(UnmanagedType.Interface)]
                IMoniker pMkToLeft,
                [In]
                ref Guid riidResult);

            [return: MarshalAs(UnmanagedType.Interface)]
            object BindToStorage(
                [In, MarshalAs(UnmanagedType.Interface)]
                object pbc,
                [In, MarshalAs(UnmanagedType.Interface)]
                IMoniker pMkToLeft,
                [In]
                ref Guid riidResult);

            [return: MarshalAs(UnmanagedType.Interface)]
            IMoniker Reduce(
                [In, MarshalAs(UnmanagedType.Interface)]
                object pbc,
                [In, MarshalAs(UnmanagedType.I4)]
                int dwReduceHowFar,
                [In, Out, MarshalAs(UnmanagedType.Interface)]
                IMoniker pMkToLeft);

            [return: MarshalAs(UnmanagedType.Interface)]
            IMoniker Reduce(
                [In, MarshalAs(UnmanagedType.Interface)]
                IMoniker pMkRight,
                [In, MarshalAs(UnmanagedType.Bool)]
                bool fOnlyIfNotGeneric);

            [return: MarshalAs(UnmanagedType.Interface)]
            object Reduce(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool fForward);


            void IsEqual(
                [In, MarshalAs(UnmanagedType.Interface)]
                IMoniker pOtherMoniker);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Hash();


            void IsRunning(
                [In, MarshalAs(UnmanagedType.Interface)]
                object pbc,
                [In, MarshalAs(UnmanagedType.Interface)]
                IMoniker pMkToLeft,
                [In, MarshalAs(UnmanagedType.Interface)]
                IMoniker pMkNewlyRunning);

            [return: MarshalAs(UnmanagedType.LPStruct)]
            object GetTimeOfLastChange(
                [In, MarshalAs(UnmanagedType.Interface)]
                object pbc,
                [In, MarshalAs(UnmanagedType.Interface)]
                IMoniker pMkToLeft);

            [return: MarshalAs(UnmanagedType.Interface)]
            IMoniker Inverse();

            [return: MarshalAs(UnmanagedType.Interface)]
            IMoniker CommonPrefixWith(
                [In, MarshalAs(UnmanagedType.Interface)]
                IMoniker pMkOther);

            [return: MarshalAs(UnmanagedType.Interface)]
            IMoniker RelativePathTo(
                [In, MarshalAs(UnmanagedType.Interface)]
                IMoniker pMkOther);

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetDisplayName(
                [In, MarshalAs(UnmanagedType.Interface)]
                object pbc,
                [In, MarshalAs(UnmanagedType.Interface)]
                IMoniker pMkOther);

            [return: MarshalAs(UnmanagedType.Interface)]
            IMoniker ParseDisplayName(
                [In, MarshalAs(UnmanagedType.Interface)]
                object pbc,
                [In, MarshalAs(UnmanagedType.Interface)]
                IMoniker pMkToLeft,
                [In, MarshalAs(UnmanagedType.BStr)]
                string pszDisplayName,
                [Out, MarshalAs(UnmanagedType.LPArray)]
                int[] pchEaten);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int IsSystemMoniker();
        }

        #region OLE interfaces
        [ComVisible(true), Guid("00000118-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleClientSite 
        {

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int SaveObject();

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetMoniker(
                [In, MarshalAs(UnmanagedType.U4)]          int dwAssign,
                [In, MarshalAs(UnmanagedType.U4)]          int dwWhichMoniker,
                [Out, MarshalAs(UnmanagedType.Interface)] out object ppmk);
			
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
			int GetContainer([MarshalAs(UnmanagedType.Interface)] out NativeMethods.IOleContainer container);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int ShowObject();

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int OnShowWindow(
                [In, MarshalAs(UnmanagedType.I4)] int fShow);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int RequestNewObjectLayout();
        }

        [ComVisible(true), ComImport(), Guid("B722BCCB-4E68-101B-A2BC-00AA00404770"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleCommandTarget 
        {

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int QueryStatus(
                ref Guid pguidCmdGroup,
                int cCmds,
                [In, Out] tagOLECMD prgCmds,
                [In, Out] int pCmdText);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Exec(
                ref Guid pguidCmdGroup,
                int nCmdID,
                int nCmdexecopt,
                // we need to have this an array because callers need to be able to specify NULL or VT_NULL
                [In, MarshalAs(UnmanagedType.LPArray)] object[] pvaIn,
                [Out, MarshalAs(UnmanagedType.LPArray)] object[] pvaOut);
        }

        [ComVisible(true), Guid("0000011B-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleContainer 
        {


            void ParseDisplayName(
                [In, MarshalAs(UnmanagedType.Interface)] object pbc,
                [In, MarshalAs(UnmanagedType.BStr)]      string pszDisplayName,
                [Out, MarshalAs(UnmanagedType.LPArray)] int[] pchEaten,
                [Out, MarshalAs(UnmanagedType.LPArray)] object[] ppmkOut);


            void EnumObjects(
                [In, MarshalAs(UnmanagedType.U4)]        int grfFlags,
                [Out, MarshalAs(UnmanagedType.LPArray)] object[] ppenum);


            void LockContainer(
                [In, MarshalAs(UnmanagedType.I4)] int fLock);
        }

        [ComVisible(true), ComImport(), Guid("0000010E-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleDataObject 
        {

            int OleGetData(
                FORMATETC pFormatetc,
                [Out]
                STGMEDIUM pMedium);


            int OleGetDataHere(
                FORMATETC pFormatetc,
                [In, Out]
                STGMEDIUM pMedium);


            int OleQueryGetData(
                FORMATETC pFormatetc);


            int OleGetCanonicalFormatEtc(
                FORMATETC pformatectIn,
                [Out]
                FORMATETC pformatetcOut);


            int OleSetData(
                FORMATETC pFormatectIn,
                STGMEDIUM pmedium,

                int fRelease);

            [return: MarshalAs(UnmanagedType.Interface)]
            IEnumFORMATETC OleEnumFormatEtc(
                [In, MarshalAs(UnmanagedType.U4)]
                int dwDirection);

            int OleDAdvise(
                FORMATETC pFormatetc,
                [In, MarshalAs(UnmanagedType.U4)]
                int advf,
                [In, MarshalAs(UnmanagedType.Interface)]
                object pAdvSink,
                [Out, MarshalAs(UnmanagedType.LPArray)]
                int[] pdwConnection);

            int OleDUnadvise(
                [In, MarshalAs(UnmanagedType.U4)]
                int dwConnection);

            int OleEnumDAdvise(
                [Out, MarshalAs(UnmanagedType.LPArray)]
                object[] ppenumAdvise);
        }

        [ComVisible(true), ComImport(), Guid("B722BCC7-4E68-101B-A2BC-00AA00404770"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleDocumentSite 
        {

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int ActivateMe(
                [In, MarshalAs(UnmanagedType.Interface)]
                IOleDocumentView pViewToActivate);


        }

        [ComVisible(true), Guid("B722BCC6-4E68-101B-A2BC-00AA00404770"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleDocumentView 
        {


            void SetInPlaceSite(
                [In, MarshalAs(UnmanagedType.Interface)]
                IOleInPlaceSite pIPSite);

            [return: MarshalAs(UnmanagedType.Interface)]
            IOleInPlaceSite GetInPlaceSite();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetDocument();


            void SetRect(
                [In]
                COMRECT prcView);


            void GetRect(
                [Out]
                COMRECT prcView);


            void SetRectComplex(
                [In]
                COMRECT prcView,
                [In]
                COMRECT prcHScroll,
                [In]
                COMRECT prcVScroll,
                [In]
                COMRECT prcSizeBox);


            void Show(
                [In, MarshalAs(UnmanagedType.I4)]
                int fShow);


            void UIActivate(
                [In, MarshalAs(UnmanagedType.I4)]
                int fUIActivate);


            void Open();


            void CloseView(
                [In, MarshalAs(UnmanagedType.U4)]
                int dwReserved);


            void SaveViewState(
                [In, MarshalAs(UnmanagedType.Interface)]
                IStream pstm);


            void ApplyViewState(
                [In, MarshalAs(UnmanagedType.Interface)]
                IStream pstm);


            void Clone(
                [In, MarshalAs(UnmanagedType.Interface)]
                IOleInPlaceSite pIPSiteNew,
                [Out, MarshalAs(UnmanagedType.LPArray)]
                IOleDocumentView[] ppViewNew);


        }

        [ComVisible(true), ComImport(), Guid("00000121-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleDropSource 
        {

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int QueryContinueDrag(
                [In, MarshalAs(UnmanagedType.I4)]
                int fEscapePressed,
                [In, MarshalAs(UnmanagedType.U4)]
                int grfKeyState);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GiveFeedback(
                [In, MarshalAs(UnmanagedType.U4)]
                int dwEffect);
        }

        [ComVisible(true), ComImport(), Guid("00000122-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleDropTarget 
        {

            [PreserveSig]
            int OleDragEnter(
                //[In, MarshalAs(UnmanagedType.Interface)]
                IntPtr pDataObj,
                [In, MarshalAs(UnmanagedType.U4)]
                int grfKeyState,
                [In, MarshalAs(UnmanagedType.U8)]
                long pt,
                [In, Out]
                ref int pdwEffect);

            [PreserveSig]
            int OleDragOver(
                [In, MarshalAs(UnmanagedType.U4)]
                int grfKeyState,
                [In, MarshalAs(UnmanagedType.U8)]
                long pt,
                [In, Out]
                ref int pdwEffect);

            [PreserveSig]
            int OleDragLeave();

            [PreserveSig]
            int OleDrop(
                //[In, MarshalAs(UnmanagedType.Interface)]
                IntPtr pDataObj,
                [In, MarshalAs(UnmanagedType.U4)]
                int grfKeyState,
                [In, MarshalAs(UnmanagedType.U8)]
                long pt,
                [In, Out]
                ref int pdwEffect);
        }

        [ComVisible(true), ComImport(), Guid("00000117-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleInPlaceActiveObject 
        {

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetWindow(out IntPtr hwnd);


            void ContextSensitiveHelp(
                [In, MarshalAs(UnmanagedType.I4)]
                int fEnterMode);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int TranslateAccelerator(
                [In, MarshalAs(UnmanagedType.LPStruct)] COMMSG lpmsg);


            void OnFrameWindowActivate(
                [In, MarshalAs(UnmanagedType.I4)]
                int fActivate);


            void OnDocWindowActivate(
                [In, MarshalAs(UnmanagedType.I4)]
                int fActivate);


            void ResizeBorder(
                [In]
                COMRECT prcBorder,
                [In]
                IOleInPlaceUIWindow pUIWindow,
                [In, MarshalAs(UnmanagedType.I4)]
                int fFrameWindow);


            void EnableModeless(
                [In, MarshalAs(UnmanagedType.I4)]
                int fEnable);


        }

        [ComVisible(true), ComImport(), Guid("00000116-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleInPlaceFrame 
        {


            IntPtr GetWindow();


            void ContextSensitiveHelp(
                [In, MarshalAs(UnmanagedType.I4)]
                int fEnterMode);


            void GetBorder(
                [Out]
                COMRECT lprectBorder);


            void RequestBorderSpace(
                [In]
                COMRECT pborderwidths);


            void SetBorderSpace(
                [In]
                COMRECT pborderwidths);


            void SetActiveObject(
                [In, MarshalAs(UnmanagedType.Interface)]
                IOleInPlaceActiveObject pActiveObject,
                [In, MarshalAs(UnmanagedType.LPWStr)]
                string pszObjName);


            void InsertMenus(
                [In]
                IntPtr hmenuShared,
                [In, Out]
                tagOleMenuGroupWidths lpMenuWidths);


            void SetMenu(
                [In]
                IntPtr hmenuShared,
                [In]
                IntPtr holemenu,
                [In]
                IntPtr hwndActiveObject);


            void RemoveMenus(
                [In]
                IntPtr hmenuShared);


            void SetStatusText(
                [In, MarshalAs(UnmanagedType.BStr)]
                string pszStatusText);


            void EnableModeless(
                [In, MarshalAs(UnmanagedType.I4)]
                int fEnable);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int TranslateAccelerator(
                [In, MarshalAs(UnmanagedType.LPStruct)]
                COMMSG lpmsg,
                [In, MarshalAs(UnmanagedType.U2)]
                short wID);


        }

        [ComVisible(true), ComImport(), Guid("00000113-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleInPlaceObject 
        {

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetWindow([Out]out IntPtr hwnd);


            void ContextSensitiveHelp(
                [In, MarshalAs(UnmanagedType.I4)]
                int fEnterMode);


            void InPlaceDeactivate();

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int UIDeactivate();


            void SetObjectRects(
                [In]
                COMRECT lprcPosRect,
                [In]
                COMRECT lprcClipRect);


            void ReactivateAndUndo();


        }

        [ComVisible(true), ComImport(), Guid("00000119-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleInPlaceSite 
        {

            IntPtr GetWindow();

            void ContextSensitiveHelp(
                [In, MarshalAs(UnmanagedType.I4)]
                int fEnterMode);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int CanInPlaceActivate();


            void OnInPlaceActivate();


            void OnUIActivate();


            void GetWindowContext(
                [Out]
                out IOleInPlaceFrame ppFrame,
                [Out]
                out IOleInPlaceUIWindow ppDoc,
                [Out]
                COMRECT lprcPosRect,
                [Out]
                COMRECT lprcClipRect,
                [In, Out]
                tagOIFI lpFrameInfo);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Scroll(
                [In, MarshalAs(UnmanagedType.U4)]
                tagSIZE scrollExtent);


            void OnUIDeactivate(
                [In, MarshalAs(UnmanagedType.I4)]
                int fUndoable);


            void OnInPlaceDeactivate();


            void DiscardUndoState();


            void DeactivateAndUndo();

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int OnPosRectChange(
                [In]
                COMRECT lprcPosRect);
        }

        [ComVisible(true), Guid("00000115-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleInPlaceUIWindow 
        {

            //C#r: UNDONE (Field in interface) public static readonly    Guid iid;

            IntPtr GetWindow();


            void ContextSensitiveHelp(
                [In, MarshalAs(UnmanagedType.I4)]
                int fEnterMode);


            void GetBorder(
                [Out]
                COMRECT lprectBorder);


            void RequestBorderSpace(
                [In]
                COMRECT pborderwidths);


            void SetBorderSpace(
                [In]
                COMRECT pborderwidths);


            void SetActiveObject(
                [In, MarshalAs(UnmanagedType.Interface)]
                IOleInPlaceActiveObject pActiveObject,
                [In, MarshalAs(UnmanagedType.LPWStr)]
                string pszObjName);


        }

        [ComVisible(true), ComImport(), Guid("00000112-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleObject 
        {

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int SetClientSite(
                [In, MarshalAs(UnmanagedType.Interface)]
                IOleClientSite pClientSite);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetClientSite(out IOleClientSite site);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int SetHostNames(
                [In, MarshalAs(UnmanagedType.LPWStr)]
                string szContainerApp,
                [In, MarshalAs(UnmanagedType.LPWStr)]
                string szContainerObj);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Close(
                [In, MarshalAs(UnmanagedType.I4)]
                int dwSaveOption);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int SetMoniker(
                [In, MarshalAs(UnmanagedType.U4)]
                int dwWhichMoniker,
                [In, MarshalAs(UnmanagedType.Interface)]
                object pmk);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetMoniker(
                [In, MarshalAs(UnmanagedType.U4)]
                int dwAssign,
                [In, MarshalAs(UnmanagedType.U4)]
                int dwWhichMoniker,
                out object moniker);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int InitFromData(
                [In, MarshalAs(UnmanagedType.Interface)]
                IOleDataObject pDataObject,
                [In, MarshalAs(UnmanagedType.I4)]
                int fCreation,
                [In, MarshalAs(UnmanagedType.U4)]
                int dwReserved);

            int GetClipboardData(
                [In, MarshalAs(UnmanagedType.U4)]
                int dwReserved,
                out IOleDataObject data);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int DoVerb(
                [In, MarshalAs(UnmanagedType.I4)]
                int iVerb,
                [In]
                IntPtr lpmsg,
                [In, MarshalAs(UnmanagedType.Interface)]
                IOleClientSite pActiveSite,
                [In, MarshalAs(UnmanagedType.I4)]
                int lindex,
                [In]
                IntPtr hwndParent,
                [In]
                COMRECT lprcPosRect);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int EnumVerbs(out IEnumOLEVERB e);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int OleUpdate();

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int IsUpToDate();

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetUserClassID(
                [In, Out]
                ref Guid pClsid);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetUserType(
                [In, MarshalAs(UnmanagedType.U4)]
                int dwFormOfType,
                [Out, MarshalAs(UnmanagedType.LPWStr)]
                out string userType);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int SetExtent(
                [In, MarshalAs(UnmanagedType.U4)]
                int dwDrawAspect,
                [In]
                tagSIZEL pSizel);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetExtent(
                [In, MarshalAs(UnmanagedType.U4)]
                int dwDrawAspect,
                [Out]
                tagSIZEL pSizel);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Advise([In, MarshalAs(UnmanagedType.Interface)] IAdviseSink pAdvSink, out int cookie);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Unadvise([In, MarshalAs(UnmanagedType.U4)] int dwConnection);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int EnumAdvise(out IEnumSTATDATA e);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetMiscStatus([In, MarshalAs(UnmanagedType.U4)] int dwAspect, out int misc);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int SetColorScheme([In] tagLOGPALETTE pLogpal);
        }

        [ComVisible(true), Guid("A1FAF330-EF97-11CE-9BC9-00AA00608E01"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleParentUndoUnit : IOleUndoUnit 
        {
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Open([In, MarshalAs(UnmanagedType.Interface)] IOleParentUndoUnit parentUnit);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Close([In, MarshalAs(UnmanagedType.Interface)] IOleParentUndoUnit parentUnit, [In, MarshalAs(UnmanagedType.Bool)] bool fCommit);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Add([In, MarshalAs(UnmanagedType.Interface)] IOleUndoUnit undoUnit);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int FindUnit([In, MarshalAs(UnmanagedType.Interface)] IOleUndoUnit undoUnit);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetParentState([Out, MarshalAs(UnmanagedType.I8)] out long state);
        }
        [ComVisible(true), ComImport(), Guid("6D5140C1-7436-11CE-8034-00AA006009FA"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleServiceProvider 
        {
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int QueryService([In] ref Guid guidService, [In] ref Guid riid, out IntPtr ppvObject);
        }

        [ComVisible(true), Guid("D001F200-EF97-11CE-9BC9-00AA00608E01"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleUndoManager 
        {
            void Open([In, MarshalAs(UnmanagedType.Interface)] IOleParentUndoUnit parentUndo);
            void Close([In, MarshalAs(UnmanagedType.Interface)] IOleParentUndoUnit parentUndo, [In, MarshalAs(UnmanagedType.Bool)] bool fCommit);
            void Add([In, MarshalAs(UnmanagedType.Interface)] IOleUndoUnit undoUnit);
            [return: MarshalAs(UnmanagedType.I8)]
            long GetOpenParentState();
            void DiscardFrom([In, MarshalAs(UnmanagedType.Interface)] IOleUndoUnit undoUnit);
            void UndoTo([In, MarshalAs(UnmanagedType.Interface)] IOleUndoUnit undoUnit);
            void RedoTo([In, MarshalAs(UnmanagedType.Interface)] IOleUndoUnit undoUnit);
            [return: MarshalAs(UnmanagedType.Interface)]
            IEnumOleUndoUnits EnumUndoable();
            [return: MarshalAs(UnmanagedType.Interface)]
            IEnumOleUndoUnits EnumRedoable();
            [return: MarshalAs(UnmanagedType.BStr)]
            string GetLastUndoDescription();
            [return: MarshalAs(UnmanagedType.BStr)]
            string GetLastRedoDescription();
            void Enable([In, MarshalAs(UnmanagedType.Bool)] bool fEnable);
        }

        [ComVisible(true), Guid("894AD3B0-EF97-11CE-9BC9-00AA00608E01"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleUndoUnit  
        {
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Do([In, MarshalAs(UnmanagedType.Interface)] IOleUndoManager undoManager);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetDescription([Out, MarshalAs(UnmanagedType.BStr)] out string bStr);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetUnitType([Out, MarshalAs(UnmanagedType.I4)] out int clsid, [Out, MarshalAs(UnmanagedType.I4)] out int plID);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int OnNextAdd();
        }
        #endregion
        [ComVisible(true), Guid("79eac9c9-baf9-11ce-8c82-00aa004ba90b"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IPersistMoniker 
        {
            void GetClassID([In, Out] ref Guid pClassID);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int IsDirty();
            void Load([In] int fFullyAvailable, [In, MarshalAs(UnmanagedType.Interface)] IMoniker pmk, [In, MarshalAs(UnmanagedType.Interface)] object pbc, [In] int grfMode);
            void Save([In, MarshalAs(UnmanagedType.Interface)] IMoniker pimkName, [In, MarshalAs(UnmanagedType.Interface)] object pbc, [In, MarshalAs(UnmanagedType.Bool)] bool fRemember);
            void SaveCompleted([In, MarshalAs(UnmanagedType.Interface)] IMoniker pmk, [In, MarshalAs(UnmanagedType.Interface)] object pbc);
            [return: MarshalAs(UnmanagedType.Interface)]
            IMoniker GetCurMoniker();
        }

        [ComVisible(true), ComImport(), Guid("7FD52380-4E07-101B-AE2D-08002B2EC713"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		public interface IPersistStreamInit 
        {
            void GetClassID([In, Out] ref Guid pClassID);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int IsDirty();
            void Load([In, MarshalAs(UnmanagedType.Interface)] IStream pstm);
            void Save([In, MarshalAs(UnmanagedType.Interface)] IStream pstm, [In, MarshalAs(UnmanagedType.I4)] int fClearDirty);
            void GetSizeMax([Out, MarshalAs(UnmanagedType.LPArray)] long pcbSize);
            void InitNew();
        }

        [ComVisible(true), Guid("9BFBBC02-EFF1-101A-84ED-00AA00341D07"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		public interface IPropertyNotifySink 
        {
            void OnChanged(int dispID);
            void OnRequestEdit(int dispID);
        }

        [ComVisible(true), Guid("6D5140C1-7436-11CE-8034-00AA006009FA"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		public interface IServiceProvider 
        {
            [return: MarshalAs(UnmanagedType.I4)]
            int QueryService([In] ref Guid sid, [In] ref Guid iid, out IntPtr service);
        }

		[ComVisible(true), Guid("0000000C-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		public interface IStream 
        {
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Read([In] IntPtr buf, [In, MarshalAs(UnmanagedType.I4)] int len);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Write([In] IntPtr buf, [In, MarshalAs(UnmanagedType.I4)] int len);
            [return: MarshalAs(UnmanagedType.I8)]
            long Seek([In, MarshalAs(UnmanagedType.I8)] long dlibMove, [In, MarshalAs(UnmanagedType.I4)] int dwOrigin);
            void SetSize([In, MarshalAs(UnmanagedType.I8)] long libNewSize);
            [return: MarshalAs(UnmanagedType.I8)]
            long CopyTo([In, MarshalAs(UnmanagedType.Interface)] IStream pstm, [In, MarshalAs(UnmanagedType.I8)] long cb, [Out, MarshalAs(UnmanagedType.LPArray)] long[] pcbRead);
            void Commit([In, MarshalAs(UnmanagedType.I4)] int grfCommitFlags);
            void Revert();
            void LockRegion([In, MarshalAs(UnmanagedType.I8)] long libOffset, [In, MarshalAs(UnmanagedType.I8)] long cb, [In, MarshalAs(UnmanagedType.I4)] int dwLockType);
			void UnlockRegion([In, MarshalAs(UnmanagedType.I8)] long libOffset, [In, MarshalAs(UnmanagedType.I8)] long cb, [In, MarshalAs(UnmanagedType.I4)] int dwLockType);
            void Stat([Out] STATSTG pstatstg, [In, MarshalAs(UnmanagedType.I4)] int grfStatFlag);
            [return: MarshalAs(UnmanagedType.Interface)]
            IStream Clone();
        }

        #endregion

	
		[ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
		internal interface IConnectionPoint
		{
			void GetConnectionInterface(out Guid interfaceIdentifier);


			void GetConnectionPointContainer(out IConnectionPointContainer container);


			void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int cookie);


			void Unadvise(int cookie);


			void EnumConnections(out object enumerator);
		}



		[ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
		internal interface IConnectionPointContainer
		{
			void EnumConnectionPoints(out object enumerator);


			void FindConnectionPoint([In] ref Guid riid, out IConnectionPoint connectionPoint);
		}
	}
}
