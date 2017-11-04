using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Collections.Generic
{
    public class CircleOverwriteEventArgs<T> : EventArgs
    {
        public T Item { get; private set; }
        public CircleOverwriteEventArgs(T item)
        {
            this.Item = item;
        }
    }
}
