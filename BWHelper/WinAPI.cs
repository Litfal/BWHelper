using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Litfal
{
    static public class WinAPI
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            private int _Left;
            private int _Top;
            private int _Right;
            private int _Bottom;

            public RECT(System.Drawing.Rectangle Rectangle)
                : this(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom)
            {
            }
            public RECT(int Left, int Top, int Right, int Bottom)
            {
                _Left = Left;
                _Top = Top;
                _Right = Right;
                _Bottom = Bottom;
            }

            public int X
            {
                get { return _Left; }
                set { _Left = value; }
            }
            public int Y
            {
                get { return _Top; }
                set { _Top = value; }
            }
            public int Left
            {
                get { return _Left; }
                set { _Left = value; }
            }
            public int Top
            {
                get { return _Top; }
                set { _Top = value; }
            }
            public int Right
            {
                get { return _Right; }
                set { _Right = value; }
            }
            public int Bottom
            {
                get { return _Bottom; }
                set { _Bottom = value; }
            }
            public int Height
            {
                get { return _Bottom - _Top; }
                set { _Bottom = value - _Top; }
            }
            public int Width
            {
                get { return _Right - _Left; }
                set { _Right = value + _Left; }
            }
            public System.Drawing.Point Location
            {
                get { return new System.Drawing.Point(Left, Top); }
                set
                {
                    _Left = value.X;
                    _Top = value.Y;
                }
            }
            public System.Drawing.Size Size
            {
                get { return new System.Drawing.Size(Width, Height); }
                set
                {
                    _Right = value.Height + _Left;
                    _Bottom = value.Height + _Top;
                }
            }

            public System.Drawing.Rectangle ToRectangle()
            {
                return new System.Drawing.Rectangle(this.Left, this.Top, this.Width, this.Height);
            }
            public static System.Drawing.Rectangle ToRectangle(RECT Rectangle)
            {
                return Rectangle.ToRectangle();
            }
            public static RECT FromRectangle(System.Drawing.Rectangle Rectangle)
            {
                return new RECT(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
            }

            public static implicit operator System.Drawing.Rectangle(RECT Rectangle)
            {
                return Rectangle.ToRectangle();
            }
            public static implicit operator RECT(System.Drawing.Rectangle Rectangle)
            {
                return new RECT(Rectangle);
            }
            public static bool operator ==(RECT Rectangle1, RECT Rectangle2)
            {
                return Rectangle1.Equals(Rectangle2);
            }
            public static bool operator !=(RECT Rectangle1, RECT Rectangle2)
            {
                return !Rectangle1.Equals(Rectangle2);
            }

            public override string ToString()
            {
                return "{Left: " + _Left + "; " + "Top: " + _Top + "; Right: " + _Right + "; Bottom: " + _Bottom + "}";
            }

            public bool Equals(RECT Rectangle)
            {
                return Rectangle.Left == _Left && Rectangle.Top == _Top && Rectangle.Right == _Right && Rectangle.Bottom == _Bottom;
            }
            public override bool Equals(object Object)
            {
                if (Object is RECT)
                {
                    return Equals((RECT)Object);
                }
                else if (Object is System.Drawing.Rectangle)
                {
                    return Equals(new RECT((System.Drawing.Rectangle)Object));
                }

                return false;
            }

            public override int GetHashCode()
            {
                return Left.GetHashCode() ^ Right.GetHashCode() ^ Top.GetHashCode() ^ Bottom.GetHashCode();
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT lpPoint);
        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hwnd, out RECT rc);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, GetWindow_Cmd uCmd);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, Int32 wParam, Int32 lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr PostMessage(IntPtr hWnd, Int32 Msg, Int32 wParam, Int32 lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr PostMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(System.Drawing.Point pnt);

        /// <summary>
        /// The MoveWindow function changes the position and dimensions of the specified window. For a top-level window, the position and dimensions are relative to the upper-left corner of the screen. For a child window, they are relative to the upper-left corner of the parent window's client area.
        /// </summary>
        /// <param name="hWnd">Handle to the window.</param>
        /// <param name="X">Specifies the new position of the left side of the window.</param>
        /// <param name="Y">Specifies the new position of the top of the window.</param>
        /// <param name="nWidth">Specifies the new width of the window.</param>
        /// <param name="nHeight">Specifies the new height of the window.</param>
        /// <param name="bRepaint">Specifies whether the window is to be repainted. If this parameter is TRUE, the window receives a message. If the parameter is FALSE, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of moving a child window.</param>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// <para>If the function fails, the return value is zero. To get extended error information, call GetLastError.</para></returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, IntPtr className, IntPtr windowTitle);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, IntPtr className, string windowTitle);

        public delegate bool EnumWindowsProc(IntPtr callback, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int EnumWindows(EnumWindowsProc hWnd, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware();

        public const UInt32 SWP_NOSIZE = 0x0001;
        public const UInt32 SWP_NOMOVE = 0x0002;
        public const UInt32 SWP_NOZORDER = 0x0004;
        public const UInt32 SWP_NOREDRAW = 0x0008;
        public const UInt32 SWP_NOACTIVATE = 0x0010;
        public const UInt32 SWP_FRAMECHANGED = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
        public const UInt32 SWP_SHOWWINDOW = 0x0040;
        public const UInt32 SWP_HIDEWINDOW = 0x0080;
        public const UInt32 SWP_NOCOPYBITS = 0x0100;
        public const UInt32 SWP_NOOWNERZORDER = 0x0200;  /* Don't do owner Z ordering */
        public const UInt32 SWP_NOSENDCHANGING = 0x0400;  /* Don't send WM_WINDOWPOSCHANGING */

        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        public static readonly IntPtr HWND_TOP = new IntPtr(0);


        //struct rect
        //{
        //    long X;
        //    long Y;
        //    long Width;
        //    long Height;
        //}
        public enum KEYEVENTF : uint
        {
            KEYDOWN = 0x00,
            EXTENDEDKEY = 0x01,
            KEYUP = 0x02,
        }

        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x0002,
            LEFTUP = 0x0004,
            MIDDLEDOWN = 0x0020,
            MIDDLEUP = 0x0040,
            WHEEL = 0x0800,
            MOVE = 0x0001,
            ABSOLUTE = 0x8000,
            RIGHTDOWN = 0x0008,
            RIGHTUP = 0x0010
        }

        public enum SW
        {
            SW_HIDE = 0,
            SW_NORMAL = 1,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10
        }

        public enum VirtualKey : int
        {
            VK_PRIOR = 0x21,
            VK_NEXT = 0x22,
            VK_PAGEDOWN = 34,
            VK_PAGEUP = 33,
        }

        public enum MouseKey : int
        {
            MK_CONTROL = 0x0008,
            MK_LBUTTON = 0x0001,
            MK_MBUTTON = 0x0010,
            MK_RBUTTON = 0x0002,
            MK_SHIFT = 0x0004,
            MK_XBUTTON1 = 0x0020,
            MK_XBUTTON2 = 0x0040,
        }

        public enum ScrollBarCommands : int
        {
            SB_LINEUP = 0x0,
            SB_LINEDOWN = 0x1,
        }

        public enum WindowMessage : int
        {
            WM_VSCROLL = 0x115,
            WM_HSCROLL = 0x114,
            WM_KEYDOWN = 0x100,
            WM_KEYUP = 0x101,
            WM_MOUSEMOVE = 0x0200,
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_LBUTTONDBLCLK = 0x0203,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_RBUTTONDBLCLK = 0x0206,
            WM_MBUTTONDOWN = 0x0207,
            WM_MBUTTONUP = 0x0208,
            WM_MBUTTONDBLCLK = 0x0209,
            WM_MOUSEWHEEL = 0x020A,
        }

        public enum GetWindow_Cmd : uint
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6
        }

        public enum WindowLongIndex : int
        {
            /// <summary> 得到窗口回调函数的地址，或者句柄。得到后必须使用CallWindowProc函数来调用 </summary>
            GWL_WNDPROC = -4,
            /// <summary> 得到应用程序运行实例的句柄 </summary>
            GWL_HINSTANCE = -6,
            /// <summary> 得到父窗口的句柄 </summary>
            GWL_HWNDPARENT = -8,
            /// <summary> 得到窗口的标识符 </summary>  
            GWL_ID = -12,
            /// <summary> window的基本样式 </summary>   
            GWL_STYLE = -16,
            /// <summary> window的扩展样式 </summary>  
            GWL_EXSTYLE = -20,
            /// <summary> 得到和窗口相关联的32位的值（每一个窗口都有一个有意留给创建窗口的应用程序是用的32位的值 </summary>  
            GWL_USERDATA = -21,
            /// <summary> 得到对话框回调函数中消息处理过程的返回值 </summary>  
            DWL_MSGRESULT = 0,
            /// <summary> 得到对话框回调函数的地址，或者句柄。得到后必须使用CallWindowProc函数来调用 </summary>  
            DWL_DLGPROC = 4,
            /// <summary> 得到额外的应用程序私有信息，如一些句柄和指针等 </summary>  
            DWL_USER = 8,
        }


        [Flags]
        public enum WindowStyles : uint
        {
            OVERLAPPED = 0,
            POPUP = 0x80000000,
            CHILD = 0x40000000,
            MINIMIZE = 0x20000000,
            VISIBLE = 0x10000000,
            DISABLED = 0x8000000,
            CLIPSIBLINGS = 0x4000000,
            CLIPCHILDREN = 0x2000000,
            MAXIMIZE = 0x1000000,
            CAPTION = 0xC00000,
            BORDER = 0x800000,
            DLGFRAME = 0x400000,
            VSCROLL = 0x200000,
            HSCROLL = 0x100000,
            SYSMENU = 0x80000,
            THICKFRAME = 0x40000,
            GROUP = 0x20000,
            TABSTOP = 0x10000,
            MINIMIZEBOX = 0x20000,
            MAXIMIZEBOX = 0x10000,
            TILED = OVERLAPPED,
            ICONIC = MINIMIZE,
            SIZEBOX = THICKFRAME,
        }

        [Flags]
        public enum ExtendedWindowStyles : uint
        {
            DLGMODALFRAME = 0x0001,
            NOPARENTNOTIFY = 0x0004,
            TOPMOST = 0x0008,
            ACCEPTFILES = 0x0010,
            TRANSPARENT = 0x0020,
            MDICHILD = 0x0040,
            TOOLWINDOW = 0x0080,
            WINDOWEDGE = 0x0100,
            CLIENTEDGE = 0x0200,
            CONTEXTHELP = 0x0400,
            RIGHT = 0x1000,
            LEFT = 0x0000,
            RTLREADING = 0x2000,
            LTRREADING = 0x0000,
            LEFTSCROLLBAR = 0x4000,
            RIGHTSCROLLBAR = 0x0000,
            CONTROLPARENT = 0x10000,
            STATICEDGE = 0x20000,
            APPWINDOW = 0x40000,
            OVERLAPPEDWINDOW = (WINDOWEDGE | CLIENTEDGE),
            PALETTEWINDOW = (WINDOWEDGE | TOOLWINDOW | TOPMOST),
            LAYERED = 0x00080000,
            /// <summary> Disable inheritence of mirroring by children </summary>
            NOINHERITLAYOUT = 0x00100000,
            /// <summary> Right to left mirroring </summary>
            LAYOUTRTL = 0x00400000,
            COMPOSITED = 0x02000000,
            NOACTIVATE = 0x08000000,
        }

        /// <summary>   
        /// 带有alpha的样式   
        /// </summary>   
        public const long LWA_ALPHA = 0x00000002L;



        /// <summary>   
        /// 颜色设置   
        /// </summary>   
        public const long LWA_COLORKEY = 0x00000001L;




        /// <summary>
        /// 设置窗体的样式
        /// </summary>
        /// <param name="handle">操作窗体的句柄</param>
        /// <param name="nIndex">进行设置窗体的样式类型.</param>
        /// <param name="newStyle">新样式</param>
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern void SetWindowLong(IntPtr hWnd, WindowLongIndex nIndex, long dwNewLong);



        /// <summary>   
        /// 获取窗体指定的样式.   
        /// </summary>   
        /// <param name="handle">操作窗体的句柄</param>   
        /// <param name="nIndex">要进行返回的样式</param>   
        /// <returns>当前window的样式</returns>
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern long GetWindowLong(IntPtr hWnd, WindowLongIndex nIndex);



        /// <summary>
        /// 设置窗体的工作区域.
        /// </summary>
        /// <param name="handle">操作窗体的句柄.</param>
        /// <param name="handleRegion">操作窗体区域的句柄.</param>
        /// <param name="regraw">if set to <c>true</c> [regraw].</param>
        /// <returns>返回值</returns>   

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern int SetWindowRgn(IntPtr hWnd, IntPtr handleRegion, bool regraw);



        /// <summary>
        /// 创建带有圆角的区域.
        /// </summary>
        /// <param name="x1">左上角坐标的X值.</param>
        /// <param name="y1">左上角坐标的Y值.</param>
        /// <param name="x2">右下角坐标的X值.</param>
        /// <param name="y2">右下角坐标的Y值.</param>
        /// <param name="width">圆角椭圆的width.</param>
        /// <param name="height">圆角椭圆的height.</param>
        /// <returns>hRgn的句柄</returns>
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int width, int height);



        /// <summary>
        /// Sets the layered window attributes.
        /// </summary>
        /// <param name="handle">要进行操作的窗口句柄</param>   
        /// <param name="colorKey">RGB的值</param>
        /// <param name="alpha">Alpha的值，透明度</param>
        /// <param name="flags">附带参数</param>
        /// <returns>true or false</returns>
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, ulong colorKey, byte alpha, long flags);



        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindow(IntPtr hWnd);

    }

    namespace CommonWinAPI
    {
        public static class WindowControl
        {
            static object mouseLock = new object();

            public static void ScrollWheel(IntPtr hwnd, ScrollType scrollType, int delta)
            {
                int lineDownOrUp;
                if (delta < 0)
                {
                    lineDownOrUp = 0;
                    delta = -delta;
                }
                else
                    lineDownOrUp = 1;
                while (delta-- > 0)
                    WinAPI.SendMessage(hwnd, (int)scrollType, lineDownOrUp, 0);
            }

            public static void SendKeyDown(IntPtr hwnd, int keycode)
            {
                WinAPI.SendMessage(hwnd, (int)WinAPI.WindowMessage.WM_KEYDOWN, keycode, 0);
            }

            public static void SendKeyUp(IntPtr hwnd, int keycode)
            {
                WinAPI.SendMessage(hwnd, (int)WinAPI.WindowMessage.WM_KEYUP, keycode, 0);
            }

            public static void KeyDown(byte keycode)
            {
                WinAPI.keybd_event(keycode, 0, (uint)WinAPI.KEYEVENTF.KEYDOWN, UIntPtr.Zero);
            }
            public static void KeyUp(byte keycode)
            {
                WinAPI.keybd_event(keycode, 0, (uint)WinAPI.KEYEVENTF.KEYUP, UIntPtr.Zero);
            }
            public static void KeyPress(byte keycode)
            {
                WinAPI.keybd_event(keycode, 0, (uint)WinAPI.KEYEVENTF.KEYDOWN, UIntPtr.Zero);
                WinAPI.keybd_event(keycode, 0, (uint)WinAPI.KEYEVENTF.KEYUP, UIntPtr.Zero);
            }

            public enum ScrollType
            {
                Horizontal = 0x114,
                Vertical = 0x115,
            }

            public static void MouseLeftClick(IntPtr hwnd, System.Drawing.Point pt)
            {
                lock (mouseLock)
                {
                    WinAPI.SendMessage(hwnd, (int)WinAPI.WindowMessage.WM_MOUSEMOVE, 0, (pt.X & 0xFFFF) | (pt.Y << 16));
                    WinAPI.SendMessage(hwnd, (int)WinAPI.WindowMessage.WM_LBUTTONDOWN, (int)WinAPI.MouseKey.MK_LBUTTON, (pt.X & 0xFFFF) | (pt.Y << 16));
                    WinAPI.SendMessage(hwnd, (int)WinAPI.WindowMessage.WM_LBUTTONUP, (int)WinAPI.MouseKey.MK_LBUTTON, (pt.X & 0xFFFF) | (pt.Y << 16));
                }
                // WinAPI.SetCursorPos(p.X, p.Y);
            }

            public static void MouseLeftClick(System.Drawing.Point pt)
            {
                MouseLeftClick(pt.X, pt.Y);
            }
            public static void MouseLeftClick(int x, int y)
            {
                lock (mouseLock)
                {
                    WinAPI.POINT p;
                    WinAPI.GetCursorPos(out p);
                    WinAPI.SetCursorPos(x, y);
                    WinAPI.mouse_event((uint)(WinAPI.MouseEventFlags.LEFTDOWN | WinAPI.MouseEventFlags.ABSOLUTE), (uint)x, (uint)y, 0, UIntPtr.Zero);
                    WinAPI.mouse_event((uint)(WinAPI.MouseEventFlags.LEFTUP | WinAPI.MouseEventFlags.ABSOLUTE), (uint)x, (uint)y, 0, UIntPtr.Zero);
                    
                }
                // WinAPI.SetCursorPos(p.X, p.Y);
            }
            public static void MouseLeftDown()
            {
                lock (mouseLock)
                    WinAPI.mouse_event((uint)(WinAPI.MouseEventFlags.LEFTDOWN), 0, 0, 0, UIntPtr.Zero);
            }
            public static void MouseLeftUp()
            {
                lock (mouseLock)
                    WinAPI.mouse_event((uint)(WinAPI.MouseEventFlags.LEFTUP), 0, 0, 0, UIntPtr.Zero);
            }
            public static void MouseLeftClick()
            {
                lock (mouseLock)
                {
                    WinAPI.mouse_event((uint)(WinAPI.MouseEventFlags.LEFTDOWN), 0, 0, 0, UIntPtr.Zero);
                    WinAPI.mouse_event((uint)(WinAPI.MouseEventFlags.LEFTUP), 0, 0, 0, UIntPtr.Zero);
                }
            }
            public static void MouseLeftDbClick()
            {
                lock (mouseLock)
                {
                    MouseLeftClick();
                    MouseLeftClick();
                }
            }

            public static void MouseMiddleDown()
            {
                WinAPI.mouse_event((uint)(WinAPI.MouseEventFlags.MIDDLEDOWN), 0, 0, 0, UIntPtr.Zero);
            }
            public static void MouseMiddleUp()
            {
                WinAPI.mouse_event((uint)(WinAPI.MouseEventFlags.MIDDLEUP), 0, 0, 0, UIntPtr.Zero);
            }
            public static void MouseMiddleClick()
            {
                WinAPI.mouse_event((uint)(WinAPI.MouseEventFlags.MIDDLEDOWN), 0, 0, 0, UIntPtr.Zero);
                WinAPI.mouse_event((uint)(WinAPI.MouseEventFlags.MIDDLEUP), 0, 0, 0, UIntPtr.Zero);
            }
            public static void MouseMiddleDbClick()
            {
                MouseMiddleClick();
                MouseMiddleClick();
            }

            public static void MouseRightDown()
            {
                WinAPI.mouse_event((uint)(WinAPI.MouseEventFlags.RIGHTDOWN), 0, 0, 0, UIntPtr.Zero);
            }
            public static void MouseRightUp()
            {
                WinAPI.mouse_event((uint)(WinAPI.MouseEventFlags.RIGHTUP), 0, 0, 0, UIntPtr.Zero);
            }
            public static void MouseRightClick()
            {
                WinAPI.mouse_event((uint)(WinAPI.MouseEventFlags.RIGHTDOWN), 0, 0, 0, UIntPtr.Zero);
                WinAPI.mouse_event((uint)(WinAPI.MouseEventFlags.RIGHTUP), 0, 0, 0, UIntPtr.Zero);
            }
            public static void MouseRightDbClick()
            {
                MouseRightClick();
                MouseRightClick();
            }


            public static void MouseMove(int x, int y)
            {
                WinAPI.mouse_event((uint)(WinAPI.MouseEventFlags.MOVE | WinAPI.MouseEventFlags.ABSOLUTE),
                    (uint)((x << 16) / System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width),
                    (uint)((y << 16) / System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height), 0, UIntPtr.Zero);


            }
            public static void MouseMoveOffset(int dx, int dy)
            {
                WinAPI.mouse_event((uint)(WinAPI.MouseEventFlags.MOVE), (uint)dx, (uint)dy, 0, UIntPtr.Zero);
            }
            public static void MouseMoveCenter()
            {
                WinAPI.mouse_event((uint)(WinAPI.MouseEventFlags.MOVE | WinAPI.MouseEventFlags.ABSOLUTE), 32768, 32768, 0, UIntPtr.Zero);
            }

            public static void PerformClick(IntPtr hwnd)
            {
                WinAPI.SendMessage(hwnd, 0x00F5, 0, 0);
            }




            public static WinAPI.WindowStyles GetWindowStyles(IntPtr hWnd)
            {
                return (WinAPI.WindowStyles)WinAPI.GetWindowLong(hWnd, WinAPI.WindowLongIndex.GWL_STYLE);
            }

            public static WinAPI.ExtendedWindowStyles GetExtendedWindowStyle(IntPtr hWnd)
            {
                return (WinAPI.ExtendedWindowStyles)WinAPI.GetWindowLong(hWnd, WinAPI.WindowLongIndex.GWL_EXSTYLE);
            }

            /// <summary>  
            /// 设置窗体为无边框风格  
            /// </summary>  
            /// <param name="hWnd"></param>  
            public static void SetWindowNoBorder(IntPtr hWnd)
            {
                var oldstyle = GetWindowStyles(hWnd);
                SetWindowStyles(hWnd, oldstyle & (~WinAPI.WindowStyles.CAPTION));

                WinAPI.RECT rect;
                WinAPI.GetWindowRect(hWnd, out rect);

                int x = rect.X < 0 ? 0 : rect.X;
                int y = rect.Y < 0 ? 0 : rect.Y;
                int w = rect.Width > System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width ? System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width : rect.Width;
                int h = rect.Height > System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height ? System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height : rect.Height;
                WinAPI.MoveWindow(hWnd, x, y, w, h, false);
            }


            public static void SetWindowStyles(IntPtr hWnd, WinAPI.WindowStyles style)
            {
                WinAPI.SetWindowLong(hWnd, WinAPI.WindowLongIndex.GWL_STYLE, (long)style);
            }

            public static void SetExtendedWindowStyle(IntPtr hWnd, WinAPI.ExtendedWindowStyles style)
            {
                WinAPI.SetWindowLong(hWnd, WinAPI.WindowLongIndex.GWL_EXSTYLE, (long)style);
            }


            public static IntPtr GetWindowUnderCursor()
            {
                WinAPI.POINT pt;
                WinAPI.GetCursorPos(out pt);
                return WinAPI.WindowFromPoint(pt);
            }
        }

    }
}
