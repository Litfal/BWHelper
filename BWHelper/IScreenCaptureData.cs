using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWHelper
{
    interface IScreenCaptureData<T>
    {
        T GetData();

        void SetData(T data);

        Size Size { get; set; }

        void Dispose();
    }
}
