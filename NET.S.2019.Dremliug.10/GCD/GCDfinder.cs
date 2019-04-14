using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCD
{
    // Originaly made with delegate, nothing to refactor.
    
    /// <summary>
    /// Provides methods to calculate a GCD of integer numbers.
    /// </summary>
    public static class GCDfinder
    {
        #region public EuclideanGCD()
        /// <summary> Finds the GCD of two or more integers by the Euclidean algorithm. </summary>
        /// <param name="numbers"> Numbers whose GCD is to be found. </param>
        /// <returns> The GCD of given integers. </returns>
        public static int EuclideanGCD(params int[] numbers) => EuclideanGCD(out _, numbers);

        /// <summary> Finds the GCD of two or more integers by the Euclidean algorithm and displays time spent on calculations. </summary>
        /// <param name="elapsedTime"> Time spent on calculations. </param>
        /// <param name="numbers"> Numbers whose GCD is to be found. </param>
        /// <returns> The GCD of given integers. </returns>
        public static int EuclideanGCD(out TimeSpan elapsedTime, params int[] numbers) => FindGCD(EuclideanAlgorithm, out elapsedTime, numbers);
        #endregion

        #region public SteinGCD()
        /// <summary> Finds the GCD of two or more integers by the Stein algorithm. </summary>
        /// <param name="numbers"> Numbers whose GCD is to be found. </param>
        /// <returns> The GCD of given integers. </returns>
        public static int SteinGCD(params int[] numbers) => SteinGCD(out _, numbers);

        /// <summary> Finds the GCD of two or more integers by the Stein algorithm and displays time spent on calculations. </summary>
        /// <param name="elapsedTime"> Time spent on calculations. </param>
        /// <param name="numbers"> Numbers whose GCD is to be found. </param>
        /// <returns> The GCD of given integers. </returns>
        public static int SteinGCD(out TimeSpan elapsedTime, params int[] numbers) => FindGCD(SteinAlgorithm, out elapsedTime, numbers);
        #endregion

        #region private FindGCD(): does the job.
        /// <summary> Finds the GCD of two or more integers by the selected algorithm. </summary>
        /// <param name="gcdAlgorithm"> Selected algorithm. </param>
        /// <param name="elapsedTime"> Time spent on calculations. </param>
        /// <param name="numbers"> Numbers whose GCD is to be found. </param>
        /// <returns> The GCD of given integers. </returns>
        private static int FindGCD(Func<int, int, int> gcdAlgorithm, out TimeSpan elapsedTime, params int[] numbers)
        {
            if (numbers is null)
            {
                throw new ArgumentNullException();
            }

            if (numbers.Length < 2)
            {
                throw new ArgumentException("Provide at least two numbers");
            }

            Stopwatch watch = Stopwatch.StartNew();
            int currentGCD = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                currentGCD = gcdAlgorithm(currentGCD, numbers[i]);
            }

            watch.Stop();

            elapsedTime = watch.Elapsed;
            return currentGCD;
        }
        #endregion

        #region Inner implementation of algorithms.

        #region Euclidean GCD of two numbers
        /// <summary> Finds the GCD of two numbers by the Euclidean algorithm. </summary>
        /// <param name="a"> An integer number. </param>
        /// <param name="b"> An integer number. </param>
        /// <returns> The GCD of two given integers. </returns>
        private static int EuclideanAlgorithm(int a, int b)
        {
            if (a == 0 && b == 0)
            {
                throw new ArgumentException("GCD of 0 and 0 is undefined");
            }

            if (a == 0)
            {
                return b;
            }

            if (b == 0)
            {
                return a;
            }

            // GCD of (± number, ± number) is the same positive number.
            int argA = Math.Abs(a);
            int argB = Math.Abs(b);

            // Set a greater element as argA.
            if (argA < argB)
            {
                (argA, argB) = (argB, argA);
            }

            while (argB != 0)
            {
                (argA, argB) = (argB, argA % argB);
            }

            return argA;
        }
        #endregion

        #region Stein GCD of two numbers
        /// <summary> Local function to find a GCD of two numbers by the Stein algorithm. </summary>
        /// <param name="a"> An integer number. </param>
        /// <param name="b"> An integer number. </param>
        /// <returns> The GCD of two given integers. </returns>
        private static int SteinAlgorithm(int a, int b)
        {
            if (a == 0 && b == 0)
            {
                throw new ArgumentException("GCD of 0 and 0 is undefined");
            }

            if (a == 0)
            {
                return b;
            }

            if (b == 0)
            {
                return a;
            }

            // GCD of (± number, ± number) is the same positive number.
            int argA = Math.Abs(a);
            int argB = Math.Abs(b);

            // If argA and argB are both even, gcd(argA, argB) = 2 * gcd(argA / 2, argB / 2).
            // If argA is even and argB is odd, gcd(argA, argB) = gcd(argA / 2, argB).
            // Otherwise both are odd, and gcd(argA, argB) = gcd( |argA - argB| / 2, argB).
            int shift = 0;

            // While both argA and argB are even.
            while (((argA | argB) & 1) == 0)
            {
                argA >>= 1;
                argB >>= 1;
                shift++;
            }

            // Make argA odd.
            while ((argA & 1) == 0)
            {
                argA >>= 1;
            }

            do
            {
                // Make argB odd.
                while ((argB & 1) == 0)
                {
                    argB >>= 1;
                }

                if (argA > argB)
                {
                    // Swap.
                    (argA, argB) = (argB, argA);
                }

                argB -= argA;
            }
            while (argB != 0);

            return argA << shift;
        }
        #endregion

        #endregion
    }
}
