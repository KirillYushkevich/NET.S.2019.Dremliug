using System;
using NUnit.Framework;

namespace NET.S._2019.Dremliug._04
{
    [TestFixture]
    public class DoubleExtensionsTests
    {
        [TestCase(-255.255)]
        [TestCase(255.255)]
        [TestCase(4294967295.0)]
        [TestCase(double.MinValue)]
        [TestCase(double.MaxValue)]
        [TestCase(double.Epsilon)]
        [TestCase(double.NaN)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(-0.0)]
        [TestCase(0.0)]
        public void ToBinaryStringTests(double number)
        {
            string expected = Convert.ToString(BitConverter.DoubleToInt64Bits(number), 2).PadLeft(64, '0');

            string actual = number.ToBinaryString();

            Assert.AreEqual(expected, actual);
        }

        [TestCase(-255.255, ExpectedResult = "1100000001101111111010000010100011110101110000101000111101011100")]
        [TestCase(255.255, ExpectedResult = "0100000001101111111010000010100011110101110000101000111101011100")]
        [TestCase(4294967295.0, ExpectedResult = "0100000111101111111111111111111111111111111000000000000000000000")]
        [TestCase(double.MinValue, ExpectedResult = "1111111111101111111111111111111111111111111111111111111111111111")]
        [TestCase(double.MaxValue, ExpectedResult = "0111111111101111111111111111111111111111111111111111111111111111")]
        [TestCase(double.Epsilon, ExpectedResult = "0000000000000000000000000000000000000000000000000000000000000001")]
        [TestCase(double.NaN, ExpectedResult = "1111111111111000000000000000000000000000000000000000000000000000")]
        [TestCase(double.NegativeInfinity, ExpectedResult = "1111111111110000000000000000000000000000000000000000000000000000")]
        [TestCase(double.PositiveInfinity, ExpectedResult = "0111111111110000000000000000000000000000000000000000000000000000")]
        [TestCase(-0.0, ExpectedResult = "1000000000000000000000000000000000000000000000000000000000000000")]
        [TestCase(0.0, ExpectedResult = "0000000000000000000000000000000000000000000000000000000000000000")]
        public string ToBinaryStringTests_HardcodedResults(double number)
            => number.ToBinaryString();
    }
}
