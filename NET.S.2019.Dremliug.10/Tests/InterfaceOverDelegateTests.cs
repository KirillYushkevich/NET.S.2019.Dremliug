using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JaggedArray.InterfaceInDelegate;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class InterfaceOverDelegateTests
    {
        private int[][] actual;

        public void CreateActual()
        {
            actual = new[]
            {
                new[] { 3, 2, 1, 5, 8 },
                null,
                new int[] { },
                new[] { 3, 2, 6, 1 },
                new[] { 2, 4 },
                new[] { int.MaxValue, int.MaxValue, 1 },
                new[] { 2, 5, -3 },
                null,
            };
        }

        [Test]
        public void SortRowsBySumAscending()
        {
            int[][] expected = new[]
            {
                null,
                new int[] { },
                null,
                new[] { 2, 5, -3 },
                new[] { 2, 4 },
                new[] { 3, 2, 6, 1 },
                new[] { 3, 2, 1, 5, 8 },
                new[] { int.MaxValue, int.MaxValue, 1 },
            };

            CreateActual();
            JaggedArraySort.SortRowsBySum(actual);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SortRowsBySumDescending()
        {
            int[][] expected = new[]
            {
                new[] { int.MaxValue, int.MaxValue, 1 },
                new[] { 3, 2, 1, 5, 8 },
                new[] { 3, 2, 6, 1 },
                new[] { 2, 4 },
                new[] { 2, 5, -3 },
                null,
                new int[] { },
                null,
            };

            CreateActual();
            JaggedArraySort.SortRowsBySum(actual, inDescendingOrder: true);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SortRowsByMaxAscending()
        {
            int[][] expected = new[]
            {
                null,
                new int[] { },
                null,
                new[] { 2, 4 },
                new[] { 2, 5, -3 },
                new[] { 3, 2, 6, 1 },
                new[] { 3, 2, 1, 5, 8 },
                new[] { int.MaxValue, int.MaxValue, 1 },
            };

            CreateActual();
            JaggedArraySort.SortRowsByMax(actual);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SortRowsByMaxDescending()
        {
            int[][] expected = new[]
            {
                new[] { int.MaxValue, int.MaxValue, 1 },
                new[] { 3, 2, 1, 5, 8 },
                new[] { 3, 2, 6, 1 },
                new[] { 2, 5, -3 },
                new[] { 2, 4 },
                null,
                new int[] { },
                null,
            };

            CreateActual();
            JaggedArraySort.SortRowsByMax(actual, inDescendingOrder: true);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SortRowsByMinAscending()
        {
            int[][] expected = new[]
            {
                null,
                new int[] { },
                null,
                new[] { 2, 5, -3 },
                new[] { 3, 2, 1, 5, 8 },
                new[] { 3, 2, 6, 1 },
                new[] { int.MaxValue, int.MaxValue, 1 },
                new[] { 2, 4 },
            };

            CreateActual();
            JaggedArraySort.SortRowsByMin(actual);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SortRowsByMinDescending()
        {
            int[][] expected = new[]
            {
               new[] { 2, 4 },
               new[] { 3, 2, 1, 5, 8 },
               new[] { 3, 2, 6, 1 },
               new[] { int.MaxValue, int.MaxValue, 1 },
               new[] { 2, 5, -3 },
               null,
               new int[] { },
               null,
            };

            CreateActual();
            JaggedArraySort.SortRowsByMin(actual, inDescendingOrder: true);

            Assert.AreEqual(expected, actual);
        }
    }
}
