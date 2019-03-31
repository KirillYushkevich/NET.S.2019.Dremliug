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
        private readonly double[] _coeffs;

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

        #region Properties
        /// <summary>
        /// The degree of polynomial.
        /// </summary>
        public int Degree { get; }
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

            sb.Append($"{_coeffs[length - 1]}*x^{length - 1} ");

            for (int i = length - 2; i >= 2; i--)
            {
                if (_coeffs[i] != 0)
                {
                    sb.Append($"{withSign(_coeffs[i])}*x^{i} ");
                }
            }

            if (_coeffs[1] != 0)
            {
                sb.Append($"{withSign(_coeffs[1])}*x ");
            }
            if (_coeffs[0] != 0)
            {
                sb.Append($"{withSign(_coeffs[0])}");
            }

            return sb.ToString().Trim();

            string withSign(double value)
            {
                return value > 0 ? $"+ {value}" : $"- {-value}";
            }
        }
        #endregion

        public double[] ToArray() => _coeffs.ToArray();

        #region overloaded operators
        public static Polynomial operator +(Polynomial left, Polynomial right)
        {
            double[] leftA = left.ToArray();
            double[] rightA = right.ToArray();

            // Zip both arrays + remaining elements of the left if any + remaining elements of the right if any.
            return new Polynomial(leftA.Zip(rightA, (x, y) => x + y)
                                        .Concat(leftA.Skip(rightA.Length))
                                        .Concat(rightA.Skip(leftA.Length))
                                        .ToArray());
        }

        public static Polynomial operator -(Polynomial left, Polynomial right)
        {
            double[] leftA = left.ToArray();
            double[] rightA = right.ToArray();

            // Zip both arrays + remaining elements of the left if any + remaining elements of the right multiplied by -1 if any.
            return new Polynomial(leftA.Zip(rightA, (x, y) => x - y)
                                        .Concat(leftA.Skip(rightA.Length))
                                        .Concat(rightA.Skip(leftA.Length).Select((x) => -x))
                                        .ToArray());
        }

        public static Polynomial operator *(Polynomial left, Polynomial right)
        {
            double[] leftA = left.ToArray();
            double[] rightA = right.ToArray();
            double[] result = new double[leftA.Length + rightA.Length - 1];

            for (int i = 0; i < leftA.Length; i++)
            {
                for (int j = 0; j < rightA.Length; j++)
                {
                    result[i + j] += leftA[i] * rightA[j];
                }
            }

            return new Polynomial(result);
        }
        #endregion
    }
}
