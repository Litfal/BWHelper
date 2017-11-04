using System;
using System.Collections.Generic;

namespace System.Collections.Generic
{
    public class BlockQueue<T> : IQueue<T>
    {
        public event EventHandler<CircleOverwriteEventArgs<T>> Overwrited;


        public delegate T CreateItemFunc();
        public delegate T CreateItemFuncP(object arg);

        object lck;
        System.Threading.ManualResetEvent EnQWait;
        System.Threading.ManualResetEvent DeQWait;
        bool _IsBlock;
        bool _ThrowException;

        IQueue<T> m_innerQueue;
        int m_maxSize;
        int m_innerCount = 0;
        bool m_overwriteIfFull = false;

        public int MaxSize { get { return m_maxSize; } }

        public BlockQueue(int maxSize, bool overwriteIfFull = false)
        {
            this.m_overwriteIfFull = overwriteIfFull;
            var cq = new CircleQueue<T>(maxSize, overwriteIfFull);
            if (overwriteIfFull) cq.Overwrited += this.OnOverwrited;
            m_innerQueue = cq;
            m_maxSize = maxSize;
            this.lck = new object();
            this.EnQWait = new System.Threading.ManualResetEvent(false);
            this.DeQWait = new System.Threading.ManualResetEvent(false);
            this._IsBlock = true;
            
        }

        private void OnOverwrited(object sender, CircleOverwriteEventArgs<T> e)
        {
            if (Overwrited != null)
                Overwrited(this, e);
        }

        public BlockQueue()
        {
            m_innerQueue = new BaseQueue<T>();
            m_maxSize = -1;
            this.lck = new object();
            this.EnQWait = new System.Threading.ManualResetEvent(false);
            this.DeQWait = new System.Threading.ManualResetEvent(false);
            this._IsBlock = true;
        }

        public void Enqueue(T item)
        {
            while (true)
            {
                lock (lck)
                {
                    if (m_maxSize < 0 || m_innerCount < m_maxSize)
                    {
                        m_innerCount++;
                        m_innerQueue.Enqueue(item);
                        this.DeQWait.Set();
                        return;
                    }
                    else if(m_overwriteIfFull && m_innerCount == m_maxSize)
                    {
                        m_innerQueue.Enqueue(item);
                        this.DeQWait.Set();
                        return;
                    }
                    this.EnQWait.Reset();
                }
                this.EnQWait.WaitOne();
            }
        }

        public void Enqueue(CreateItemFunc func)
        {
            while (true)
            {
                bool pass = false;
                lock (lck)
                {
                    if (m_maxSize < 0 || m_innerCount < m_maxSize)
                    {
                        m_innerCount++;
                        pass = true;
                    }
                }
                if (pass)
                {
                    m_innerQueue.Enqueue(func());
                    this.DeQWait.Set();
                    return;
                }
                this.EnQWait.Reset();
                this.EnQWait.WaitOne();
            }
        }

        public void Enqueue(CreateItemFuncP func, object arg)
        {
            while (true)
            {
                bool pass = false;
                lock (lck)
                {
                    if (m_maxSize < 0 || m_innerCount < m_maxSize)
                    {
                        m_innerCount++;
                        pass = true;
                    }
                }
                if (pass)
                {
                    m_innerQueue.Enqueue(func(arg));
                    this.DeQWait.Set();
                    return;
                }
                this.EnQWait.Reset();
                this.EnQWait.WaitOne();
            }
        }

        public T Dequeue()
        {
            while (true)
            {
                lock (lck)
                {
                    if (m_innerQueue.Count > 0)
                    {
                        T item = m_innerQueue.Dequeue();
                        m_innerCount--;
                        this.EnQWait.Set();
                        return item;
                    }
                    else if (!_IsBlock)
                    {
                        if (_ThrowException)
                            throw new InvalidOperationException("Queue 是空的");
                        else
                            return default(T);
                    }
                    this.DeQWait.Reset();
                }
                this.DeQWait.WaitOne();
            }
        }

        public void Clear()
        {
            lock (lck)
                m_innerQueue.Clear();
            this.EnQWait.Set();
        }

        public int Count { get { return m_innerQueue.Count; } }

        /// <summary>
        /// 設定封鎖, Dequeue()將會於序列為空時被封鎖
        /// </summary>
        public void SetBlock()
        {
            lock (lck)
                this._IsBlock = true;
        }

        /// <summary>
        /// 取消封鎖, Dequeue()將會於序列為空時依throwException指定值觸發不同結果
        /// <para>throwException = true : 將會 throw InvalidOperationException</para>
        /// <para>throwException = false : Dequeue() 回傳型別T的預設值 (若為參考型別則為null), 實值型別則為預設值</para>
        /// </summary>
        /// <param name="throwException"></param>
        public void ResetBlock(bool throwException)
        {
            lock (lck)
            {
                this._IsBlock = false;
                this._ThrowException = throwException;
                this.DeQWait.Set();
            }
        }
    }
}
