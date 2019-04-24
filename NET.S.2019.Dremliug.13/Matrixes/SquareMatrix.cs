using System;
using System.Collections;

namespace Matrixes
{
    /// <summary>
    /// Represents a generic square matrix.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SquareMatrix<T> : IEnumerable
    {
        #region Fields
        // Use property. https://csharpindepth.com/Articles/PropertiesMatter
        // protected T[,] _array;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an empty square matrix.
        /// </summary>
        public SquareMatrix()
        {
            this._array = new T[0, 0];
        }

        /// <summary>
        /// Creates a square matrix of given size.
        /// </summary>
        /// <param name="size"></param>
        public SquareMatrix(int size)
        {
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException("Size must be non-negative.");
            }

            this._array = new T[size, size];
        }

        /// <summary>
        /// Creates a square matrix from a two-dimensional array.
        /// </summary>
        /// <param name="array"></param>
        public SquareMatrix(T[,] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException($"Array must not be null.");
            }

            this._array = ToSquareMatrix(array);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when matrix element changed.
        /// </summary>
        public event EventHandler<ElementChangedEventArgs<T>> ElementChanged;
        #endregion

        #region Properties
        public int Size { get => _array.GetLength(0); }

        protected T[,] _array { get; set; }
        #endregion

        #region Indexer
        /// <summary>
        /// Get or set the element at the given position.
        /// </summary>
        /// <param name="i">Row index.</param>
        /// <param name="j">Column index.</param>
        /// <returns></returns>
        public virtual T this[int i, int j]
        {
            get
            {
                if (IndexesAreCorrect(i, j))
                {
                    return this._array[i, j];
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Indexes are out of range i: {i}, j: {j}");
                }
            }

            set
            {
                if (IndexesAreCorrect(i, j))
                {
                    this._array[i, j] = value;
                    this.OnElementChanged(new ElementChangedEventArgs<T>(i, j, value));
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Indexes are out of range i: {i}, j: {j}");
                }
            }
        }
        #endregion

        #region IEnumerable
        public IEnumerator GetEnumerator() => _array.GetEnumerator();
        #endregion

        #region OnEvent
        protected virtual void OnElementChanged(ElementChangedEventArgs<T> args)
            => ElementChanged?.Invoke(this, args);
        #endregion

        #region Protected methods
        /// <summary>
        /// Checks indexes before using them in indexer.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        protected bool IndexesAreCorrect(int i, int j)
        {
            bool areCorrect = false;

            if (i >= 0 && i < Size &&
                j >= 0 && j < Size)
            {
                areCorrect = true;
            }

            return areCorrect;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Creates a square matrix from a two-dimensional array. Use in constructor and explicit cast.
        /// Converts null array to null.
        /// </summary>
        /// <param name="array"></param>
        /// <returns> New square array. </returns>
        private T[,] ToSquareMatrix(T[,] array)
        {
            T[,] result = null;

            if (array != null)
            {
                int size = Math.Min(array.GetLength(0), array.GetLength(1));
                result = new T[size, size];

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        result[i, j] = array[i, j];
                    }
                }
            }

            return result;
        }
        #endregion

        #region ElementChangedEventArgs
        public class ElementChangedEventArgs<K> : EventArgs
        {
            public ElementChangedEventArgs(int i, int j, K value)
            {
                this.Iindex = i;
                this.Jindex = j;
                this.Value = value;
            }

            public K Value { get; }

            public int Iindex { get; }

            public int Jindex { get; }
        }
        #endregion
    }
}
