using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWHelper
{
    interface IScreenCapturer<T> : IScreenCapturerBase
    {
        IScreenCaptureData<T> DequeueBuffer();

        void EequeueBuffer(IScreenCaptureData<T> data);


        event EventHandler<CapturedEventArgs<T>> Captured;
    }

    class CapturedEventArgs<T> : EventArgs
    {
        public IScreenCaptureData<T> Data { get; private set; }
        public CapturedEventArgs(IScreenCaptureData<T> data)
        {
            Data = data;
        }
    }
}


