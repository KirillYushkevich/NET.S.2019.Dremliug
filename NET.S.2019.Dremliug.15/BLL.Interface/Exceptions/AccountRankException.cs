using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Exceptions
{
    /// <summary>
    /// Represents exceptions in account rank operations.
    /// </summary>
    public class AccountRankException : Exception
    {
        public AccountRankException() : base()
        {
        }

        public AccountRankException(string message) : base(message)
        {
        }
    }
}
