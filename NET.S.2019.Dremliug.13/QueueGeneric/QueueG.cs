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
        private LinkedList<T> _items;

        public QueueG() => _items = new LinkedList<T>();

        public QueueG(IEnumerable<T> collection) : this()
        {
            if (collection is null)
            {
                throw new ArgumentNullException($"collection is null");
            }

            foreach (T item in collection)
            {
                _items.AddLast(item);
            }
        }

        public void Enqueue(T item) => _items.AddLast(item);

        public T Dequeue()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            T item = _items.First.Value;
            _items.RemoveFirst();

            return item;
        }

        public void Clear() => _items.Clear();

        public T Peek() => _items.First.Value;

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
            private LinkedList<T> _linkedList;
            private LinkedListNode<T> _currentNode;

            public Enumerator(LinkedList<T> linkedList)
            {
                _linkedList = linkedList;
                _currentNode = null;
            }

            public T Current
            {
                get
                {
                    return _currentNode is null ? throw new InvalidOperationException() : _currentNode.Value;
                }
            }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (ReferenceEquals(_currentNode, _linkedList.Last))
                {
                    return false;
                }

                _currentNode = _currentNode?.Next ?? _linkedList.First;

                return true;
            }

            public void Reset() => _currentNode = null;

            public void Dispose()
            {
            }
        }
        #endregion
    }
}
