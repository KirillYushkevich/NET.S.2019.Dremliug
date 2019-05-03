using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    /// <summary>
    /// Represents exceptions in account control service operations.
    /// </summary>
    public class AccountControlServiceException : Exception
    {
        public AccountControlServiceException() : base()
        {
        }

        public AccountControlServiceException(string message) : base(message)
        {
        }
    }
}
