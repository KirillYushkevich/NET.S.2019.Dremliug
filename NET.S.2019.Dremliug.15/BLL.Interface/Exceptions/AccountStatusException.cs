using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Exceptions
{
    /// <summary>
    /// Represents exceptions in account status operations.
    /// </summary>
    public class AccountStatusException : Exception
    {
        public AccountStatusException() : base()
        {
        }

        public AccountStatusException(string message) : base(message)
        {
        }
    }
}
