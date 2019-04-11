using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class AccountOwnerException : BankAccountException
    {
        public AccountOwnerException() : base()
        {
        }

        public AccountOwnerException(string message) : base(message)
        {
        }
    }
}
