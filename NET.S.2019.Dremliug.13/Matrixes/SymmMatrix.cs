using System;

namespace Matrixes
{
    /// <summary>
    /// Represents a generic symmetric matrix.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SymmMatrix<T> : SquareMatrix<T>
    {
        #region Constructors
        /// <summary>
        /// Creates an empty symmetric matrix.
        /// </summary>
        public SymmMatrix() : base()
        {
        }

        /// <summary>
        /// Creates a symmetric matrix of given size.
        /// </summary>
        /// <param name="size"></param>
        public SymmMatrix(int size) : base(size)
        {
        }

        /// <summary>
        /// Creates a symmetric matrix from a two-dimensional array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="fromTop"> Use the values above the main diagonal if true, otherwise use the values below.</param>
        public SymmMatrix(T[,] array, bool fromTopPart)
        {
            if (array is null)
            {
                throw new ArgumentNullException($"Array must not be null.");
            }

            this._array = this.ToSymmMatrix(array, fromTopPart);
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
                    this._array[i, j] = value;
                    this.OnElementChanged(new ElementChangedEventArgs<T>(i, j, value));

                    if (i != j)
                    {
                        this._array[j, i] = value;
                        this.OnElementChanged(new ElementChangedEventArgs<T>(j, i, value));
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
        /// Creates a symmetric matrix from a two-dimensional array. Use in constructor and explicit cast.
        /// Converts null array to null.
        /// </summary>
        /// <param name="array"></param>
        /// <returns> New symmetric array. </returns>
        private T[,] ToSymmMatrix(T[,] array, bool fromTopPart)
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

                    for (int j = i + 1; j < size; j++)
                    {
                        T value;

                        // Get the value from the given part of the array.
                        if (fromTopPart)
                        {
                            value = array[i, j];
                        }
                        else
                        {
                            value = array[j, i];
                        }

                        // Place a symmetrical copy in both parts of the result. 
                        result[i, j] = value;
                        result[j, i] = value;
                    }
                }
            }

            return result;
        }
        #endregion
    }
}
