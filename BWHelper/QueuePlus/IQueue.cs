using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic
{
    public interface IQueue<T>
    {
        void Enqueue(T item);
        T Dequeue();
        void Clear();
        int Count { get; }
    }
}
