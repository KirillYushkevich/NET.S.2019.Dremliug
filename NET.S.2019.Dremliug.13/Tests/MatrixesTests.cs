using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrixes;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MatrixesTests
    {
        #region Int + String creation empty
        [Test]
        public void EmptyInt_MatixCreationTest()
        {
            int[,] expectedEmpty = new int[0, 0];

            var emptySquare = new SquareMatrix<int>();
            EveryElementAssert(expectedEmpty, emptySquare, "Empty int Square creation");

            var emptySymm = new SymmMatrix<int>();
            EveryElementAssert(expectedEmpty, emptySymm, "Empty int Symm creation");

            var emptyDiag = new DiagMatrix<int>();
            EveryElementAssert(expectedEmpty, emptyDiag, "Empty int Diag creation");
        }

        [Test]
        public void EmptyString_MatixCreationTest()
        {
            string[,] expectedEmpty = new string[0, 0];

            var emptySquare = new SquareMatrix<string>();
            EveryElementAssert(expectedEmpty, emptySquare, "Empty string Square creation");

            var emptySymm = new SymmMatrix<string>();
            EveryElementAssert(expectedEmpty, emptySymm, "Empty string Symm creation");

            var emptyDiag = new DiagMatrix<string>();
            EveryElementAssert(expectedEmpty, emptyDiag, "Empty string Diag creation");
        }
        #endregion

        #region Int + String creation of size
        [Test]
        public void GivenSizeInt_MatixCreationTest()
        {
            int[,] expectedDefaultValues =
            {
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            };

            var sizedSquare = new SquareMatrix<int>(5);
            EveryElementAssert(expectedDefaultValues, sizedSquare, "Sized int Square default values creation");

            var sizedSymm = new SymmMatrix<int>(5);
            EveryElementAssert(expectedDefaultValues, sizedSymm, "Sized int Symm default values creation");

            var sizedDiag = new DiagMatrix<int>(5);
            EveryElementAssert(expectedDefaultValues, sizedDiag, "Sized int Diag default values creation");
        }

        [Test]
        public void GivenSizeString_MatixCreationTest()
        {
            string[,] expectedDefaultValues =
            {
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            };

            var sizedSquare = new SquareMatrix<string>(5);
            EveryElementAssert(expectedDefaultValues, sizedSquare, "Sized string Square default values creation");

            var sizedSymm = new SymmMatrix<string>(5);
            EveryElementAssert(expectedDefaultValues, sizedSymm, "Sized string Symm default values creation");

            var sizedDiag = new DiagMatrix<string>(5);
            EveryElementAssert(expectedDefaultValues, sizedDiag, "Sized string Diag default values creation");
        }
        #endregion

        #region Int Creation from non-square
        [Test]
        public void FromNonSquareIntArray_MatixCreationTest()
        {
            int[,] nonSquareInput =
            {
            { 1, 2, 3, 4 },
            { 1, 2, 3, 4 },
            { 1, 2, 3, 4 },
            { 1, 2, 3, 4 },
            { 1, 2, 3, 4 },
            };

            int[,] expectedSquare =
            {
            { 1, 2, 3, 4 },
            { 1, 2, 3, 4 },
            { 1, 2, 3, 4 },
            { 1, 2, 3, 4 },
            };

            int[,] expectedTopSymm =
            {
            { 1, 2, 3, 4 },
            { 2, 2, 3, 4 },
            { 3, 3, 3, 4 },
            { 4, 4, 4, 4 },
            };

            int[,] expectedBottomSymm =
            {
            { 1, 1, 1, 1 },
            { 1, 2, 2, 2 },
            { 1, 2, 3, 3 },
            { 1, 2, 3, 4 },
            };

            int[,] expectedDiag =
            {
            { 1, 0, 0, 0 },
            { 0, 2, 0, 0 },
            { 0, 0, 3, 0 },
            { 0, 0, 0, 4 },
            };

            var actualSquare = new SquareMatrix<int>(nonSquareInput);
            EveryElementAssert(expectedSquare, actualSquare, "From int non-square to Square creation");

            var actualTopSymm = new SymmMatrix<int>(nonSquareInput, true);
            EveryElementAssert(expectedTopSymm, actualTopSymm, "From int non-square to TopSymm creation");

            var actualBottomSymm = new SymmMatrix<int>(nonSquareInput, false);
            EveryElementAssert(expectedBottomSymm, actualBottomSymm, "From int non-square to BottomSymm creation");

            var actualDiag = new DiagMatrix<int>(nonSquareInput);
            EveryElementAssert(expectedDiag, actualDiag, "From int non-square to Diag creation");
        }
        #endregion

        #region String Creation from non-square
        [Test]
        public void FromNonSquareStringArray_MatixCreationTest()
        {
            string[,] nonSquareInput =
            {
            { "1", null, "3", "4" },
            { "1", null, "3", "4" },
            { "1", null, "3", "4" },
            { "1", null, "3", "4" },
            { "1", null, "3", "4" },
            };

            string[,] expectedSquare =
            {
            { "1", null, "3", "4" },
            { "1", null, "3", "4" },
            { "1", null, "3", "4" },
            { "1", null, "3", "4" },
            };

            string[,] expectedTopSymm =
            {
            { "1", null, "3", "4" },
            { null, null, "3", "4" },
            { "3", "3", "3", "4" },
            { "4", "4", "4", "4" },
            };

            string[,] expectedBottomSymm =
            {
            { "1", "1", "1", "1" },
            { "1", null, null, null },
            { "1", null, "3", "3" },
            { "1", null, "3", "4" },
            };

            string[,] expectedDiag =
            {
            { "1", null, null, null },
            { null, null, null, null },
            { null, null, "3", null },
            { null, null, null, "4" },
            };

            var actualSquare = new SquareMatrix<string>(nonSquareInput);
            EveryElementAssert(expectedSquare, actualSquare, "From string non-square to Square creation");

            var actualTopSymm = new SymmMatrix<string>(nonSquareInput, true);
            EveryElementAssert(expectedTopSymm, actualTopSymm, "From string non-square to TopSymm creation");

            var actualBottomSymm = new SymmMatrix<string>(nonSquareInput, false);
            EveryElementAssert(expectedBottomSymm, actualBottomSymm, "From string non-square to BottomSymm creation");

            var actualDiag = new DiagMatrix<string>(nonSquareInput);
            EveryElementAssert(expectedDiag, actualDiag, "From string non-square to Diag creation");
        }
        #endregion

        #region Int Sum
        [Test]
        public void Int_MatixSumTest()
        {
            int[,] inputArrayLeft =
            {
            { 1, 2, 3, 4, 5 },
            { 1, 2, 3, 4, 5 },
            { 1, 2, 3, 4, 5 },
            { 1, 2, 3, 4, 5 },
            { 1, 2, 3, 4, 5 },
            };

            int[,] inputArrayRight =
            {
            { 1, 2, 3, 2, 1 },
            { 5, 2, 5, 5, 4 },
            { 1, 2, 3, 2, 1 },
            { 1, 3, 3, 3, 1 },
            { 0, 1, 1, 2, 2 },
            };

            int[,] expectedSquareArray =
            {
            { 2, 4, 6, 6, 6 },
            { 6, 4, 8, 9, 9 },
            { 2, 4, 6, 6, 6 },
            { 2, 5, 6, 7, 6 },
            { 1, 3, 4, 6, 7 },
            };

            int[,] expectedSymmArray =
            {
            { 2, 2, 3, 4, 5 },
            { 2, 4, 3, 4, 5 },
            { 3, 3, 6, 4, 5 },
            { 4, 4, 4, 7, 5 },
            { 5, 5, 5, 5, 7 },
            };

            var actualSquareLeft = new SquareMatrix<int>(inputArrayLeft);
            var actualSquareRight = new SquareMatrix<int>(inputArrayRight);
            var actualSum = MatrixOperator<int>.Sum(actualSquareLeft, actualSquareRight);
            EveryElementAssert(expectedSquareArray, actualSum, "Sum two Square matrixes.");

            // Also check return type generalization.
            var actualSymmLeft = new SymmMatrix<int>(inputArrayLeft, true);
            var actualDiagRight = new DiagMatrix<int>(inputArrayRight);
            var actualSymmSum = MatrixOperator<int>.Sum(actualSymmLeft, actualDiagRight);
            Assert.AreEqual(typeof(SymmMatrix<int>), actualSymmSum.GetType(), $"Return type is not generic, expected: {typeof(SymmMatrix<int>)} but was {actualSymmLeft.GetType()}");
            EveryElementAssert(expectedSymmArray, actualSymmSum, "SymmMatrix + DiagMatrix = SymmMatrix");
        }
        #endregion

        #region String Sum
        [Test]
        public void String_MatixSumTest()
        {
            string[,] inputArrayLeft =
            {
            { "a", null, "c", "d" },
            { "a", null, "c", "d" },
            { "a", null, "c", "d" },
            { "a", null, "c", "d" },
            };

            string[,] inputArrayRight =
            {
            { "a", null, "c", "d" },
            { "a", "b", "c", "d" },
            { "a", null, null, "d" },
            { "a", "b", "c", "d" },
            };

            string em = string.Empty; // Style cop hates "".
            string[,] expectedSquareArray =
            {
            { "aa", em, "cc", "dd" },
            { "aa", "b", "cc", "dd" },
            { "aa", em, "c", "dd" },
            { "aa", "b", "cc", "dd" },
            };

            string[,] expectedSymmArray =
            {
            { "aa", em, "c", "d" },
            { em, "b", "c", "d" },
            { "c", "c", "c", "d" },
            { "d", "d", "d", "dd" },
            };

            var actualSquareLeft = new SquareMatrix<string>(inputArrayLeft);
            var actualSquareRight = new SquareMatrix<string>(inputArrayRight);
            var actualSum = MatrixOperator<string>.Sum(actualSquareLeft, actualSquareRight);
            EveryElementAssert(expectedSquareArray, actualSum, "Sum two Square matrixes.");

            // Also check return type generalization.
            var actualSymmLeft = new SymmMatrix<string>(inputArrayLeft, true);
            var actualDiagRight = new DiagMatrix<string>(inputArrayRight);
            var actualSymmSum = MatrixOperator<string>.Sum(actualSymmLeft, actualDiagRight);
            Assert.AreEqual(typeof(SymmMatrix<string>), actualSymmSum.GetType(), $"Return type is not generic, expected: {typeof(SymmMatrix<string>)} but was {actualSymmLeft.GetType()}");
            EveryElementAssert(expectedSymmArray, actualSymmSum, "SymmMatrix + DiagMatrix = SymmMatrix");
        }
        #endregion

        #region Exceptions Tests
        [Test]
        public void ThrowsArgOutOfRange_NegativeSize()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new SquareMatrix<int>(-1), "Square with negative size.");
            Assert.Throws<ArgumentOutOfRangeException>(() => new SymmMatrix<int>(-1), "Symm with negative size.");
            Assert.Throws<ArgumentOutOfRangeException>(() => new DiagMatrix<int>(-1), "Diag with negative size.");

            Assert.Throws<ArgumentOutOfRangeException>(() => new DiagMatrix<int>(2)[1, 0] = 3, "Attempt to change element outside main Diag.");
        }

        [Test]
        public void ThrowsArgNullExc_NullRefCreation()
        {
            int[,] array = null;
            Assert.Throws<ArgumentNullException>(() => new SquareMatrix<int>(array), "Square with null ref.");
            Assert.Throws<ArgumentNullException>(() => new SymmMatrix<int>(array, true), "Symm with null ref.");
            Assert.Throws<ArgumentNullException>(() => new DiagMatrix<int>(array), "Diag with null ref.");
        }
        #endregion

        #region Event tests
        [Test]
        public void SquareEventTest()
        {
            // This matrix will send event.
            var square = new SquareMatrix<int>(2);

            // This element will be changed.
            int newValue = 5;
            int i = 0;
            int j = 1;

            // This indicates if any event was fired.
            bool eventFired = false;

            // Subscribe to the event.
            square.ElementChanged += eventChecker;

            // Change the element to raise the event.
            square[i, j] = newValue;

            // Warn if there was no event. 
            Assert.IsTrue(eventFired, "No event is registered.");

            // Event listener.
            void eventChecker(object eventSender, SquareMatrix<int>.ElementChangedEventArgs<int> eventArgs)
            {
                // Check the event.
                Assert.IsNotNull(eventSender, "Event sender is registered.");
                Assert.AreSame(square, eventSender, "Event sender is the correct object.");
                Assert.IsNotNull(eventArgs, "Event arguments are provided.");
                Assert.AreEqual(newValue, eventArgs.Value, "Event args contain correct new value.");
                Assert.AreEqual(i, eventArgs.Iindex, "Event args contain correct I index.");
                Assert.AreEqual(j, eventArgs.Jindex, "Event args contain correct J index.");

                eventFired = true;
            }
        }

        [Test]
        public void SymmEventTest()
        {
            // This matrix will send event.
            var symm = new SymmMatrix<int>(2);

            // This element will be changed.
            int newValue = 5;
            int i = 0;
            int j = 1;

            // This indicates if any event was fired.
            int eventsFired = 0;

            // Subscribe to the event.
            symm.ElementChanged += eventChecker;

            // Change the element to raise the event.
            symm[i, j] = newValue;

            // Warn if there was no event. 
            Assert.AreEqual(2, eventsFired, $"{eventsFired} Event(s) registered.");

            // Event listener.
            void eventChecker(object eventSender, SymmMatrix<int>.ElementChangedEventArgs<int> eventArgs)
            {
                // Check the event.
                Assert.IsNotNull(eventSender, "Event sender is registered.");
                Assert.AreSame(symm, eventSender, "Event sender is the correct object.");
                Assert.IsNotNull(eventArgs, "Event arguments are provided.");
                Assert.AreEqual(newValue, eventArgs.Value, "Event args contain correct new value.");
                Assert.That(eventArgs.Iindex == i || eventArgs.Iindex == j, "Event args contain correct I index.");
                Assert.That(eventArgs.Iindex == i || eventArgs.Iindex == j, "Event args contain correct J index.");

                eventsFired++;
            }
        }
        #endregion

        #region private EveryElementAssert method
        private void EveryElementAssert(dynamic expectedMatrix, dynamic actualMatrix, string testTypeMessage)
        {
            Assert.AreEqual(expectedMatrix.GetLength(0), actualMatrix.Size, $"GetLength(0) mismatch: expected {expectedMatrix.GetLength(0)} but was {actualMatrix.Size}");

            Assert.AreEqual(expectedMatrix.GetLength(1), actualMatrix.Size, $"GetLength(1) mismatch: expected {expectedMatrix.GetLength(1)} but was {actualMatrix.Size}");

            for (int i = 0; i < expectedMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < expectedMatrix.GetLength(1); j++)
                {
                    Assert.AreEqual(expectedMatrix[i, j], actualMatrix[i, j], $"{testTypeMessage}, wrong value at [{i}, {j}] expected: {expectedMatrix[i, j] ?? "null"} but was {actualMatrix[i, j] ?? "null"}");
                }
            }
        }
        #endregion
    }
}
