using System;
using System.Collections.Generic;
using System.Linq;
using BinarySearchTreeGeneric;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class BinarySearchTreeGTests
    {
        #region Int Data
        private int[] intData = { 25, 15, 10, 4, 12, 22, 18, 24, 50, 35, 31, 44, 70, 66, 90, };

        private int[] expectedIntPreorder = { 25, 15, 10, 4, 12, 22, 18, 24, 50, 35, 31, 44, 70, 66, 90, };
        private int[] expectedIntInorder = { 4, 10, 12, 15, 18, 22, 24, 25, 31, 35, 44, 50, 66, 70, 90, };
        private int[] expectedIntPostorder = { 4, 12, 10, 18, 24, 22, 15, 31, 44, 35, 66, 90, 70, 50, 25, };

        private int[] expectedIntPreorderC = { 25, 50, 70, 90, 66, 35, 44, 31, 15, 22, 24, 18, 10, 12, 4, };
        private int[] expectedIntInorderC = { 90, 70, 66, 50, 44, 35, 31, 25, 24, 22, 18, 15, 12, 10, 4, };
        private int[] expectedIntPostorderC = { 90, 66, 70, 44, 31, 35, 50, 24, 18, 22, 12, 4, 10, 15, 25, };
        #endregion

        #region String Data
        private string[] stringData = { "dd", "aa", "ee", "bb", "cc", "gg", "ff", };

        private string[] expectedStringPreorder = { "dd", "aa", "bb", "cc", "ee", "gg", "ff", };
        private string[] expectedStringInorder = { "aa", "bb", "cc", "dd", "ee", "ff", "gg", };
        private string[] expectedStringPostorder = { "cc", "bb", "aa", "ff", "gg", "ee", "dd", };

        private string[] expectedStringPreorderC = { "dd", "ee", "gg", "ff", "aa", "bb", "cc", };
        private string[] expectedStringInorderC = { "gg", "ff", "ee", "dd", "cc", "bb", "aa", };
        private string[] expectedStringPostorderC = { "ff", "gg", "ee", "cc", "bb", "aa", "dd", };
        #endregion

        #region Book Data
        private Book[] bookData = { new Book("A-dd", "T-dd"), new Book("A-aa", "T-aa"), new Book("A-ee", "T-ee"), new Book("A-bb", "T-bb"), new Book("A-cc", "T-cc"), new Book("A-gg", "T-gg"), };

        private Book[] expectedBookPreorder = { new Book("A-dd", "T-dd"), new Book("A-aa", "T-aa"), new Book("A-bb", "T-bb"), new Book("A-cc", "T-cc"), new Book("A-ee", "T-ee"), new Book("A-gg", "T-gg"), };
        private Book[] expectedBookInorder = { new Book("A-aa", "T-aa"), new Book("A-bb", "T-bb"), new Book("A-cc", "T-cc"), new Book("A-dd", "T-dd"), new Book("A-ee", "T-ee"), new Book("A-gg", "T-gg"), };
        private Book[] expectedBookPostorder = { new Book("A-cc", "T-cc"), new Book("A-bb", "T-bb"), new Book("A-aa", "T-aa"), new Book("A-gg", "T-gg"), new Book("A-ee", "T-ee"), new Book("A-dd", "T-dd"), };

        private Book[] expectedBookPreorderC = { new Book("A-dd", "T-dd"), new Book("A-ee", "T-ee"), new Book("A-gg", "T-gg"), new Book("A-aa", "T-aa"), new Book("A-bb", "T-bb"), new Book("A-cc", "T-cc"), };
        private Book[] expectedBookInorderC = { new Book("A-gg", "T-gg"), new Book("A-ee", "T-ee"), new Book("A-dd", "T-dd"), new Book("A-cc", "T-cc"), new Book("A-bb", "T-bb"), new Book("A-aa", "T-aa"), };
        private Book[] expectedBookPostorderC = { new Book("A-gg", "T-gg"), new Book("A-ee", "T-ee"), new Book("A-cc", "T-cc"), new Book("A-bb", "T-bb"), new Book("A-aa", "T-aa"), new Book("A-dd", "T-dd"), };
        #endregion

        #region Point Data
        private Point[] pointData = { new Point(2d, 5d), new Point(1d, 5d), new Point(1d, 0d), new Point(5d, 0d), new Point(3d, 1d), new Point(6d, 3d), new Point(4d, 7d), };

        private Point[] expectedPointPreorder = { new Point(2d, 5d), new Point(1d, 5d), new Point(1d, 0d), new Point(5d, 0d), new Point(3d, 1d), new Point(4d, 7d), new Point(6d, 3d), };
        private Point[] expectedPointInorder = { new Point(1d, 0d), new Point(1d, 5d), new Point(2d, 5d), new Point(3d, 1d), new Point(4d, 7d), new Point(5d, 0d), new Point(6d, 3d), };
        private Point[] expectedPointPostorder = { new Point(1d, 0d), new Point(1d, 5d), new Point(4d, 7d), new Point(3d, 1d), new Point(6d, 3d), new Point(5d, 0d), new Point(2d, 5d), };

        private Point[] expectedPointPreorderC = { new Point(2d, 5d), new Point(5d, 0d), new Point(6d, 3d), new Point(3d, 1d), new Point(4d, 7d), new Point(1d, 5d), new Point(1d, 0d), };
        private Point[] expectedPointInorderC = { new Point(6d, 3d), new Point(5d, 0d), new Point(4d, 7d), new Point(3d, 1d), new Point(2d, 5d), new Point(1d, 5d), new Point(1d, 0d), };
        private Point[] expectedPointPostorderC = { new Point(6d, 3d), new Point(4d, 7d), new Point(3d, 1d), new Point(5d, 0d), new Point(1d, 0d), new Point(1d, 5d), new Point(2d, 5d), };
        #endregion

        #region Int Tests
        [Test]
        public void IntOrderTest_DefaultComparer()
        {
            BinarySearchTreeG<int> tree = new BinarySearchTreeG<int>(intData);

            Check(expectedIntPreorder, tree.WalkPreorder(), "Int PREorder, default comparer.");
            Check(expectedIntInorder, tree.WalkInorder(), "Int INorder, default comparer.");
            Check(expectedIntPostorder, tree.WalkPostorder(), "Int POSTorder, default comparer.");
        }

        [Test]
        public void IntOrderTest_CustomComparer()
        {
            var comparer = Comparer<int>.Create((a, b) => b.CompareTo(a)); // Descending.
            BinarySearchTreeG<int> tree = new BinarySearchTreeG<int>(intData, comparer);

            Check(expectedIntPreorderC, tree.WalkPreorder(), "Int PREorder, custom comparer.");
            Check(expectedIntInorderC, tree.WalkInorder(), "Int INorder, custom comparer.");
            Check(expectedIntPostorderC, tree.WalkPostorder(), "Int POSTorder, custom comparer.");
        }
        #endregion

        #region String Tests
        [Test]
        public void StringOrderTest_DefaultComparer()
        {
            BinarySearchTreeG<string> tree = new BinarySearchTreeG<string>(stringData);

            Check(expectedStringPreorder, tree.WalkPreorder(), "String PREorder, default comparer.");
            Check(expectedStringInorder, tree.WalkInorder(), "String INorder, default comparer.");
            Check(expectedStringPostorder, tree.WalkPostorder(), "String POSTorder, default comparer.");
        }

        [Test]
        public void StringOrderTest_CustomComparer()
        {
            var comparer = Comparer<string>.Create((a, b) => b.CompareTo(a)); // Descending.
            BinarySearchTreeG<string> tree = new BinarySearchTreeG<string>(stringData, comparer);

            Check(expectedStringPreorderC, tree.WalkPreorder(), "String PREorder, custom comparer.");
            Check(expectedStringInorderC, tree.WalkInorder(), "String INorder, custom comparer.");
            Check(expectedStringPostorderC, tree.WalkPostorder(), "String POSTorder, custom comparer.");
        }
        #endregion

        #region Book Tests
        [Test]
        public void BookOrderTest_DefaultComparer()
        {
            BinarySearchTreeG<Book> tree = new BinarySearchTreeG<Book>(bookData);

            Check(expectedBookPreorder, tree.WalkPreorder(), "Book PREorder, default comparer.");
            Check(expectedBookInorder, tree.WalkInorder(), "Book INorder, default comparer.");
            Check(expectedBookPostorder, tree.WalkPostorder(), "Book POSTorder, default comparer.");
        }

        [Test]
        public void BookOrderTest_CustomComparer()
        {
            var comparer = Comparer<Book>.Create((a, b) => b.CompareTo(a)); // Descending.
            BinarySearchTreeG<Book> tree = new BinarySearchTreeG<Book>(bookData, comparer);

            Check(expectedBookPreorderC, tree.WalkPreorder(), "Book PREorder, custom comparer.");
            Check(expectedBookInorderC, tree.WalkInorder(), "Book INorder, custom comparer.");
            Check(expectedBookPostorderC, tree.WalkPostorder(), "Book POSTorder, custom comparer.");
        }
        #endregion

        #region Point Tests
        [Test]
        public void PointOrderTest_CustomComparer_A()
        {
            // Ascending.
            var comparer = Comparer<Point>.Create((a, b) => (a.X, a.Y).CompareTo((b.X, b.Y)));
            BinarySearchTreeG<Point> tree = new BinarySearchTreeG<Point>(pointData, comparer);

            Check(expectedPointPreorder, tree.WalkPreorder(), "Point PREorder, default comparer.");
            Check(expectedPointInorder, tree.WalkInorder(), "Point INorder, default comparer.");
            Check(expectedPointPostorder, tree.WalkPostorder(), "Point POSTorder, default comparer.");
        }

        [Test]
        public void PointOrderTest_CustomComparer_B()
        {
            // Descending.
            var comparer = Comparer<Point>.Create((a, b) => (b.X, b.Y).CompareTo((a.X, a.Y)));
            BinarySearchTreeG<Point> tree = new BinarySearchTreeG<Point>(pointData, comparer);

            Check(expectedPointPreorderC, tree.WalkPreorder(), "Point PREorder, custom comparer.");
            Check(expectedPointInorderC, tree.WalkInorder(), "Point INorder, custom comparer.");
            Check(expectedPointPostorderC, tree.WalkPostorder(), "Point POSTorder, custom comparer.");
        }
        #endregion

        #region Exceptions Tests
        [Test]
        public void ThrowsInvOPExc_NoComparer_IfNoDefault()
        {
            Assert.Throws<InvalidOperationException>(() => new BinarySearchTreeG<Point>(pointData));
        }

        [Test]
        public void ThrowsArgNullExc_NullRefCollection()
        {
            int[] collection = null;

            Assert.Throws<ArgumentNullException>(() => new BinarySearchTreeG<int>(collection));
        }
        #endregion

        #region Check helper method
        private void Check<T>(T[] expected, IEnumerable<Node<T>> actual, string message)
        {
            Assert.AreEqual(expected, actual.Select(x => x.Value).ToArray(), message);
        }
        #endregion

        #region Point struct
        /// <summary>
        /// Simple struct for test purposes.
        /// </summary>
        public struct Point : IEquatable<Point>
        {
            public Point(double x, double y)
            {
                this.X = x;
                this.Y = y;
            }

            public double X { get; }

            public double Y { get; }

            public static bool operator ==(Point point1, Point point2) => EqualityComparer<Point>.Default.Equals(point1, point2);

            public static bool operator !=(Point point1, Point point2) => !(point1 == point2);

            public override bool Equals(object obj) => this.Equals(obj);

            public bool Equals(Point other)
                => other != null && this.GetType() == other.GetType() &&
                   (this.X, this.Y) == (other.X, other.Y);

            public override int GetHashCode() => (this.X, this.Y).GetHashCode();
        }
        #endregion

        #region Book class
        /// <summary>
        /// Simple class for test purposes.
        /// </summary>
        public sealed class Book : IComparable<Book>, IEquatable<Book>
        {
            public Book(string author, string title)
            {
                this.Author = author;
                this.Title = title;
            }

            public string Author { get; }

            public string Title { get; }

            public static bool operator ==(Book book1, Book book2) => EqualityComparer<Book>.Default.Equals(book1, book2);

            public static bool operator !=(Book book1, Book book2) => !(book1 == book2);

            public int CompareTo(Book other)
                => (this.Author, this.Title).CompareTo((other.Author, other.Title));

            public override bool Equals(object obj) => this.Equals(obj as Book);

            public bool Equals(Book other)
                => other != null && this.GetType() == other.GetType() &&
                   (this.Author, this.Title) == (other.Author, other.Title);

            public override int GetHashCode() => (this.Author, this.Title).GetHashCode();
        }
        #endregion
    }
}
