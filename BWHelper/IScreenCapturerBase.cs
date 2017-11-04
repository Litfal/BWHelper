using System;

namespace BWHelper
{
    interface IScreenCapturerBase:IDisposable
    {
        string Name { get; }
        int BackBufferCount { get; }
        int DrawBufferCount { get; }
        double FPS { get; }
        double MaxCaptureMs { get; }

        void SetWindowHandle(IntPtr hwnd);
        void Start();
        void Stop();
    }
}