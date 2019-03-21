using System;
using System.Collections.Generic;

namespace NET.S._2019.Dremliug._01
{
    /// <summary> Provides extension methods for sorting an integer array. </summary>
    public static class ArraySortExtensions
    {
        #region MergeSort

        /// <summary> Sorts with Quick sort algorithm. </summary>
        /// <param name="array"></param>
        public static void MergeSort(this int[] array)
        {
            #region Validation

            if (array is null)
            {
                throw new ArgumentNullException();
            }

            if (array.Length == 1)
            {
                return;
            }

            #endregion

            #region Initialization

            // Queue will contain sorted arrays.
            Queue<int[]> partsToMerge = new Queue<int[]>();

            // Split given array into single element arrays. Single element array is sorted.
            foreach (var v in array)
            {
                partsToMerge.Enqueue(new int[] { v });
            }

            #endregion

            #region Processing

            while (partsToMerge.Count > 1)
            {
                // Take two arrays from the queue and create new array for sorted data.
                int[] partA = partsToMerge.Dequeue();
                int indexA = 0;
                int lengthA = partA.Length;

                int[] partB = partsToMerge.Dequeue();
                int indexB = 0;
                int lengthB = partB.Length;

                int[] mergeResult = new int[lengthA + lengthB];
                int indexResult = 0;

                // Merge two arrays into the resulting sorted array.
                while (indexA < lengthA && indexB < lengthB)
                {
                    if (partA[indexA] <= partB[indexB])
                    {
                        mergeResult[indexResult] = partA[indexA];
                        indexA++;
                    }
                    else
                    {
                        mergeResult[indexResult] = partB[indexB];
                        indexB++;
                    }

                    indexResult++;
                }

                // Copy the remaining elements.
                while (indexA < lengthA)
                {
                    mergeResult[indexResult] = partA[indexA];
                    indexA++;
                    indexResult++;
                }

                // Copy the remaining elements.
                while (indexB < lengthB)
                {
                    mergeResult[indexResult] = partB[indexB];
                    indexB++;
                    indexResult++;
                }

                // Put sorted result back into the queue.
                partsToMerge.Enqueue(mergeResult);
            }

            // Now queue contains one sorted array. Fill the original array with sorted data.
            int[] sorted = partsToMerge.Dequeue();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = sorted[i];
            }

            #endregion
        }

        #endregion

        #region QuickSort

        /// <summary> Sorts with Quick sort algorithm. </summary>
        /// <param name="array"></param>
        public static void QuickSort(this int[] array)
        {
            #region Validation

            if (array is null)
            {
                throw new ArgumentNullException();
            }

            if (array.Length == 1)
            {
                return;
            }

            #endregion

            #region Processing

            QuickSortRecursive(array, 0, array.Length - 1);

            void QuickSortRecursive(int[] arr, int startIndex, int endIndex)
            {
                if (startIndex < endIndex)
                {
                    // Get pivot index.
                    int pivotIndex = Partition(arr, startIndex, endIndex);

                    // Sort to the left of the pivot position. 
                    QuickSortRecursive(arr, startIndex, pivotIndex - 1);
                    // Sort to the right of the pivot position.
                    QuickSortRecursive(arr, pivotIndex + 1, endIndex);
                }
            }

            int Partition(int[] arr, int startIndex, int endIndex)
            {
                // Use the first element as a pivot.
                int pivotValue = arr[startIndex];
                int temp;

                // Index of the last element with a value less than the pivot value. 
                int indexOfLastSmaller = startIndex;
                for (int i = startIndex + 1; i <= endIndex; i++)
                {
                    // If current element is smaller than or equal to the pivot value.
                    if (arr[i] <= pivotValue)
                    {
                        indexOfLastSmaller++;

                        // Swap.
                        temp = arr[indexOfLastSmaller];
                        arr[indexOfLastSmaller] = arr[i];
                        arr[i] = temp;
                    }
                }

                // Place the pivot element at the correct position, after the last element smaller than the pivot value. 
                temp = arr[indexOfLastSmaller];
                arr[indexOfLastSmaller] = arr[startIndex];
                arr[startIndex] = temp;

                // Return the index of the pivot.
                return indexOfLastSmaller;
            }

            #endregion
        }

        #endregion
    }

}

