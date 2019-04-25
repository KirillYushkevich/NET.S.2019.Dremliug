using System;
using System.Linq.Expressions;

namespace Matrixes
{
    /// <summary>
    /// Provides operations with square matrixes.
    /// </summary>
    /// <typeparam name="Telement"></typeparam>
    public static class MatrixOperator<Telement>
    {
        #region public Sum()
        /// <summary>
        /// Sums two square matrixes using <paramref name="sumFunction"/> on their values.
        /// </summary>
        /// <typeparam name="Tmatrix"> SquareMatrix or derived. </typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="sumFunction"></param>
        /// <returns></returns>
        public static Tmatrix Sum<Tmatrix>(Tmatrix a, Tmatrix b, Func<Telement, Telement, Telement> sumFunction = null)
            where Tmatrix : SquareMatrix<Telement>
        {
            sumFunction = sumFunction ?? FindSumFunction() ??
                throw new MatrixOperationException($"Cannot find a method for summing values of type {typeof(Telement)}");

            return Combine(a, b, sumFunction);
        } 
        #endregion

        #region public Combine()
        /// <summary>
        /// Combines two square matrixes using <paramref name="combineFunction"/> on their values.
        /// </summary>
        /// <typeparam name="Tmatrix"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="combineFunction"></param>
        /// <returns></returns>
        public static Tmatrix Combine<Tmatrix>(Tmatrix a, Tmatrix b, Func<Telement, Telement, Telement> combineFunction)
             where Tmatrix : SquareMatrix<Telement>
        {
            if (a is null || b is null)
            {
                return a ?? b;
            }

            int sizeOfA = a.Size;
            int sizeOfB = b.Size;
            int sizeOfResult = Math.Min(sizeOfA, sizeOfB);

            // Get the constructor of this generic type and create a new matrix.
            Tmatrix result =
                (Tmatrix)
                typeof(Tmatrix)
                .GetConstructor(new Type[] { typeof(int) })
                ?.Invoke(new object[] { sizeOfResult })
                ??
                throw new MatrixOperationException($"Failed to get a contructor with the matrix size parameter for the resulting matrix");

            // Fill in the new matrix.
            for (int j = 0; j < sizeOfResult; j++)
            {
                for (int i = 0; i < sizeOfResult; i++)
                {
                    result[i, j] = combineFunction(a[i, j], b[i, j]);
                }
            }

            return result;
        } 
        #endregion

        #region private FindSumFunction()
        /// <summary>
        /// Tries to find a sum method for instances of the class.
        /// </summary>
        /// <returns></returns>
        private static Func<Telement, Telement, Telement> FindSumFunction()
        {
            Func<Telement, Telement, Telement> sumFunction = null;

            // Read more here https://jonskeet.uk/csharp/genericoperators.html .
            try
            {
                ParameterExpression left = Expression.Parameter(typeof(Telement), "left");
                ParameterExpression right = Expression.Parameter(typeof(Telement), "right");
                BinaryExpression operation = Expression.Add(left, right);
                sumFunction = Expression.Lambda<Func<Telement, Telement, Telement>>(operation, left, right).Compile();
            }
            catch (InvalidOperationException)
            {
                // + operator is not applicable for this type. Failed to compile the lambda.
            }

            // If the lambda hasn't beed compiled then try this way.
            if (sumFunction is null)
            {
                // Possible names of a static method that may provide any kind of a sum-like operation.
                string[] sumMethodNames =
                {
                    "op_Addition",
                    "Concat",
                    "Combine",
                    "Sum",
                    "Add",
                };

                foreach (string methodName in sumMethodNames)
                {
                    // Try to find a static method by name.
                    sumFunction =
                        (Func<Telement, Telement, Telement>)
                        typeof(Telement)
                        .GetMethod(methodName, new Type[] { typeof(Telement), typeof(Telement) })
                        ?.CreateDelegate(typeof(Func<Telement, Telement, Telement>));

                    // Stop the search if succeeded.
                    if (sumFunction != null)
                    {
                        break;
                    }
                }
            }

            return sumFunction;
        } 
        #endregion
    }
}
