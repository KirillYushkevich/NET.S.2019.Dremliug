using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class AccountRankException : BankAccountException
    {
        public AccountRankException() : base()
        {
        }

        public AccountRankException(string message) : base(message)
        {
        }
    }
}
