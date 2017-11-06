using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BWHelper.Plugins
{
    class PageControllerByMouseWheel : IPageController
    {
        public IntPtr WindowHandle { get; set; } = IntPtr.Zero;

        public PageControllerByMouseWheel()
        {

        }

        public PageControllerByMouseWheel(IntPtr targetHwnd)
        {
            WindowHandle = targetHwnd;
        }

        public void PageDown()
        {
            if (WindowHandle != IntPtr.Zero)
                Litfal.CommonWinAPI.MouseControl.MouseWheel(WindowHandle, -120, 0, 0);
        }

        public void PageUp()
        {
            if (WindowHandle != IntPtr.Zero)
                Litfal.CommonWinAPI.MouseControl.MouseWheel(WindowHandle, 120, 0, 0);
        }
    }
}