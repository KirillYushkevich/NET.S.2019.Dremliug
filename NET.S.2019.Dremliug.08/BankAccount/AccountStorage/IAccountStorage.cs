using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal interface IAccountStorage
    {
        IEnumerable<IBankAccount> LoadAccountBase();

        void SaveAccountBase(IEnumerable<IBankAccount> accountBase);
    }
}
