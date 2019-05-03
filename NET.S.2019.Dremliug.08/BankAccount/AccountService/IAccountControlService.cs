using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal interface IAccountControlService
    {
        void AddFunds(IBankAccount account, decimal amount);

        void WithdrawFunds(IBankAccount account, decimal amount);

        void CreateNew(IAccountOwner owner, AccountRank rank, decimal balance = 0);

        void Suspend(IBankAccount account);

        void Close(IBankAccount account);

        void LoadBase();

        void SaveBase();

        IEnumerable<IBankAccount> ShowBase();
    }
}
