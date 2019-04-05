using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.S._2019.Dremliug._02
{
    public static class Homework02
    {
        #region InsertNumber
        /// <summary> Copies bits of a <paramref name="source"/> number into [<paramref name="startOfBitRange"/>, <paramref name="endOfBitRange"/>] range of a <paramref name="target"/> number. </summary>
        /// <param name="target"> An integer to insert bits into. </param>
        /// <param name="source"> An integer to insert bits from. </param>
        /// <param name="startOfBitRange"> The first position of a range. </param>
        /// <param name="endOfBitRange"> The last position of a range. </param>
        /// <returns> Modified target number. </returns>
        public static int InsertNumber(int target, int source, int startOfBitRange, int endOfBitRange)
        {
            // Make sure that the range of bits corresponds to a 32-bit integer.
            if (startOfBitRange < 0 || endOfBitRange < startOfBitRange || endOfBitRange > 31)
            {
                throw new ArgumentOutOfRangeException($"Bit positions must be in [0, 31] range; [{startOfBitRange}, {endOfBitRange}]");
            }

            // Create a bit mask of the required length.
            int mask = ~(-1 << (endOfBitRange - startOfBitRange + 1));

            // Take the required amount of bits from the source number.
            int takenBitField = source & mask;

            // Replace bits in the target number and return the result.
            return (target & ~(mask << startOfBitRange)) | (takenBitField << startOfBitRange);
        }
        #endregion

        #region FindNextBiggerNumber
        /// <summary> Takes a positive integer number and returns the nearest largest integer consisting of the digits of the original number, -1 if no such number exists. </summary>
        /// <param name="number"> A positive integer number. </param>
        /// <returns> The nearest largest integer consisting of the digits of the original number; -1 if no such number exists. </returns>
        public static int FindNextBiggerNumber(int number)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException("number", $"{number} must be positive");
            }

            // Next bigger number is guaranteed not exist.
            if (number < 12)
            {
                return -1;
            }

            // Split the number to digits in the original order and store in a list. Lower positions of the digits have smaller indexes.
            var digits = new List<int>(10);
            int numberRemains = number;

            do
            {
                digits.Add(numberRemains % 10);
                numberRemains /= 10;
            }
            while (numberRemains > 0);

            // Search for a position that has a higher digit value than the digit in previous position.
            int index;
            for (index = 1; index < digits.Count; index++)
            {
                if (digits[index - 1] > digits[index])
                {
                    break;
                }
            }

            // If failed to find then NextBigger does not exist.
            if (index == digits.Count)
            {
                return -1;
            }

            // Swap the digit at [index] with the digit at [index - 1].
            (digits[index], digits[index - 1]) = (digits[index - 1], digits[index]);

            // Sort digits by value at [0, index] range in descending order to make the new number as small as possible.
            digits.Sort(0, index, Comparer<int>.Create((a, b) => b.CompareTo(a)));

            // Build the new number.
            int newNumber = 0;
            for (int i = 0; i < digits.Count; i++)
            {
                try
                {
                    checked
                    {
                        newNumber += digits[i] * (int)Math.Pow(10, i);
                    }
                }
                catch (OverflowException)
                {
                    // If overflow happens then Int32 number does not exist.
                    return -1;
                }
            }

            return newNumber;
        }
        #endregion

        #region TimeElapsed
        /// <summary>
        /// Displays an execution time of FindNextBiggerNumber method.
        /// </summary>
        /// <param name="number"> A number for FindNextBiggerNumber() method. </param>
        /// <returns> Elapsed time as a TimeSpan value. </returns>
        public static TimeSpan TimeElapsedFindNextBiggerNumber(int number)
        {
            // Warm-up is required.
            FindNextBiggerNumber(number);

            var watch = Stopwatch.StartNew();
            FindNextBiggerNumber(number);
            watch.Stop();

            return watch.Elapsed;
        }
        #endregion

        #region FilterDigit
        /// <summary> Takes a list of integers and filters the list so that only numbers containing the specified digit are left. </summary>
        /// <param name="list"> A list of integers. </param>
        /// <param name="digit"> A digit to search for. </param>
        public static void FilterDigit(ref List<int> list, int digit)
        {
            if (list is null)
            {
                throw new ArgumentNullException("list");
            }

            if (list.Count == 0)
            {
                throw new ArgumentException("list", "list must contain elements");
            }

            if (digit > 9 || digit < 0)
            {
                throw new ArgumentOutOfRangeException("digit", $"digit must be in [0, 9] range; {digit}");
            }

            var filteredList = new List<int>(list.Count);
            var alreadySeenElements = new HashSet<int>(list.Count);
            int currentElement;

            foreach (var v in list)
            {
                // If the current value has not been processed before.
                if (!alreadySeenElements.Contains(v))
                {
                    alreadySeenElements.Add(v);
                    currentElement = Math.Abs(v);

                    // Check every digit of each number.
                    do
                    {
                        if (currentElement % 10 == digit)
                        {
                            filteredList.Add(v);
                            alreadySeenElements.Add(v);
                            break;
                        }

                        currentElement /= 10;
                    }
                    while (currentElement > 0);
                }
            }

            // Set reference to the filtered list.
            list = filteredList;
        }
        #endregion

        #region FindNthRoot
        /// <summary>
        /// Calculates the nth root of a number by the Newton method with a given accuracy.
        /// </summary>
        /// <param name="number"> A number to calculate root from. </param>
        /// <param name="degree"> A degree of the root. </param>
        /// <param name="precision"> An accuracy of the result. </param>
        /// <returns> <paramref name="Degree"/> root of the <paramref name="number"/> with the <paramref name="precision"/>. </returns>
        public static double FindNthRoot(double number, int degree, double precision)
        {
            if (double.IsNaN(number) || double.IsInfinity(number))
            {
                throw new ArgumentOutOfRangeException("number", $"number must be a real number; {number}");
            }

            if (degree < 1)
            {
                throw new ArgumentOutOfRangeException("degree", $"degree must be a natural number; {degree}");
            }

            if (precision >= 1 || precision <= 0 || double.IsInfinity(precision) || double.IsNaN(precision))
            {
                throw new ArgumentOutOfRangeException("precision", $"precision must be in (0, 1) range; {precision}");
            }

            int numberOfFractionalDigits = 0;
            while (precision < 1 && numberOfFractionalDigits < 16)
            {
                precision *= 10;
                numberOfFractionalDigits++;
            }

            // A Double value has up to 15 decimal digits of precision. 
            if (numberOfFractionalDigits > 15)
            {
                throw new ArgumentOutOfRangeException("precision", $"precision limit is 15 fractional digits; {precision}");
            }

            double x0;

            // Guess an initial value of x1.
            double x1 = 1;
            do
            {
                x0 = x1;
                x1 += ((number / Math.Pow(x1, degree - 1.0)) - x1) / degree;
            }
            while (Math.Abs(x0 - x1) > Math.Pow(10, -16));

            // (10 ^ -16) difference is the precision limit. Further computations won't change x1.
            // If an endless loop happens, decrease to (10 ^ -15).

            // Round to the required precision.
            return Math.Round(x1, numberOfFractionalDigits);
        }
        #endregion
    }
}
