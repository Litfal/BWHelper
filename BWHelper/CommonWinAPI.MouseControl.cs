using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litfal.CommonWinAPI
{
    public static class MouseControl
    {
        internal static void MouseMove(IntPtr hwnd, int x, int y)
        {
            MouseEvent(hwnd, WinAPI.WindowMessage.WM_MOUSEMOVE, x, y);
        }

        internal static void MouseLeftDown(IntPtr hwnd, int x, int y)
        {
            MouseEvent(hwnd, WinAPI.WindowMessage.WM_LBUTTONDOWN, x, y);
        }

        internal static void MouseLeftUp(IntPtr hwnd, int x, int y)
        {
            MouseEvent(hwnd, WinAPI.WindowMessage.WM_LBUTTONUP, x, y);
        }

        internal static void MouseWheel(IntPtr hwnd, int delta, int x, int y)
        {
            MouseEvent(hwnd, WinAPI.WindowMessage.WM_MOUSEWHEEL, x, y, delta << 16);
        }


        internal static void MouseEvent(IntPtr hwnd, WinAPI.WindowMessage wmsg, int x, int y, int wParam = 0)
        {
            WinAPI.SendMessage(hwnd, (int)wmsg, wParam, (x & 0xFFFF) | (y << 16));
        }
    }
}
