using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BWHelper
{
    abstract class GDIScreenCapturerBase<T> :IScreenCapturer<T>
    {
        IntPtr captureHandle = IntPtr.Zero;
        IntPtr targetHDc = IntPtr.Zero;
        BlockQueue<IScreenCaptureData<T>> backBuffer;
        BlockQueue<IScreenCaptureData<T>> drawBuffer;
        Thread workThread;
        ManualResetEvent pauseResetEvent = new ManualResetEvent(false);
        bool disposing = false;

        public event EventHandler<CapturedEventArgs<T>> Captured;


        public string Name
        {
            get
            {
                return "Software mode";
            }
        }

        public bool HighAccurateFPSLimit { get; set; }

        private long minIntervalTicks = 0;

        public double FPSLimit
        {
            get { if (minIntervalTicks == 0) return 0; return minIntervalTicks / System.Diagnostics.Stopwatch.Frequency; }
            set
            {
                if (value <= 0)
                    minIntervalTicks = 0;
                else minIntervalTicks = (long)(System.Diagnostics.Stopwatch.Frequency / value);
            }
        }

        public double FPS
        {
            get;
            private set;
        }

        double _maxCaptureMs;
        public double MaxCaptureMs
        {
            get { return _maxCaptureMs; }
        }


        public GDIScreenCapturerBase()
        {
            initBuffer();
            workThread = new Thread(new ThreadStart(capture_proc_loop));
            workThread.Priority = ThreadPriority.Highest;
            workThread.Name = "GDIScreenCapturer Thread";
            workThread.Start();
        }

        #region Queue Buffer

        private void initBuffer()
        {
            backBuffer = new BlockQueue<IScreenCaptureData<T>>(BackBufferSize);
            drawBuffer = new BlockQueue<IScreenCaptureData<T>>(DrawBufferSize, true);
            drawBuffer.Overwrited += DrawBuffer_Overwrited;
            for (int i = 0; i < backBuffer.MaxSize; i++)
            {
                backBuffer.Enqueue(createCaptureData());
            }
        }

        public virtual int DrawBufferSize { get { return 1; } }
        public virtual int BackBufferSize { get { return 3; } }


        abstract protected IScreenCaptureData<T> createCaptureData();

        private void DrawBuffer_Overwrited(object sender, CircleOverwriteEventArgs<IScreenCaptureData<T>> e)
        {
            backBuffer.Enqueue(e.Item);
        }
        
        public void EequeueBuffer(IScreenCaptureData<T> data)
        {
            if (data != null)
                backBuffer.Enqueue(data);
        }

        public IScreenCaptureData<T> DequeueBuffer()
        {
            return drawBuffer.Dequeue();
        }

        public int DrawBufferCount
        {
            get { return drawBuffer.Count; }
        }

        public int BackBufferCount
        {
            get { return backBuffer.Count; }
        }

        #endregion

        public void SetWindowHandle(IntPtr hwnd)
        {
            captureHandle = hwnd;
            targetHDc = Litfal.WinAPI.GetWindowDC(captureHandle);
        }

        private void capture_proc_loop()
        {

            var watch = System.Diagnostics.Stopwatch.StartNew();
            int frameCount = 0;
            var watchPerCap = new System.Diagnostics.Stopwatch();

            while (!disposing)
            {


                pauseResetEvent.WaitOne();

                watch.Start();

                watchPerCap.Restart();

                IScreenCaptureData<T> buffer;
                if (backBuffer.Count > 0)
                {
                    buffer = backBuffer.Dequeue();
                }
                else
                {
                    buffer = createCaptureData();
                }

                Bitmap bmp = GetBitmapFromBuffer(buffer);
                Rectangle rect;
                if(! Litfal.CaptureHelper.CaptureWindowByBitBlt(captureHandle, targetHDc, out rect, ref bmp))
                {
                    backBuffer.Enqueue(buffer);
                    continue;
                }
                buffer.Size = rect.Size;
                FillBitmapToBuffer(bmp, buffer);

                OnCaptured(buffer);

                drawBuffer.Enqueue(buffer);
                frameCount++;


                if (watch.ElapsedMilliseconds > 5000)
                {
                    FPS = frameCount * System.Diagnostics.Stopwatch.Frequency / (double)watch.ElapsedTicks;
                    FPS = frameCount / (double)watch.ElapsedMilliseconds * 1000;
                    frameCount = 0;
                    _maxCaptureMs = 0;
                    watch.Reset();
                }

                if (HighAccurateFPSLimit)
                {
                    while (watchPerCap.ElapsedTicks < minIntervalTicks)
                    {
                        Thread.SpinWait(1000);
                    }
                }
                else
                {
                    long sleepTicks = minIntervalTicks - watchPerCap.ElapsedTicks;
                    int sleepMs = (int)(sleepTicks * 1000 / System.Diagnostics.Stopwatch.Frequency);
                    if (sleepMs > 1) Thread.Sleep(sleepMs);
                }

                _maxCaptureMs = Math.Max(_maxCaptureMs, watchPerCap.ElapsedTicks * 1000d / System.Diagnostics.Stopwatch.Frequency);
            }
        }


        abstract protected Bitmap GetBitmapFromBuffer(IScreenCaptureData<T> buffer);

        abstract protected void FillBitmapToBuffer(Bitmap bmp, IScreenCaptureData<T> buffer);


        private void OnCaptured(IScreenCaptureData<T> buffer)
        {
            if (Captured != null)
                Captured(this, new CapturedEventArgs<T>(buffer));
        }

        public void Start()
        {
            pauseResetEvent.Set();
        }

        public void Stop()
        {
            pauseResetEvent.Reset();
        }

        public void Dispose()
        {
            if (disposing) return;
            disposing = true;
            try
            {
                workThread?.Join(1000);
                workThread?.Abort();
            }
            finally
            {
                workThread = null;
            }


        }






    }
}
