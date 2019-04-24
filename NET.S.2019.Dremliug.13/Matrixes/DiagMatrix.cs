using System;

namespace Matrixes
{
    /// <summary>
    /// Represents a generic diagonal matrix.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DiagMatrix<T> : SymmMatrix<T>
    {
        #region Constructors
        /// <summary>
        /// Creates an empty diagonal matrix.
        /// </summary>
        public DiagMatrix() : base()
        {
        }

        /// <summary>
        /// Creates a diagonal matrix of given size.
        /// </summary>
        /// <param name="size"></param>
        public DiagMatrix(int size) : base(size)
        {
        }

        /// <summary>
        /// Creates a diagonal matrix from a two-dimensional array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="fromTop"> Use the values above the main diagonal if true, otherwise use the values below.</param>
        public DiagMatrix(T[,] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException($"Array must not be null.");
            }

            this._array = this.ToDiagMatrix(array);
        }
        #endregion

        #region Indexer
        /// <summary>
        /// Get or set the element at the given position.
        /// </summary>
        /// <param name="i">Row index.</param>
        /// <param name="j">Column index.</param>
        /// <returns></returns>
        public override T this[int i, int j]
        {
            get
            {
                if (this.IndexesAreCorrect(i, j))
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
                if (this.IndexesAreCorrect(i, j))
                {
                    // Only the main diag slots are open for set.
                    // Silently do nothing on attempt to set element outside the main diag. 
                    if (i == j)
                    {
                        this._array[i, j] = value;
                        this.OnElementChanged(new ElementChangedEventArgs<T>(i, j, value));
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Indexes are out of range i: {i}, j: {j}");
                }
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Creates a diagonal matrix from a two-dimensional array. Use in constructor and explicit cast.
        /// Converts null array to null.
        /// </summary>
        /// <param name="array"></param>
        /// <returns> New diagonal array. </returns>
        private T[,] ToDiagMatrix(T[,] array)
        {
            T[,] result = null;

            if (array != null)
            {
                int size = Math.Min(array.GetLength(0), array.GetLength(1));
                result = new T[size, size];

                for (int i = 0; i < size; i++)
                {
                    // Copy the values of the main diag;
                    result[i, i] = array[i, i];
                }
            }

            return result;
        }
        #endregion
    }
}
