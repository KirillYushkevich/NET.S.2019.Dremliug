using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QueueGeneric
{
    /// <summary>
    /// Generic Queue with basic operations support.
    /// </summary>
    public class QueueG<T> : IEnumerable<T>
    {
        // Use List<T>. Makes Dequeue() O(n) time but noone is going to use this class anyway.
        private List<T> _items;

        public QueueG() => _items = new List<T>();

        public QueueG(IEnumerable<T> collection) => _items = collection?.ToList() ?? throw new ArgumentNullException($"collection is null");

        public void Enqueue(T item) => _items.Add(item);

        public T Dequeue()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            T item = _items[0];
            _items.Remove(item);

            return item;
        }

        public void Clear() => _items.Clear();

        public T Peek() => _items[0];

        public bool Contains(T item) => _items.Contains(item);

        public T[] ToArray() => _items.ToArray();

        #region IEnumerable
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(_items);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        #region IEnumerator
        public class Enumerator : IEnumerator<T>
        {
            private List<T> _items;
            private int _index;

            public Enumerator(List<T> list)
            {
                _items = list;
                _index = -1;
            }

            public T Current => _items[_index];

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                _index++;

                if (_index >= _items.Count)
                {
                    return false;
                }

                return true;
            }

            public void Reset() => _index = -1;

            public void Dispose()
            {
            }
        }
        #endregion
    }
}
