using System;

namespace Matrixes
{
    public class MatrixOperationException : Exception
    {
        public MatrixOperationException() : base()
        {
        }

        public MatrixOperationException(string message) : base(message)
        {
        }
    }
}
