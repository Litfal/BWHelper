using System;
using System.Collections.Generic;

namespace System.Collections.Generic
{
    public class CircleQueue<T>
        : IEnumerable<T>, ICollection, IEnumerable, ICloneable, IQueue<T>
    {
        public event EventHandler<CircleOverwriteEventArgs<T>> Overwrited;

        protected T[] mData = null;
        protected int DequeueIndex;
        protected int EnqueueIndex;
        public int MaxSize { get; private set; }
        protected int _Count;
        bool _overwrite;
        public int Count { get { return _Count; } }

        protected CircleQueue(int maxSize)
        {
            if (maxSize <= 0)
                throw new ArgumentOutOfRangeException("CircleQueue 空間必須大於0");
            _overwrite = false;
            MaxSize = maxSize;
            mData = new T[maxSize];
            EnqueueIndex = 0;
            DequeueIndex = 0;
            _Count = 0;
        }

        public CircleQueue(int maxSize, bool OverwriteIfFull)
        {
            if (maxSize <= 0)
                throw new ArgumentOutOfRangeException("CircleQueue 空間必須大於0");
            _overwrite = OverwriteIfFull;
            MaxSize = maxSize;
            mData = new T[maxSize];
            EnqueueIndex = 0;
            DequeueIndex = 0;
            _Count = 0;
        }

        public virtual void Enqueue(T item)
        {

            if (_Count == MaxSize && !_overwrite)
                throw new InvalidOperationException("CircleQueue 已滿");
            else
            {
                T overItem = mData[DequeueIndex];

                mData[EnqueueIndex] = item;
                int nextEnIndex = EnqueueIndex + 1 < MaxSize ? EnqueueIndex + 1 : 0;
                EnqueueIndex = nextEnIndex;
                if (_Count == MaxSize)
                {
                    DequeueIndex = nextEnIndex;
                    OnOverwrited(overItem);
                }
                else
                    _Count++;
            }
        }

        private void OnOverwrited(T item)
        {
            if (Overwrited != null)
                Overwrited(this, new CircleOverwriteEventArgs<T>(item));
        }

        public virtual T Dequeue()
        {
            if (_Count == 0)
                throw new InvalidOperationException("Queue 是空的");
            else
            {
                T item = mData[DequeueIndex];
                mData[DequeueIndex] = default(T);
                DequeueIndex = DequeueIndex + 1 < MaxSize ? DequeueIndex + 1 : 0;
                _Count--;
                return item;
            }
        }

        public virtual T Peek()
        {
            if (_Count == 0)
                throw new InvalidOperationException("Queue 是空的");
            else
                return mData[DequeueIndex];
        }

        public virtual void Clear()
        {
            EnqueueIndex = 0;
            DequeueIndex = 0;
            _Count = 0;
            //不以迴圈設為default(T), 而將mData指向新的位址, 
            //以利用GC自行清除記憶體(如果沒有其他參考的話)
            mData = new T[MaxSize]; 
        }

        public virtual void CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException("array 為 Null 參照");
            if (index < 0)
                throw new ArgumentOutOfRangeException("index 小於零");
            if (array.Rank > 1)
                throw new ArgumentException("array 為多維");
            if(index >= array.Length)
                throw new ArgumentException("index 等於或大於 array  的長度");
            if (index + _Count > array.Length)
                throw new ArgumentException("來源 Queue 中的元素數大於從 index 到目的 array 末端的可用空間");

            if (_Count == 0)
                return;
            if (EnqueueIndex > DequeueIndex) //Copy one times
                Array.Copy(mData, DequeueIndex, array, index, _Count);
            else //Copy two times
            {
                Array.Copy(mData, DequeueIndex, array, index, MaxSize - DequeueIndex);
                index += (MaxSize - DequeueIndex);
                Array.Copy(mData, 0, array, index, EnqueueIndex);
            }
        }

        public virtual CircleQueue<T> Clone()
        {
            CircleQueue<T> clnCQ = new CircleQueue<T>(MaxSize, _overwrite);
            this.CopyTo(clnCQ.mData, 0);
            clnCQ.EnqueueIndex = this._Count;
            clnCQ._Count = this._Count;
            return clnCQ;
        }
        object ICloneable.Clone() { return this.Clone(); }

        System.Collections.Generic.IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            int currentIndex = DequeueIndex;
            int ECount = 0;
            while (ECount < _Count)
            {
                yield return mData[currentIndex];
                currentIndex = currentIndex + 1 < MaxSize ? currentIndex + 1 : 0;
                ECount++;
            }
        }

        System.Collections.IEnumerator IEnumerable.GetEnumerator()
        {
            int currentIndex = DequeueIndex;
            int ECount = 0;
            while (ECount < _Count)
            {
                yield return mData[currentIndex];
                currentIndex = currentIndex + 1 < MaxSize ? currentIndex + 1 : 0;
                ECount++;
            }
        }

        object ICollection.SyncRoot { get { return this; } }
        public virtual object SyncRoot { get { return this; } }
        bool ICollection.IsSynchronized { get { return false; } }
        public virtual bool IsSynchronized { get { return false; } }
    }
}
