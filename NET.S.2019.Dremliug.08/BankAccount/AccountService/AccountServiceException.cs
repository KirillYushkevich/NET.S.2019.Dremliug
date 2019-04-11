using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class AccountServiceException : BankAccountException
    {
        public AccountServiceException() : base() { }

        public AccountServiceException(string message) : base(message) { }
    }
}
