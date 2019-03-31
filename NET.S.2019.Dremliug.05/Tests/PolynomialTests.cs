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
        #region Exceptions Tests
        [Test]
        public void Throws_ArgExc() =>
    Assert.Throws<ArgumentException>(() => new Polynomial());

        [Test]
        public void Throws_ArgNullExc() =>
            Assert.Throws<ArgumentNullException>(() => new Polynomial(null)); 
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

        [TestCase(new double[] { 1d, 2d, 3d }, ExpectedResult = "3*x^2 + 2*x + 1")]
        [TestCase(new double[] { 0d, 2d, 3d }, ExpectedResult = "3*x^2 + 2*x")]
        //[TestCase(new double[] { 1d, 2d }, ExpectedResult = "2*x + 1")]
        [TestCase(new double[] { 0.0, -1, -1.0, -56.0, 345.223 }, ExpectedResult = "345.223*x^4 - 56*x^3 - 1*x^2 - 1*x")]
        public string ToStringTest(double[] arr)
        {
            Polynomial polynomial = new Polynomial(arr);

            return polynomial.ToString();
        }
    }
}
