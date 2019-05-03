using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NET.S._2019.Dremliug._11
{
    [TestFixture]
    public class BinarySearchGenericTests
    {
        [TestCase(new[] { -1, 0, 1, 2, 3, 4, 5, 6, 7, 8 }, 3, ExpectedResult = 4)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 15, ExpectedResult = -11)]
        public int BinarySearchTest_Int(IList<int> list, int value) => list.Search(value);

        [TestCase(new[] { 1d, 2d, 4d, 7d, 9d, 13d }, 13d, ExpectedResult = 5)]
        public int BinarySearchTests_Double(IList<double> list, double value) => list.Search(value);

        [TestCase(new[] { 'a', 'd', 'f', 'm', 'u', 'z' }, 'f', ExpectedResult = 2)]
        public int BinarySearchTest_Char(IList<char> list, char value) => list.Search(value);

        [TestCase(new[] { "av", "baz", "bazz", "call", "no", "rts", "uu" }, "rts", ExpectedResult = 5)]
        [TestCase(new[] { "av", "baz", "bazz", "call", "no", "rts", "uu" }, "ns", ExpectedResult = -6)]
        public int BinarySearchTest_String(IList<string> list, string value) => list.Search(value, StringComparer.InvariantCulture);

        [TestCase(null, 3)]
        [TestCase(new object[] { 2, 3 }, null)]
        public void BinarySearchTest_ThrowsArgNullException(IList<object> list, object value) => Assert.Throws<ArgumentNullException>(() => list.Search(value));

        [TestCase(new object[] { 2, 3 }, (object)2)]
        public void BinarySearchTest_ThrowsInvalidOPException(IList<object> list, object value) => Assert.Throws<InvalidOperationException>(() => list.Search(value));
    }
}
