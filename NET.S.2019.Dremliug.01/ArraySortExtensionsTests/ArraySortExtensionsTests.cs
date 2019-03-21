using System;
using NUnit.Framework;

namespace NET.S._2019.Dremliug._01.ArraySortExtensionsTests
{
    class ArraySortExtensionsTests
    {
        #region SortTests

        [TestCase(new[] { 4 }, new[] { 4 })]
        [TestCase(new[] { 8, 4, 6, 3, 7 }, new[] { 3, 4, 6, 7, 8 })]
        [TestCase(
            new[] { -29, 44, 41, -9, -33, 33, 38, 46, -47, -34, -16, 40, -15, 24, 45, 47, 42, -14, 25, -6, 4, 5, 1, 44, -50, -44, 26, 31, -2, -26, -45, -39, 40, 45, 47, -34, -5, 2, 34, 12 },
            new[] { -50, -47, -45, -44, -39, -34, -34, -33, -29, -26, -16, -15, -14, -9, -6, -5, -2, 1, 2, 4, 5, 12, 24, 25, 26, 31, 33, 34, 38, 40, 40, 41, 42, 44, 44, 45, 45, 46, 47, 47 })]

        public void MergeSortTests(int[] array, int[] result)
        {
            array.MergeSort();
            Assert.AreEqual(result, array);
        }

        [TestCase(new[] { 4 }, new[] { 4 })]
        [TestCase(new[] { 8, 4, 6, 3, 7 }, new[] { 3, 4, 6, 7, 8 })]
        [TestCase(
            new[] { -29, 44, 41, -9, -33, 33, 38, 46, -47, -34, -16, 40, -15, 24, 45, 47, 42, -14, 25, -6, 4, 5, 1, 44, -50, -44, 26, 31, -2, -26, -45, -39, 40, 45, 47, -34, -5, 2, 34, 12 },
            new[] { -50, -47, -45, -44, -39, -34, -34, -33, -29, -26, -16, -15, -14, -9, -6, -5, -2, 1, 2, 4, 5, 12, 24, 25, 26, 31, 33, 34, 38, 40, 40, 41, 42, 44, 44, 45, 45, 46, 47, 47 })]
        public void QuickSortTests(int[] array, int[] result)
        {
            array.QuickSort();
            Assert.AreEqual(result, array);
        }

        #endregion

        #region NullArgumentTests

        [TestCase(null)]
        public void MergeSortArgumentIsNullTest(int[] array)
        {
            Assert.Throws<ArgumentNullException>(() => array.MergeSort());
        }

        [TestCase(null)]
        public void QuickSortArgumentIsNullTest(int[] array)
        {
            Assert.Throws<ArgumentNullException>(() => array.QuickSort());
        }

        #endregion
    }
}
