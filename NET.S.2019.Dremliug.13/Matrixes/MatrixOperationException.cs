using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
