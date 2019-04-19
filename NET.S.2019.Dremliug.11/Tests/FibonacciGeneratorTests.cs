using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NET.S._2019.Dremliug._11
{
    [TestFixture]
    public class FibonacciTests
    {
        [TestCase(new[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 })]
        public void FibonacciTest(int[] expectedSequence)
        {
            CollectionAssert.AreEqual(expectedSequence, Fibonacci.Sequence().Take(expectedSequence.Length));
        }
    }
}
