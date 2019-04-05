using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NET.S._2019.Dremliug._02.Tests
{
    [TestClass]
    public class Homework02MSbuildTests
    {
        #region InsertNumber

        [TestMethod]
        [DataRow(15, 15, 0, 0, 15)]
        [DataRow(8, 15, 0, 0, 9)]
        [DataRow(8, 15, 3, 8, 120)]
        [DataRow(-819436951, 1839623026, 7, 26, -885671575)]
        public void InsertNumberTest(int target, int source, int startOfBitRange, int endOfBitRange, int expected)
            => Assert.AreEqual(expected, Homework02.InsertNumber(target, source, startOfBitRange, endOfBitRange));

        [TestMethod]
        [DataRow(8, 15, 8, 3)]
        [DataRow(8, 15, -1, 8)]
        [DataRow(8, 15, 3, 32)]
        [DataRow(8, 15, 8, 3)]
        public void InsertNumberTests_WrongBitPositions_ThrowsArgumentOutOfRangeException(int target, int source, int startOfBitRange, int endOfBitRange)
            => Assert.ThrowsException<ArgumentOutOfRangeException>(() => Homework02.InsertNumber(target, source, startOfBitRange, endOfBitRange));

        #endregion
    }
}
