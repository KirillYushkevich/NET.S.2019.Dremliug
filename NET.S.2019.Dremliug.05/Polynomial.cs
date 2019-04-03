using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.S._2019.Dremliug._05
{
    /// <summary> Floating-point polynomial in one variable. </summary>
    public sealed class Polynomial : IEquatable<Polynomial>
    {
        #region fields
        private readonly double[] _coeffs;
        #endregion

        #region Properties
        /// <summary>
        /// The degree of polynomial.
        /// </summary>
        public int Degree { get; }

        public double this[int index]
        {
            get
            {
                try
                {
                    return _coeffs[index];
                }
                // Suppress the exception of a private field to keep the details private and throw a new one from the indexer.
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException($"{index}");
                }
            }
            set {
                try
                {
                    _coeffs[index] = value;
                }
                // Suppress the exception of a private field to keep the details private and throw a new one from the indexer.
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException($"{index}");
                }
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a polynomial with given coefficients.
        /// </summary>
        /// <param name="coeffs"> Polynomial coefficients. </param>
        public Polynomial(params double[] coeffs)
        {
            if (coeffs is null)
            {
                throw new ArgumentNullException($"{coeffs} must not be null");
            }
            if (coeffs.Length == 0)
            {
                throw new ArgumentException($"{coeffs} must have at least one argument");
            }

            // Get the highers degree of polynomial.
            int highestDegreeWithNonZeroCoeff = 0;
            for (int i = coeffs.Length - 1; i >= 0; i--)
            {
                if (coeffs[i] != 0)
                {
                    highestDegreeWithNonZeroCoeff = i;
                    break;
                }
            }

            // Create a polynomial of the degree found.
            _coeffs = new double[highestDegreeWithNonZeroCoeff + 1];
            for (int i = 0; i < _coeffs.Length; i++)
            {
                _coeffs[i] = coeffs[i];
            }

            // Set the degree of polynomial.
            Degree = highestDegreeWithNonZeroCoeff;
        }
        #endregion

        #region Equality
        public override bool Equals(object obj)
        {
            return Equals(obj as Polynomial);
        }

        public bool Equals(Polynomial other)
        {
            return other != null &&
                   this.ToArray().SequenceEqual(other.ToArray());
        }

        public override int GetHashCode()
        {
            return ((IStructuralEquatable)_coeffs).GetHashCode(EqualityComparer<double>.Default);
        }

        public static bool operator ==(Polynomial polynomial1, Polynomial polynomial2)
        {
            return ReferenceEquals(polynomial1, polynomial2) ||
                (polynomial1?.ToArray().SequenceEqual(polynomial2?.ToArray() ?? Enumerable.Empty<double>()) ?? false);
        }

        public static bool operator !=(Polynomial polynomial1, Polynomial polynomial2)
        {
            return !(polynomial1 == polynomial2);
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            int length = _coeffs.Length;

            for (int i = length - 1; i >= 0; i--)
            {
                if (_coeffs[i] != 0)
                {
                    sb.Append($"{withSign(_coeffs[i])}");
                    if (i == 1)
                    {
                        sb.Append($"x");
                    }
                    else if (i > 1)
                    {
                        sb.Append($"x^{i}");
                    }
                }
            }

            sb.Remove(0, 3);

            return sb.ToString();

            string withSign(double value)
            {
                return value > 0 ? $" + {value}" : $" - {-value}";
            }
        }
        #endregion

        /// <summary>
        /// Creates a <see cref="double"/>[] array of coefficients from a <see cref="Polynomial"/>.
        /// </summary>
        /// <returns></returns>
        public double[] ToArray() => _coeffs.ToArray();

        #region overloaded operators
        public static Polynomial operator +(Polynomial left, Polynomial right)
        {
            (double[] leftAsArray, double[] rightAsArray) = TryOperandsAsArrays(left, right);

            // Zip both arrays + remaining elements of the left if any + remaining elements of the right if any.
            return new Polynomial(leftAsArray.Zip(rightAsArray, (x, y) => x + y)
                                        .Concat(leftAsArray.Skip(rightAsArray.Length))
                                        .Concat(rightAsArray.Skip(leftAsArray.Length))
                                        .ToArray());
        }

        public static Polynomial operator -(Polynomial left, Polynomial right)
        {
            (double[] leftAsArray, double[] rightAsArray) = TryOperandsAsArrays(left, right);

            // Zip both arrays + remaining elements of the left if any + remaining elements of the right multiplied by -1 if any.
            return new Polynomial(leftAsArray.Zip(rightAsArray, (x, y) => x - y)
                                        .Concat(leftAsArray.Skip(rightAsArray.Length))
                                        .Concat(rightAsArray.Skip(leftAsArray.Length).Select((x) => -x))
                                        .ToArray());
        }

        public static Polynomial operator *(Polynomial left, Polynomial right)
        {
            (double[] leftAsArray, double[] rightAsArray) = TryOperandsAsArrays(left, right);

            double[] result = new double[leftAsArray.Length + rightAsArray.Length - 1];

            for (int i = 0; i < leftAsArray.Length; i++)
            {
                for (int j = 0; j < rightAsArray.Length; j++)
                {
                    result[i + j] += leftAsArray[i] * rightAsArray[j];
                }
            }

            return new Polynomial(result);
        }

        #region private TryOperandsAsArrays()
        /// <summary>
        /// Tries to convert two <see cref="Polynomial"/> to arrays.
        /// </summary>
        /// <param name="left"> Left <see cref="Polynomial"/> operand. </param>
        /// <param name="right"> Right <see cref="Polynomial"/> operand. </param>
        /// <returns> (left <see cref="double"/>[], right <see cref="double"/>[]) </returns>
        /// <exception cref="ArgumentNullException"> Thrown when <paramref name="left"/> or <paramref name="right"/> is null. </exception>
        private static (double[], double[]) TryOperandsAsArrays(Polynomial left, Polynomial right)
        {
            if (left is null || right is null)
            {
                throw new ArgumentNullException($"left is {left?.GetType().ToString() ?? "null"}, right is {right?.GetType().ToString() ?? "null" }");
            }

            return (left.ToArray(), right.ToArray());
        }
        #endregion
        #endregion
    }
}
