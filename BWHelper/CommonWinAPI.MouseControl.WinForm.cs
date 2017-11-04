using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litfal.CommonWinAPI.WinForm
{
    public static class MouseControl
    {
        internal static void MouseMove(IntPtr hwnd, int x, int y)
        {
            CommonWinAPI.MouseControl.MouseMove(hwnd, x, y);
        }

        internal static void MouseDown(IntPtr hwnd, System.Windows.Forms.MouseButtons button, int x, int y)
        {
            WinAPI.WindowMessage wmsg;
            switch (button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    wmsg = WinAPI.WindowMessage.WM_LBUTTONDOWN;
                    break;
                case System.Windows.Forms.MouseButtons.Middle:
                    wmsg = WinAPI.WindowMessage.WM_MBUTTONDOWN;
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    wmsg = WinAPI.WindowMessage.WM_RBUTTONDOWN;
                    break;
                default:
                    return;
            }
            CommonWinAPI.MouseControl.MouseEvent(hwnd, wmsg, x, y);
        }

        internal static void MouseUp(IntPtr hwnd, System.Windows.Forms.MouseButtons button, int x, int y)
        {
            WinAPI.WindowMessage wmsg;
            switch (button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    wmsg = WinAPI.WindowMessage.WM_LBUTTONDOWN;
                    break;
                case System.Windows.Forms.MouseButtons.Middle:
                    wmsg = WinAPI.WindowMessage.WM_MBUTTONDOWN;
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    wmsg = WinAPI.WindowMessage.WM_RBUTTONDOWN;
                    break;
                default:
                    return;
            }
            CommonWinAPI.MouseControl.MouseEvent(hwnd, wmsg + 1, x, y);
        }

        internal static void MouseWheel(IntPtr hwnd, int delta, int x, int y)
        {
            CommonWinAPI.MouseControl.MouseEvent(hwnd, WinAPI.WindowMessage.WM_MOUSEWHEEL, x, y, delta << 16);
        }
    }
}

