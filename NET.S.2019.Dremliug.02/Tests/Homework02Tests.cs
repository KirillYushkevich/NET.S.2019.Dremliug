using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NET.S._2019.Dremliug._02.Tests
{
    [TestFixture]
    class Homework02Tests
    {

        #region InsertNumber

        [TestCase(15, 15, 0, 0, ExpectedResult = 15)]
        [TestCase(8, 15, 0, 0, ExpectedResult = 9)]
        [TestCase(8, 15, 3, 8, ExpectedResult = 120)]
        [TestCase(-819436951, 1839623026, 7, 26, ExpectedResult = -885671575)]
        public int InsertNumberTests(int target, int source, int startOfBitRange, int endOfBitRange)
            => Homework02.InsertNumber(target, source, startOfBitRange, endOfBitRange);

        [TestCase(8, 15, 8, 3)]
        [TestCase(8, 15, -1, 8)]
        [TestCase(8, 15, 3, 32)]
        public void InsertNumberTests_WrongBitPositions_ThrowsArgumentOutOfRangeException(int target, int source, int startOfBitRange, int endOfBitRange)
            => Assert.Throws<ArgumentOutOfRangeException>(() => Homework02.InsertNumber(target, source, startOfBitRange, endOfBitRange));

        #endregion

        #region FindNextBiggerNumber

        [TestCase(12, ExpectedResult = 21)]
        [TestCase(513, ExpectedResult = 531)]
        [TestCase(2017, ExpectedResult = 2071)]
        [TestCase(414, ExpectedResult = 441)]
        [TestCase(144, ExpectedResult = 414)]
        [TestCase(1234321, ExpectedResult = 1241233)]
        [TestCase(1234126, ExpectedResult = 1234162)]
        [TestCase(3456432, ExpectedResult = 3462345)]
        [TestCase(10, ExpectedResult = -1)]
        [TestCase(20, ExpectedResult = -1)]
        public int FindNextBiggerNumberTests(int number)
            => Homework02.FindNextBiggerNumber(number);

        [TestCase(0)]
        [TestCase(-1)]
        public void FindNextBiggerNumberTests_NonPositiveNumber_ThrowsArgumentOutOfRangeException(int number)
            => Assert.Throws<ArgumentOutOfRangeException>(() => Homework02.FindNextBiggerNumber(number));

        #endregion

        #region TimeElapsed

        // How to find out if the return time value is correct or not?
        [TestCase(3456432)]
        public void TimeElapsedTests(int number)
            => Assert.IsInstanceOf<string>(Homework02.TimeElapsed(number));

        #endregion

        #region FilterDigit

        private static IEnumerable<TestCaseData> DataCasesForFilterDigit
        {
            get
            {
                yield return new TestCaseData(
                    new List<int> { 7, 70, 17 },
                    new List<int> { 7, 1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 17 },
                    7);
                yield return new TestCaseData(
                    new List<int> { -46, -84, 44, 4, 46, 48 },
                    new List<int> { -5, -46, -84, 1, 44, 2, 3, 4, 5, 46, 7, 48, 68, 69, 70, 4, 17 },
                    4);
                yield return new TestCaseData(
                    new List<int> { 0, -40, 10, 2000, 304, 7028, -304 },
                    new List<int> { -5, 0, -40, -84, 10, 44, 2000, 304, 4, 5, 46, 7, 0, 68, 69, 7028, -304, 4, 17 },
                    0);
            }
        }

        [TestCaseSource(nameof(DataCasesForFilterDigit))]
        public void FilterDigitTests(List<int> expected, List<int> data, int digit)
        {
            Homework02.FilterDigit(ref data, digit);
            Assert.AreEqual(expected, data);
        }

        [Test]
        public void FilterDigitTests_NullList_ThrowsArgumentNullException()
        {
            List<int> data = null;
            int digit = 7;

            Assert.Throws<ArgumentNullException>(() => Homework02.FilterDigit(ref data, digit));
        }

        [Test]
        public void FilterDigitTests_EmptyList_ThrowsArgumentException()
        {
            List<int> data = new List<int>();
            int digit = 7;

            Assert.Throws<ArgumentException>(() => Homework02.FilterDigit(ref data, digit));
        }

        [TestCase(11)]
        [TestCase(-1)]
        public void FilterDigitTests_WrongDigit_ThrowsArgumentOutOfRangeException(int digit)
        {
            var data = new List<int> { 7, 1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 17 };

            Assert.Throws<ArgumentOutOfRangeException>(() => Homework02.FilterDigit(ref data, digit));
        }

        #endregion

        #region FindNthRoot

        [TestCase(1, 5, 0.0001, ExpectedResult = 1)]
        [TestCase(8, 3, 0.0001, ExpectedResult = 2)]
        [TestCase(0.001, 3, 0.0001, ExpectedResult = 0.1)]
        [TestCase(-0.001, 3, 0.0001, ExpectedResult = -0.1)]
        [TestCase(0.04100625, 4, 0.0001, ExpectedResult = 0.45)]
        [TestCase(0.0279936, 7, 0.0001, ExpectedResult = 0.6)]
        [TestCase(-0.0279936, 7, 0.0001, ExpectedResult = -0.6)]
        [TestCase(0.0081, 4, 0.1, ExpectedResult = 0.3)]
        [TestCase(-0.008, 3, 0.1, ExpectedResult = -0.2)]
        [TestCase(0.004241979, 9, 0.00000001, ExpectedResult = 0.545)]
        public double FindNthRootTests(double number, int degree, double precision)
        {
            return Homework02.FindNthRoot(number, degree, precision);
        }

        [TestCase(8, 15, -7)]
        [TestCase(8, 15, -0.6)]
        [TestCase(0.004241979, 9, 0)]
        [TestCase(0.004241979, 9, 0.0000000000000001)]
        [TestCase(0.004241979, -1, 0.0001)]
        [TestCase(0.004241979, 9, 1)]
        [TestCase(0.004241979, 9, 1.3)]
        [TestCase(0.004241979, 9, -0.0001)]
        [TestCase(double.NaN, 9, 0.0001)]
        [TestCase(double.PositiveInfinity, 9, 0.0001)]
        [TestCase(0.004241979, 9, double.NaN)]
        [TestCase(0.004241979, 9, double.NegativeInfinity)]
        public void FindNthRootTests_ThrowsOutOfRangeExceptions(double number, int degree, double precision)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Homework02.FindNthRoot(number, degree, precision));
        }

        #endregion
    }
}
