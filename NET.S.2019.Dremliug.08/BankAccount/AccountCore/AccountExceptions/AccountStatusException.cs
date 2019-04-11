using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class AccountStatusException : BankAccountException
    {
        public AccountStatusException() : base()
        {
        }

        public AccountStatusException(string message) : base(message)
        {
        }
    }
}
