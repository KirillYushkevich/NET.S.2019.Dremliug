using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class AccountStorageException : BankAccountException
    {
        public AccountStorageException() : base() { }

        public AccountStorageException(string message = null) : base(message) { }
    }
}
