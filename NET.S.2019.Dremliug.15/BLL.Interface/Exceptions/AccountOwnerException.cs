using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Exceptions
{
    /// <summary>
    /// Represents exceptions in account owner operations.
    /// </summary>
    public class AccountOwnerException : Exception
    {
        public AccountOwnerException() : base()
        {
        }

        public AccountOwnerException(string message) : base(message)
        {
        }
    }
}
