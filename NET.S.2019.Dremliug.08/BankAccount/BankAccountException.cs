using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    class BankAccountException : Exception
    {
        public BankAccountException() : base() { }

        public BankAccountException(string message) : base(message) { }
    }
}
