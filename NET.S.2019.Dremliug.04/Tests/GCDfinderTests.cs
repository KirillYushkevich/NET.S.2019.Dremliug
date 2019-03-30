using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NET.S._2019.Dremliug._04.Tests
{
    [TestFixture]
    class GCDfinderTests
    {
        #region Test data
        private static IEnumerable<TestCaseData> DataForGCDTests
        {
            get
            {
                yield return new TestCaseData(16, new[] { 64, 48 });
                yield return new TestCaseData(1, new[] { 661, 113 });
                yield return new TestCaseData(48, new[] { 0, 48 });
                yield return new TestCaseData(48, new[] { 48, 0 });
                yield return new TestCaseData(1, new[] { 1, 42 });
                yield return new TestCaseData(1, new[] { 64, 1 });
                yield return new TestCaseData(6, new[] { 78, 294, 570, 36 });
                yield return new TestCaseData(528, new[] { 6336, 9504, 4752, 7392 });
                yield return new TestCaseData(9, new[] { -585, 81, -189 });
            }
        }

        private static IEnumerable<TestCaseData> DataForGCDExeptionsTests
        {
            get
            {
                yield return new TestCaseData(new int[] {});
                yield return new TestCaseData(new[] { 1 });
                yield return new TestCaseData(new[] { 48 });
                yield return new TestCaseData(new[] { 0, 0 });
                yield return new TestCaseData(null);
            }
        }
        #endregion

        #region EuclideanGCDTest
        [TestCaseSource(nameof(DataForGCDTests))]
        public void EuclideanGCDTest_NoElapsedTime(int expected, int[] numbers)
            => Assert.AreEqual(expected, GCDfinder.EuclideanGCD(numbers));

        [TestCaseSource(nameof(DataForGCDTests))]
        public void EuclideanGCDTest_WithElapsedTime(int expected, int[] numbers)
        {
            int actual = GCDfinder.EuclideanGCD(out var time, numbers);
            Assert.AreEqual(expected, actual);
            Assert.IsInstanceOf<TimeSpan>(time);
        }

        [TestCaseSource(nameof(DataForGCDExeptionsTests))]
        public void EuclideanGCDExceptionsTest_NoElapsedTime(int[] numbers)
        {
            if (numbers is null)
            {
                Assert.Throws<ArgumentNullException>(() => GCDfinder.EuclideanGCD(numbers));
            }
            else
            {
                Assert.Throws<ArgumentException>(() => GCDfinder.EuclideanGCD(numbers));
            }
        }

        [TestCaseSource(nameof(DataForGCDExeptionsTests))]
        public void EuclideanGCDExceptionsTest_WithElapsedTime(int[] numbers)
        {
            if (numbers is null)
            {
                Assert.Throws<ArgumentNullException>(() => GCDfinder.EuclideanGCD(out var time, numbers));
            }
            else
            {
                Assert.Throws<ArgumentException>(() => GCDfinder.EuclideanGCD(out var time, numbers));
            }
        }
        #endregion

        #region SteinGCDTest
        [TestCaseSource(nameof(DataForGCDTests))]
        public void SteinGCDTest_NoElapsedTime(int expected, int[] numbers)
            => Assert.AreEqual(expected, GCDfinder.SteinGCD(numbers));

        [TestCaseSource(nameof(DataForGCDTests))]
        public void SteinGCDTest_WithElapsedTime(int expected, params int[] numbers)
        {
            int actual = GCDfinder.SteinGCD(out var time, numbers);
            Assert.AreEqual(expected, actual);
            Assert.IsInstanceOf<TimeSpan>(time);
        }

        [TestCaseSource(nameof(DataForGCDExeptionsTests))]
        public void SteinGCDExceptionsTest_NoElapsedTime(int[] numbers)
        {
            if (numbers is null)
            {
                Assert.Throws<ArgumentNullException>(() => GCDfinder.SteinGCD(numbers));
            }
            else
            {
                Assert.Throws<ArgumentException>(() => GCDfinder.SteinGCD(numbers));
            }
        }

        [TestCaseSource(nameof(DataForGCDExeptionsTests))]
        public void SteinGCDExceptionsTest_WithElapsedTime(int[] numbers)
        {
            if (numbers is null)
            {
                Assert.Throws<ArgumentNullException>(() => GCDfinder.SteinGCD(out var time, numbers));
            }
            else
            {
                Assert.Throws<ArgumentException>(() => GCDfinder.SteinGCD(out var time, numbers));
            }
        }
        #endregion
    }
}
