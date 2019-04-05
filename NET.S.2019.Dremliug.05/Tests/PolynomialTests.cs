using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NET.S._2019.Dremliug._05.Tests
{
    [TestFixture]
    class PolynomialTests
    {
        #region Ctor Tests
        [Test]
        public void CtorThrows_ArgExc() =>
            Assert.Throws<ArgumentException>(() => new Polynomial());

        [Test]
        public void CtorThrows_ArgNullExc() =>
            Assert.Throws<ArgumentNullException>(() => new Polynomial(null));

        [Test]
        public void CtorSuccess()
        {
            Polynomial actual = new Polynomial(14.0, 16.3, -4.8, 0.0);
            double[] expected = { 14.0, 16.3, -4.8 };

            Assert.IsTrue(actual.ToArray().SequenceEqual(expected));
        }
        #endregion

        #region Indexer Tests
        // Set indexer is private currently.

        //[TestCase(3)]
        //[TestCase(-1)]
        //public void IndexerSetThrows_IndexOutOfRangeExc(int index)
        //{
        //    Polynomial p = new Polynomial(14.0, 16.3, -4.8);

        //    Assert.Throws<IndexOutOfRangeException>(() => p[index] = 19.7);
        //}

        //[Test]
        //public void IndexerSetSuccess()
        //{
        //    Polynomial actual = new Polynomial(14.0, 16.3, -4.8);
        //    Polynomial expected = new Polynomial(2, 3, -4);

        //    actual[0] = 2;
        //    actual[1] = 3;
        //    actual[2] = -4;

        //    Assert.IsTrue(actual.ToArray().SequenceEqual(expected.ToArray()));
        //}

        [TestCase(3)]
        [TestCase(-1)]
        public void IndexerGetThrows_IndexOutOfRangeExc(int index)
        {
            Polynomial p = new Polynomial(14.0, 16.3, -4.8);
            double x;

            Assert.Throws<IndexOutOfRangeException>(() => x = p[index]);
        }

        [TestCase(0, ExpectedResult = 14.0)]
        [TestCase(1, ExpectedResult = 16.3)]
        [TestCase(2, ExpectedResult = -4.8)]
        public double IndexerGetSuccess(int index)
        {
            Polynomial p = new Polynomial(14.0, 16.3, -4.8);

            return p[index];
        }
        #endregion

        #region Equality Tests
        [Test]
        public void EqualsForTheSameObject()
        {
            Polynomial a = new Polynomial(14.0);
            Polynomial b = a;

            Assert.IsTrue(a == b);
            Assert.IsTrue(b == a);
        }

        [Test]
        public void EqualsWithOneNull()
        {
            Polynomial a = new Polynomial(14.0);
            Polynomial b = null;

            Assert.IsFalse(a == b);
            Assert.IsFalse(b == a);
        }

        [Test]
        public void EqualsWithTwoNull()
        {
            Polynomial a = null;
            Polynomial b = null;

            Assert.IsTrue(a == b);
            Assert.IsTrue(b == a);
        }

        [Test]
        public void EqualsWithTwoNonNull()
        {
            Polynomial a = new Polynomial(14.0, -1.2, 8.1);
            Polynomial b = new Polynomial(14.0, -1.2, 8.1);

            Assert.IsTrue(a == b);
            Assert.IsTrue(b == a);
        }

        [TestCase(new double[] { 1d, 2d, 3d }, new double[] { 1d, 2d, 3d }, ExpectedResult = true)]
        [TestCase(new double[] { 1d, 2d, 3d, 0d }, new double[] { 1d, 2d, 3d }, ExpectedResult = true)]
        [TestCase(new double[] { 1d, 2d }, new double[] { 1d, 2d, 3d }, ExpectedResult = false)]
        [TestCase(new double[] { 1d, 2d }, new double[] { 2d, 3d }, ExpectedResult = false)]
        public bool Overrided_Equals_Is_Correct(double[] left, double[] right)
        {
            Polynomial leftP = new Polynomial(left);
            Polynomial rightP = new Polynomial(right);

            return leftP.Equals(rightP);
        }
        #endregion

        #region HashCode Tests
        [Test]
        public void GetHashCodeTest()
        {
            Polynomial a = new Polynomial(new double[] { 14.1 });
            Polynomial b = new Polynomial(new double[] { 14.1 });
            Polynomial c = a;

            HashSet<Polynomial> hs = new HashSet<Polynomial>(new Polynomial[] { a, b, c });

            Assert.AreEqual(1, hs.Count);
        }
        #endregion

        #region Operators
        [TestCase(new double[] { 1d, 2d }, new double[] { 1d, -3d, 8d }, new double[] { 2d, -1d, 8d })]
        [TestCase(new double[] { 15.5, -27.1, 0.0, 0.0, 345.223 }, new double[] { 35, 0.0, 1, 56 }, new double[] { 50.5, -27.1, 1.0, 56, 345.223 })]
        public void PlusOperator(double[] left, double[] right, double[] expectedResult)
        {
            Polynomial leftP = new Polynomial(left);
            Polynomial rightP = new Polynomial(right);
            Polynomial expected = new Polynomial(expectedResult);

            Polynomial actual = leftP + rightP;

            Assert.IsTrue(actual.ToArray().SequenceEqual(expected.ToArray()));
        }

        [TestCase(new double[] { 1d, 2d }, new double[] { 1d, 2d, 3d }, new double[] { 0d, 0d, -3d })]
        [TestCase(
            new double[] { 15.5, -27.1, 0.0, 0.0, 345.223 },
            new double[] { 35, 0.0, 1, 56 },
            new double[] { -19.5, -27.1, -1.0, -56.0, 345.223 })]
        public void MinusOperator(double[] left, double[] right, double[] expectedResult)
        {
            Polynomial leftP = new Polynomial(left);
            Polynomial rightP = new Polynomial(right);
            Polynomial expected = new Polynomial(expectedResult);

            Polynomial actual = leftP - rightP;

            Assert.IsTrue(actual.ToArray().SequenceEqual(expected.ToArray()));
        }

        [TestCase(new double[] { 1d, 2d }, new double[] { 1d, 2d, 3d }, new double[] { 1d, 4d, 7d, 6d })]
        [TestCase(
            new double[] { 15.5, -27.1, 0.0, 0.0, 345.223 },
            new double[] { 35, 0.0, 1, 56 },
            new double[] { 542.5, -948.5, 15.5, 840.9, 10565.205, 0.0, 345.223, 19332.488 })]
        public void MulOperator(double[] left, double[] right, double[] expectedResult)
        {
            Polynomial leftP = new Polynomial(left);
            Polynomial rightP = new Polynomial(right);
            Polynomial expected = new Polynomial(expectedResult);

            Polynomial actual = leftP * rightP;

            Assert.IsTrue(actual.ToArray().SequenceEqual(expected.ToArray()));
        }
        #endregion

        #region ToString
        [TestCase(new double[] { 1d, 2d, 3d }, ExpectedResult = "3x^2 + 2x + 1")]
        [TestCase(new double[] { 0d, 2d, 3d }, ExpectedResult = "3x^2 + 2x")]
        [TestCase(new double[] { 1d, 2d }, ExpectedResult = "2x + 1")]
        [TestCase(new double[] { 0.0, -1, -1.0, -56.0, 345.223 }, ExpectedResult = "345.223x^4 - 56x^3 - 1x^2 - 1x")]
        public string ToStringTest(double[] arr)
        {
            Polynomial polynomial = new Polynomial(arr);

            return polynomial.ToString();
        } 
        #endregion
    }
}
