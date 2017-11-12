using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWHelper
{
    class EBPage
    {
        const int SUMINIT = 0x3A45F8E2;
        
        Size _lastBmpSize = new Size(-1, -1);
        long _lastBmpSum = 0;

        int _bottomSliderY;

        public ProcessSettings Settings { get; set; }
        public bool IsSamePage { get; private set; } = false;
        // public bool? IsColorPage { get; private set; } = null;  // null is unknow

        private bool? _isColorPage = null;


        int _picArea_noWhiteTh;
        const int _picArea_WidthTh = 50;
        int _picAreaX1 = -1;
        int _picAreaX2 = -1;

        int negativeColorExcludeX1 = 0;
        int negativeColorExcludeX2 = 0;

        int lightMixingExcludeX1 = 0;
        int lightMixingExcludeX2 = 0;

        public EBPage(ProcessSettings settings)
        {
            Settings = settings;
        }

        public void Process(Bitmap bmp)
        {
            if (bmp == null) throw new ArgumentNullException();
            var watch = Stopwatch.StartNew();
            // if (!Config.AnyProcess) return;
            var bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            try
            {
                _bottomSliderY = findLeftButtomLine(bd);
                long sumValue = computeSum(bd);
                IsSamePage = sumValue == _lastBmpSum;
                _lastBmpSum = sumValue;
                if (IsSamePage) return;

                _isColorPage = null;
                _picAreaX1 = _picAreaX2 = -1;
                negativeColorExcludeX1 = lightMixingExcludeX1 = 0;
                negativeColorExcludeX2 = lightMixingExcludeX2 = -1;

                bool doNegativeColorProc = Settings.NegativeColor;
                if (doNegativeColorProc && Settings.NegativeColorOffWithColorPage)
                {
                    // check is color page
                    _isColorPage = findNotGaryColor(bd, _bottomSliderY);
                    //_isColorPage = findNotGaryColorMT(bd, _bottomSliderY);
                    doNegativeColorProc = _isColorPage != true;
                }
                if (doNegativeColorProc && Settings.NegativeColorOffWithPictureArea)
                {
                    findPicArea(bd, _bottomSliderY);
                    negativeColorExcludeX1 = _picAreaX1;
                    negativeColorExcludeX2 = _picAreaX2;
                }
                
                bool doLightMixing = Settings.ReduceLight &&
                    (Settings.ReduceLightBluePercent != 1 || Settings.ReduceLightRedPercent != 1 && Settings.ReduceLightGreenPercent != 1);
                if (doLightMixing && Settings.ReduceLightOffWithColorPage)
                {
                    if (_isColorPage == null)
                    {
                        _isColorPage = findNotGaryColor(bd, _bottomSliderY);
                        //_isColorPage = findNotGaryColorMT(bd, _bottomSliderY);
                    }
                    doLightMixing = _isColorPage != true;
                }

                if (doLightMixing && Settings.ReduceLightOffWithPictureArea)
                {
                    if (_picAreaX1 == -1)
                        findPicArea(bd, _bottomSliderY);
                    lightMixingExcludeX1 = _picAreaX1;
                    lightMixingExcludeX2 = _picAreaX2;
                }

                if (doNegativeColorProc)
                {
                    //negativeColorProcess(bd, _bottomSliderY);
                    //negativeColorProcessMT(bd, _bottomSliderY);
                    negativeColorProcess64(bd, _bottomSliderY);
                    //negativeColorProcess64MT(bd, _bottomSliderY);
                }

                if (doLightMixing)
                {
                    //reduceBlueLight(bd, _bottomSliderY, 0.78);
                    //lightMixProcess(bd, _bottomSliderY, Config.LightMixingColor);
                    reduceLightProcess(bd, _bottomSliderY, Settings.ReduceLightRedPercent, Settings.ReduceLightGreenPercent, Settings.ReduceLightBluePercent);
                    //reduceLightProcessMT(bd, _bottomSliderY, Settings.ReduceLightRedPercent, Settings.ReduceLightGreenPercent, Settings.ReduceLightBluePercent);
                }
                //negativeColorProcess(bd);
                Debug.WriteLine("Process total: {0} ms", watch.ElapsedMilliseconds);


            }
            finally
            {
                bmp.UnlockBits(bd);
            }


        }

        private unsafe void reduceBlueLight(BitmapData bd, int height, double bPercent)
        {
            var watch = Stopwatch.StartNew();
            
            uint bP = (uint)(bPercent * (1 << 24));
            byte* pY = (byte*)bd.Scan0;
            for (int y = 0; y < height; y++)
            {
                byte* pX = pY;
                for (int x = 0; x < bd.Width; x++)
                {
                    if (x < lightMixingExcludeX1 || x > lightMixingExcludeX2)
                        *pX = (byte)((*pX * bP) >> 24);
                    pX += 4;
                }
                pY += bd.Stride;
            }

            Debug.WriteLine("reduceBlueLight: {0} ms", watch.ElapsedMilliseconds);
        }

        private unsafe void reduceLightProcess(BitmapData bd, int height, int rPercent, int gPercent, int bPercent)
        {
            var watch = Stopwatch.StartNew();

            uint rP = (uint)(rPercent * (1 << 24) / 100);
            uint gP = (uint)(gPercent * (1 << 24) / 100);
            uint bP = (uint)(bPercent * (1 << 24) / 100);

            
            byte* pY = (byte*)bd.Scan0;
            for (int y = 0; y < height; y++)
            {
                byte* pX = pY;
                for (int x = 0; x < lightMixingExcludeX1; x++)
                {
                    *pX = (byte)((*pX * bP) >> 24); pX++;
                    *pX = (byte)((*pX * gP) >> 24); pX++;
                    *pX = (byte)((*pX * rP) >> 24); pX += 2;
                }
                pY += bd.Stride;
            }

            int x2 = lightMixingExcludeX2 + 1;
            pY = (byte*)bd.Scan0 + x2 * 4;
            for (int y = 0; y < height; y++)
            {
                byte* pX = pY;
                for (int x = x2; x < bd.Width; x++)
                {
                    *pX = (byte)((*pX * bP) >> 24); pX++;
                    *pX = (byte)((*pX * gP) >> 24); pX++;
                    *pX = (byte)((*pX * rP) >> 24); pX += 2;
                }
                pY += bd.Stride;
            }

            Debug.WriteLine("lightReduceProcess: {0} ms", watch.ElapsedMilliseconds);
        }

        private unsafe void reduceLightProcessMT(BitmapData bd, int height, int rPercent, int gPercent, int bPercent)
        {
            var watch = Stopwatch.StartNew();

            uint rP = (uint)(rPercent * (1 << 24) / 100);
            uint gP = (uint)(gPercent * (1 << 24) / 100);
            uint bP = (uint)(bPercent * (1 << 24) / 100);

            //int xStart = lightMixingExcludeX1;
            Range[] yParts = cutRanges(height);

            Parallel.ForEach(yParts, (yPart) =>
            {
                byte* pY0 = (byte*)bd.Scan0 + yPart.Start * bd.Stride;
                byte* pY = pY0;
                for (int y = yPart.Start; y < yPart.End; y++)
                {
                    byte* pX = pY;
                    for (int x = 0; x < lightMixingExcludeX1; x++)
                    {
                        *pX = (byte)((*pX * bP) >> 24); pX++;
                        *pX = (byte)((*pX * gP) >> 24); pX++;
                        *pX = (byte)((*pX * rP) >> 24); pX += 2;
                    }
                    pY += bd.Stride;
                }

                int x2 = lightMixingExcludeX2 + 1;
                pY = pY0 + x2 * 4;
                for (int y = yPart.Start; y < yPart.End; y++)
                {
                    byte* pX = pY;
                    for (int x = x2; x < bd.Width; x++)
                    {
                        *pX = (byte)((*pX * bP) >> 24); pX++;
                        *pX = (byte)((*pX * gP) >> 24); pX++;
                        *pX = (byte)((*pX * rP) >> 24); pX += 2;
                    }
                    pY += bd.Stride;
                }
            });

            Debug.WriteLine("lightReduceProcessMT: {0} ms", watch.ElapsedMilliseconds);
        }

        private Range[] cutRanges(int height)
        {
            int rangeCount = Environment.ProcessorCount * 2;
            int yPartH = height / rangeCount;

            return Enumerable
                .Range(0, rangeCount)
                .Select(yPartIndex => new Range
                {
                    Start = yPartH * yPartIndex,
                    End = yPartIndex == rangeCount - 1 ? height : yPartH * (yPartIndex + 1)
                })
                .ToArray();
        }

        private unsafe void lightMixProcess(BitmapData bd, int height, int lightColor)
        {
            var watch = Stopwatch.StartNew();
            
            byte b = (byte)lightColor;
            byte g = (byte)(lightColor >> 8);
            byte r = (byte)(lightColor >> 16);

            byte* pY = (byte*)bd.Scan0;
            for (int y = 0; y < height; y++)
            {
                byte* pX = pY;
                for (int x = 0; x < bd.Width; x++)
                {
                    if (x < lightMixingExcludeX1 || x > lightMixingExcludeX2)
                    {
                        if (*pX > b) *pX = b; pX++;
                        if (*pX > g) *pX = g; pX++;
                        if (*pX > r) *pX = r; pX += 2;
                    }
                    else
                        pX += 4;
                }
                pY += bd.Stride;
            }

            Debug.WriteLine("lightMixProcess: {0} ms", watch.ElapsedMilliseconds);
        }

        private unsafe void negativeColorProcess(BitmapData bd, int height)
        {
            var watch = Stopwatch.StartNew();
            
            byte* pY = (byte*)bd.Scan0;
            for (int y = 0; y < height; y++)
            {
                int* pX = (int*)pY;
                for (int x = 0; x < negativeColorExcludeX1; x++)
                {
                    *pX ^= 0xffffff;
                    pX++;
                }
                pY += bd.Stride;
            }

            int x2 = negativeColorExcludeX2 + 1;
            pY = (byte*)bd.Scan0 + x2 * 4;
            for (int y = 0; y < height; y++)
            {
                int* pX = (int*)pY;
                for (int x = x2; x < bd.Width; x++)
                {
                    *pX ^= 0xffffff;
                    pX++;
                }
                pY += bd.Stride;
            }

            Debug.WriteLine("negativeColorProcess: {0} ticks", watch.ElapsedTicks);
        }

        private unsafe void negativeColorProcessMT(BitmapData bd, int height)
        {
            var watch = Stopwatch.StartNew();


            //int xStart = lightMixingExcludeX1;
            Range[] ranges = cutRanges(height);

            Parallel.ForEach(ranges, (yRange) =>
            {
                byte* pY0 = (byte*)bd.Scan0 + yRange.Start * bd.Stride;
                byte* pY = pY0;
                for (int y = yRange.Start; y < yRange.End; y++)
                {
                    int* pX = (int*)pY;
                    for (int x = 0; x < negativeColorExcludeX1; x++)
                    {
                        *pX ^= 0xffffff;
                        pX++;
                    }
                    pY += bd.Stride;
                }

                int x2 = negativeColorExcludeX2 + 1;
                pY = pY0 + x2 * 4;
                for (int y = yRange.Start; y < yRange.End; y++)
                {
                    int* pX = (int*)pY;
                    for (int x = x2; x < bd.Width; x++)
                    {
                        *pX ^= 0xffffff;
                        pX++;
                    }
                    pY += bd.Stride;
                }

            });

            Debug.WriteLine("negativeColorProcessMT: {0} ticks", watch.ElapsedTicks);
        }

        private unsafe void negativeColorProcess64(BitmapData bd, int height)
        {
            var watch = Stopwatch.StartNew();

            byte* pY = (byte*)bd.Scan0;
            int x1count64 = negativeColorExcludeX1 / 2;
            bool x1ov = (negativeColorExcludeX1 & 1) == 1;

            for (int y = 0; y < height; y++)
            {
                long* pX = (long*)pY;
                for (int x = 0; x < x1count64; x++)
                {
                    *pX ^= 0x00FFFFFF00FFFFFF;
                    pX++;
                }
                if (x1ov)
                    *(int*)pX ^= 0x00FFFFFF;
                pY += bd.Stride;
            }

            int x2 = negativeColorExcludeX2 + 1;
            int x2count32 = (bd.Width - x2);
            int x2count64 = x2count32 / 2;
            bool x2ov = (x2count32 & 1) == 1;

            pY = (byte*)bd.Scan0 + x2 * 4;
            for (int y = 0; y < height; y++)
            {
                long* pX = (long*)pY;
                for (int x = 0; x < x2count64; x++)
                {
                    *pX ^= 0x00FFFFFF00FFFFFF;
                    pX++;
                }
                if (x2ov)
                    *(int*)pX ^= 0x00FFFFFF;
                pY += bd.Stride;
            }

            Debug.WriteLine("negativeColorProcess64: {0} ticks", watch.ElapsedTicks);
        }

        private unsafe void negativeColorProcess64MT(BitmapData bd, int height)
        {
            var watch = Stopwatch.StartNew();
            
            Range[] ranges = cutRanges(height);

            Parallel.ForEach(ranges, (yRange) =>
            {
                byte* pY0 = (byte*)bd.Scan0 + yRange.Start * bd.Stride;
                byte* pY = pY0;
                int x1count64 = negativeColorExcludeX1 / 2;
                bool x1ov = (negativeColorExcludeX1 & 1) == 1;

                for (int y = yRange.Start; y < yRange.End; y++)
                {
                    long* pX = (long*)pY;
                    for (int x = 0; x < x1count64; x++)
                    {
                        *pX ^= 0x00FFFFFF00FFFFFF;
                        pX++;
                    }
                    if (x1ov)
                        *(int*)pX ^= 0x00FFFFFF;
                    pY += bd.Stride;
                }

                int x2 = negativeColorExcludeX2 + 1;
                int x2count32 = (bd.Width - x2);
                int x2count64 = x2count32 / 2;
                bool x2ov = (x2count32 & 1) == 1;

                pY = pY0 + x2 * 4;
                for (int y = yRange.Start; y < yRange.End; y++)
                {
                    long* pX = (long*)pY;
                    for (int x = 0; x < x2count64; x++)
                    {
                        *pX ^= 0x00FFFFFF00FFFFFF;
                        pX++;
                    }
                    if (x2ov)
                        *(int*)pX ^= 0x00FFFFFF;
                    pY += bd.Stride;
                }

            });

            Debug.WriteLine("negativeColorProcess64MT: {0} ticks", watch.ElapsedTicks);
        }

        private unsafe void findPicArea(BitmapData bd, int height)
        {
            var watch = Stopwatch.StartNew();

            _picArea_noWhiteTh = height / 15;
            const int write = unchecked((int)0xFFFFFFFF);
            var continuteNotWriteCount = new int[bd.Width];

            int* p = (int*)bd.Scan0;
            byte* pY = (byte*)bd.Scan0;
            for (int y = 0; y < height; y++)
            {
                int* pX = (int*)pY;
                for (int x = 0; x < bd.Width; x++)
                {
                    if (*pX == write)
                    {
                        if (continuteNotWriteCount[x] < _picArea_noWhiteTh) continuteNotWriteCount[x] = 0;
                    }
                    else
                        continuteNotWriteCount[x]++;
                    pX++;
                }
                pY += bd.Stride;
            }
            
            int xStart = -1;
            int xCount = 0;
            int xMax = -1;
            int xMaxStartIndex = 0;
            int xMaxEndIndex = 0;
            for (int x = 0; x < continuteNotWriteCount.Length; x++)
            {
                if (continuteNotWriteCount[x] >= _picArea_noWhiteTh)
                {
                    if (xStart == -1)
                    {
                        xStart = x;
                        //xCount++;
                    }
                    // if(xS)
                    xCount++;
                }
                else
                {
                    if (xCount > 0)
                    {
                        xCount--;
                    }
                    else
                        xStart = -1;
                }
                //continuteNotWriteCount[x] = xCount;
                if (xCount > xMax)
                {
                    xMaxStartIndex = xStart;
                    xMaxEndIndex = x;
                    xMax = xCount;
                }
            }

            if (xMax > _picArea_WidthTh)
            {
                _picAreaX1 = xMaxStartIndex;
                _picAreaX2 = xMaxEndIndex;
            }
            else
            {
                _picAreaX1 = 0;
                _picAreaX2 = -1;
            }

            Debug.WriteLine("findPicArea: {0} ms", watch.ElapsedMilliseconds);

        }


        private unsafe bool findNotGaryColor(BitmapData bd, int height)
        {
            var watch = Stopwatch.StartNew();

            const int white = unchecked((int)0xFFFFFFFF);
            long deltaSum = 0;
            //byte[] colorArr = new byte[3];
            byte M;
            byte m;
            byte* p = (byte*)bd.Scan0;
            byte* pY = (byte*)bd.Scan0;
            int deltaMax = 0;
            int whiteCount = 0;
            for (int y = 0; y < height; y++)
            {
                byte* pX = pY;
                for (int x = 0; x < bd.Width; x++)
                {
                    if (*(int*)pX == white) whiteCount++;
                    byte b = *(pX + 1);
                    byte r = *(pX + 2);
                    if (*pX > b)
                    {
                        M = *pX;
                        m = b;
                    }
                    else
                    {
                        M = b;
                        m = *pX;
                    }
                    if (r > M)
                        M = r;
                    if (r < m)
                        m = r;

                    int delta = M - m;
                    deltaSum += delta;
                    if (delta > deltaMax)
                        deltaMax = delta;

                    pX += 4;
                }
                pY += bd.Stride;
            }
            double deltaRate = (double)deltaSum / (height * bd.Width);
            double deltaRate2 = (double)deltaSum / (height * bd.Width - whiteCount);
            Debug.WriteLine("findNotGaryColor: deltaSum={0}, deltaMax={1}, deltaRate={2}, deltaRate2={3}, {4} ms", deltaSum, deltaMax, deltaRate, deltaRate2, watch.ElapsedMilliseconds);
            const long FindColorDeltaSumTh = 1000000;
            const double deltaRate2Th = 10;
            return deltaRate2 > deltaRate2Th;
        }

        private unsafe bool findNotGaryColorMT(BitmapData bd, int height)
        {
            var watch = Stopwatch.StartNew();
            const int white = unchecked((int)0xFFFFFFFF);
            // long deltaSum = 0;

            Range[] yRanges = cutRanges(height);
            long[] deltaSumArr = new long[yRanges.Length];
            int[] whiteCountArr = new int[yRanges.Length];
            Parallel.For(0, yRanges.Length,
                index =>
                {
                    var yRange = yRanges[index];
                    long deltaSumPart = 0;
                    int whiteCountPart = 0;
                    byte M;
                    byte m;
                    byte* pY = (byte*)bd.Scan0 + yRange.Start * bd.Stride;
                    for (int y = yRange.Start; y < yRange.End; y++)
                    {
                        byte* pX = pY;
                        for (int x = 0; x < bd.Width; x++)
                        {
                            if (*(int*)pX == white) whiteCountPart++;
                            byte b = *(pX + 1);
                            byte r = *(pX + 2);
                            if (*pX > b)
                            {
                                M = *pX;
                                m = b;
                            }
                            else
                            {
                                M = b;
                                m = *pX;
                            }
                            if (r > M)
                                M = r;
                            if (r < m)
                                m = r;

                            int delta = M - m;
                            deltaSumPart += delta;

                            pX += 4;
                        }
                        pY += bd.Stride;
                    }
                    deltaSumArr[index] = deltaSumPart;
                    whiteCountArr[index] = whiteCountPart;
                });

            long deltaSum = deltaSumArr.Sum();
            long whiteCount = whiteCountArr.Sum();
            double deltaRate = (double)deltaSum / (height * bd.Width);
            double deltaRate2 = (double)deltaSum / (height * bd.Width - whiteCount);
            Debug.WriteLine("findNotGaryColorMT: deltaSum={0}, deltaRate={1}, deltaRate2={2}, {3} ms", deltaSum, deltaRate, deltaRate2, watch.ElapsedMilliseconds);
            const long FindColorDeltaSumTh = 1000000;
            const double deltaRate2Th = 10;
            return deltaRate2 > deltaRate2Th;
        }

        public void Refresh()
        {
            _lastBmpSum = 0;
        }

        private unsafe long computeSum(Bitmap bmp)
        {
            var bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            long sum = computeSum(bd);
            bmp.UnlockBits(bd);
            return sum;
        }

        private unsafe long computeSum(BitmapData bd)
        {
            //var watch = Stopwatch.StartNew();

            #region 32-bit (old)
            //int w = bd.Width;
            //int h = bd.Height;
            //int y = (_bottomSliderY < bd.Height - 1) ? _bottomSliderY : 0;

            //int sum = SUMINIT;
            //int* p = (int*)bd.Scan0;
            //byte* pY = ((byte*)bd.Scan0) + y * bd.Stride;
            //for (; y < h; y++)
            //{
            //    int* pX = (int*)pY;
            //    for (int x = 0; x < w; x++)
            //    {
            //        sum += *pX++;
            //    }
            //    pY += bd.Stride;
            //}
            #endregion

            #region 64-bit mode (old)

            //int w = bd.Width / 2;
            //int h = bd.Height;
            //int y = (_bottomSliderY < bd.Height - 1) ? _bottomSliderY : 0;

            //long sum = SUMINIT;
            //int* p = (int*)bd.Scan0;
            //byte* pY = ((byte*)bd.Scan0) + y * bd.Stride;
            //for (; y < h; y++)
            //{
            //    long* pX = (long*)pY;
            //    for (int x = 0; x < w; x++)
            //    {
            //        sum += *pX++;
            //    }
            //    pY += bd.Stride;
            //}

            #endregion

            // 64-bit or one two mode
            if (_bottomSliderY >= bd.Height - 1) return computeSum64(bd);
            int w = bd.Width / 2;
            int h = bd.Height;
            int y1 = (_bottomSliderY + h) / 2;
            int y2 = (0 + _bottomSliderY) / 2;

            long sum = SUMINIT;
            long* p = (long*)((byte*)bd.Scan0 + y1 * bd.Stride);
            for (int x = 0; x < w; x++)
                sum += *p++;
            p = (long*)((byte*)bd.Scan0 + y2 * bd.Stride);
            for (int x = 0; x < w; x++)
                sum += *p++;

            //Debug.WriteLine("compute sum: {0}, {1} ticks", sum, watch.ElapsedTicks);
            return sum;
        }

        private unsafe long computeSum64(BitmapData bd)
        {
            int w = bd.Width / 2;
            int h = bd.Height;
            int y = (_bottomSliderY < bd.Height - 1) ? _bottomSliderY : 0;

            long sum = SUMINIT;
            int* p = (int*)bd.Scan0;
            byte* pY = ((byte*)bd.Scan0) + y * bd.Stride;
            for (; y < h; y++)
            {
                long* pX = (long*)pY;
                for (int x = 0; x < w; x++)
                {
                    sum += *pX++;
                }
                pY += bd.Stride;
            }
            return sum;
        }

        private unsafe int findLeftButtomLine(BitmapData bd)
        {
            int grayTh = 230 * 3;
            byte* p = (byte*)bd.Scan0 + (bd.Height - 1) * bd.Stride;
            for (int y = bd.Height - 1; y >= 0; y--)
            {
                int gary = *p + *(p + 1) + *(p + 2);
                if (gary < grayTh)
                {
                    return y;
                }
                p -= bd.Stride;
            }
            return bd.Height - 1;
        }
    }

    internal class Range
    {
        public int Start { get; set; }
        public int End { get; set; }
    }
}
