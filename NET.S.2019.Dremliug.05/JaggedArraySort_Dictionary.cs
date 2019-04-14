using System;
using System.Collections.Generic;
using System.Linq;

namespace NET.S._2019.Dremliug._05
{
    /// <summary>
    /// Provides methods for sorting jagged matrix of integers.
    /// </summary>
    class JaggedArraySort_Dictionary
    {
        #region public methods
        /// <summary> Sorts the rows of the jagged array by the sum of each row. </summary>
        /// <param name="jaggedMatrix"> Matrix to sort. </param>
        /// <param name="inDescendingOrder"> Enable descending order. </param>
        public static void SortRowsBySum(int[][] jaggedMatrix, bool inDescendingOrder = false) => Sort(jaggedMatrix, Mode.Sum, inDescendingOrder);

        /// <summary> Sorts the rows of the jagged array by the maximum value of each row. </summary>
        /// <param name="jaggedMatrix"> Matrix to sort. </param>
        /// <param name="inDescendingOrder"> Enable descending order. </param>
        public static void SortRowsByMax(int[][] jaggedMatrix, bool inDescendingOrder = false) => Sort(jaggedMatrix, Mode.Max, inDescendingOrder);

        /// <summary> Sorts the rows of the jagged array by the minimum value of each row. </summary>
        /// <param name="jaggedMatrix"> Matrix to sort. </param>
        /// <param name="inDescendingOrder"> Enable descending order. </param>
        public static void SortRowsByMin(int[][] jaggedMatrix, bool inDescendingOrder = false) => Sort(jaggedMatrix, Mode.Min, inDescendingOrder);
        #endregion

        #region sorting modes
        /// <summary> Supported sorting modes. </summary>
        private enum Mode
        {
            Sum, Max, Min
        }
        #endregion

        #region private Sort()
        /// <summary> Sorts jagged matrix. </summary>
        /// <param name="jaggedMatrix"> Matrix to sort. </param>
        /// <param name="mode"> Sorting mode. </param>
        /// <param name="descending"> Sort descending. </param>
        private static void Sort(int[][] jaggedMatrix, Mode mode, bool descending = false)
        {

            // Select a method for calculating the sort key.
            Func<IEnumerable<int>, Func<int, long>, long> calculateKey;
            switch (mode)
            {
                case Mode.Sum:
                    calculateKey = Enumerable.Sum;
                    break;
                case Mode.Max:
                    calculateKey = Enumerable.Max;
                    break;
                case Mode.Min:
                    calculateKey = Enumerable.Min;
                    break;
                default:
                    calculateKey = Enumerable.Sum;
                    break;
            }

            // Create a dictionary that stores the sort key of each row in the jagged matrix.
            // Key = reference to int[], Value = calculated sort key.
            var sortKeys = new Dictionary<int[], long?>();

            for (int i = 0; i < jaggedMatrix.Length; i++)
            {
                if (!(jaggedMatrix[i] is null))
                {
                    if (!jaggedMatrix[i].Any())
                    {
                        sortKeys[jaggedMatrix[i]] = (long?)null;
                    }
                    else
                    {
                        sortKeys[jaggedMatrix[i]] = calculateKey(jaggedMatrix[i], (int x) => (long)x);
                    }
                }
            }

            // Sort the matrix.
            bubbleSort();

            #region local Bubble sort routine
            void bubbleSort()
            {
                // Select a comparison method depending on the sotring order.
                Func<long?, long?, bool> compare;
                if (descending)
                {
                    // a < b
                    compare = (a, b) => Nullable.Compare(a, b) < 0;
                }
                else
                {
                    // a > b
                    compare = (a, b) => Nullable.Compare(a, b) > 0;
                }

                // Sort.
                for (int i = 0; i < jaggedMatrix.Length - 1; i++)
                    for (int j = 0; j < jaggedMatrix.Length - i - 1; j++)
                    {
                        if (compare(
                            jaggedMatrix[j] is null ? null : sortKeys[jaggedMatrix[j]],
                            jaggedMatrix[j + 1] is null ? null : sortKeys[jaggedMatrix[j + 1]]))
                        {
                            // Swap. 
                            (jaggedMatrix[j], jaggedMatrix[j + 1]) = (jaggedMatrix[j + 1], jaggedMatrix[j]);
                        }
                    }
            }
            #endregion
        }
        #endregion
    }
}
