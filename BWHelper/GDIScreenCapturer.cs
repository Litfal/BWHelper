using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BWHelper
{
    class GDIScreenCapturerData : IScreenCaptureData<Bitmap>
    {
        private Bitmap bmp;

        public GDIScreenCapturerData()
        {

        }

        public Bitmap GetData()
        {
            return bmp;
        }

        public void SetData(Bitmap data)
        {
            this.bmp = data;
        }

        public void Dispose()
        {
            if (bmp != null)
            {
                bmp.Dispose();
                bmp = null;
            }
        }

        public Size Size
        {
            get;
            set;
        }
    }

    class GDIScreenCapturer :GDIScreenCapturerBase<Bitmap>
    {
        //public override int BackBufferSize
        //{
        //    get { return 8; }
        //}

        protected override IScreenCaptureData<Bitmap> createCaptureData()
        {
            return new GDIScreenCapturerData();
        }

        protected override void FillBitmapToBuffer(Bitmap bmp, IScreenCaptureData<Bitmap> buffer)
        {
            buffer.SetData(bmp);
        }

        protected override Bitmap GetBitmapFromBuffer(IScreenCaptureData<Bitmap> buffer)
        {
            return buffer.GetData();
        }
    }
}
