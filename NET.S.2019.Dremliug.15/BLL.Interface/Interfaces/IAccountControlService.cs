using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities.BankAccount;
using BLL.Interface.Entities.BankAccountOwner;

namespace BLL.Interface.Interfaces
{
    public interface IAccountControlService
    {
        IEnumerable<Account> DisplayAllAccounts { get; }

        void DepositFunds(Account account, decimal amount);

        void WithdrawFunds(Account account, decimal amount);

        Account CreateNew(AccountOwner owner, AccountRank rank, decimal balance = 0);

        void Suspend(Account account);

        void Close(Account account);
    }
}
