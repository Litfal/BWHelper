using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Litfal
{
    static class CaptureHelper
    {
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, out Rectangle bounds);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hwnd, out Rectangle bounds);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hwnd, out Point lpPoint);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hwnd);


        [DllImport("gdi32.dll")]
        public static extern UInt64 BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, System.Int32 dwRop);


        public static void CaptureWindowByBitBlt(IntPtr handle, out Rectangle clientRect, ref Bitmap targetBitmap)
        {
            // 取得該視窗的大小與位置 
            Rectangle bounds;
            Point pt;
            Rectangle windowBounds;

            GetWindowRect(handle, out windowBounds);
            GetClientRect(handle, out bounds);
            ClientToScreen(handle, out pt);

            // client offset of window
            pt = new Point(pt.X - windowBounds.X, pt.Y - windowBounds.Y);
            clientRect = BoundsToRectangle(pt, bounds);

            if (clientRect.Width <= 0 || clientRect.Height <= 0)
                throw new Exception();

            // 抓取截圖 
            if (targetBitmap == null || targetBitmap.Width != clientRect.Width || targetBitmap.Height != clientRect.Height)
                targetBitmap = new Bitmap(clientRect.Width, clientRect.Height, PixelFormat.Format32bppArgb);


            using (Graphics gfx = Graphics.FromImage(targetBitmap))
            {
                IntPtr dc1 = gfx.GetHdc();
                IntPtr dc2 = GetWindowDC(handle);

                BitBlt(dc1, 0, 0,
                    clientRect.Width,
                    clientRect.Height, dc2, clientRect.X, clientRect.Y, 13369376);

                gfx.ReleaseHdc(dc1);
            }
        }

        public static bool CaptureWindowByBitBlt(IntPtr handle, IntPtr hDC, out Rectangle clientRect, ref Bitmap targetBitmap)
        {
            // 取得該視窗的大小與位置 
            Rectangle bounds;
            Point pt;
            Rectangle windowBounds;

            GetWindowRect(handle, out windowBounds);
            GetClientRect(handle, out bounds);
            ClientToScreen(handle, out pt);

            // client offset of window
            pt = new Point(pt.X - windowBounds.X, pt.Y - windowBounds.Y);
            clientRect = BoundsToRectangle(pt, bounds);

            if (clientRect.Width <= 0 || clientRect.Height <= 0)
                return false;

            // 抓取截圖 
            //if (targetBitmap == null || targetBitmap.Width != clientRect.Width || targetBitmap.Height != clientRect.Height)
            //    targetBitmap = new Bitmap(clientRect.Width, clientRect.Height, PixelFormat.Format32bppArgb);
            if (targetBitmap == null || targetBitmap.Width != clientRect.Width || targetBitmap.Height != clientRect.Height)
                targetBitmap = new Bitmap(clientRect.Width, clientRect.Height, PixelFormat.Format32bppArgb);


            using (Graphics gfx = Graphics.FromImage(targetBitmap))
            {
                IntPtr dc1 = gfx.GetHdc();
                // IntPtr dc2 = GetWindowDC(handle);

                BitBlt(dc1, 0, 0,
                    clientRect.Width,
                    clientRect.Height, hDC, clientRect.X, clientRect.Y, 13369376);

                gfx.ReleaseHdc(dc1);
            }
            return true;
        }


        private static Rectangle BoundsToRectangle(Point pt, Rectangle bounds)
        {
            return new Rectangle(
                pt.X,
                pt.Y,
                bounds.Width - bounds.Left,
                bounds.Height - bounds.Top);
        }

    }
}
