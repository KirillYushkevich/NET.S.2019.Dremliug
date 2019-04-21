using System;
using System.Linq;
using NUnit.Framework;
using QueueGeneric;

namespace Tests
{
    [TestFixture]
    public class QueueGTests
    {
        [Test]
        public void QueueG_ToArrayTests()
        {
            var queueG = new QueueG<int>(new[] { 1, 2, 3, 4 });

            Assert.AreEqual(new[] { 1, 2, 3, 4 }, queueG.ToArray());
        }

        [Test]
        public void QueueG_ContainsTests()
        {
            var queueG = new QueueG<int>(new[] { 1, 2, 3, 4 });

            Assert.AreEqual(true, queueG.Contains(2));
            Assert.AreEqual(false, queueG.Contains(6));
        }

        [Test]
        public void QueueG_ClearTests()
        {
            var queueG = new QueueG<int>(new[] { 1, 2, 3, 4 });
            queueG.Clear();

            Assert.AreEqual(new int[] { }, queueG.ToArray());
        }

        [Test]
        public void QueueG_EnqueueTests()
        {
            var queueG = new QueueG<int>();
            queueG.Enqueue(1);
            queueG.Enqueue(2);
            queueG.Enqueue(3);
            queueG.Enqueue(4);

            Assert.AreEqual(new[] { 1, 2, 3, 4 }, queueG.ToArray());
        }

        [Test]
        public void QueueG_DequeueTests()
        {
            var queueG = new QueueG<int>(new[] { 1, 2, 3, 4, 5, 6, 7 });
            queueG.Dequeue();
            queueG.Dequeue();
            queueG.Dequeue();
            queueG.Dequeue();

            Assert.AreEqual(new[] { 5, 6, 7 }, queueG.ToArray());
        }

        [Test]
        public void QueueG_PeekTests()
        {
            var queueG = new QueueG<int>(new[] { 1, 2, 3, 4 });

            Assert.AreEqual(1, queueG.Peek());
        }

        [Test]
        public void QueueG_EnumeratorForEachTests()
        {
            var queueG = new QueueG<int>(Enumerable.Range(1, 100));
            int i = 1;
            foreach (var element in queueG)
            {
                Assert.AreEqual(i, element);
                i++;
            }
        }

        [Test]
        public void QueueG_EnumeratorMoveNextTests()
        {
            var queueG = new QueueG<int>(Enumerable.Range(1, 100));
            int i = 1;

            using (var enumerator = queueG.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Assert.AreEqual(i, enumerator.Current);
                    i++;
                }
            }
        }

        [Test]
        public void QueueG_ThrowsArgNullExcOnBadCtorParam()
        {
            Assert.Throws<ArgumentNullException>(() => new QueueG<int>(null));
        }
    }
}
