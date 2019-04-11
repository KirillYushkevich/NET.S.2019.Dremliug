using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class AccountControlService
    {
        private HashSet<IBankAccount> _setOfAccounts;
        private ulong _nextAvailableID = 0;
        private IAccountStorage _accountStorage;
        private IBonusCalculator _bonusCalculator;

        public AccountControlService(IAccountStorage accountStorage, IBonusCalculator bonusCalculator)
        {
            _accountStorage = accountStorage;
            _bonusCalculator = bonusCalculator;
            LoadBase();
        }

        public void AddFunds(IBankAccount account, decimal amount)
        {
            if (amount <= 0)
            {
                throw new AccountServiceException($"Amont must be positive: {amount}");
            }

            account.Balance += amount;
            _bonusCalculator.UpdateBonusPoints(account: account, isBalanceIncreased: true);
        }

        public void WithdrawFunds(IBankAccount account, decimal amount)
        {
            if (amount <= 0)
            {
                throw new AccountServiceException($"Amont must be positive: {amount}");
            }

            account.Balance -= amount;
            _bonusCalculator.UpdateBonusPoints(account: account, isBalanceIncreased: false);
        }

        public void CreateNew(IAccountOwner owner, AccountRank rank, decimal balance = 0)
        {
            _setOfAccounts.Add(new AccountTemplate(_nextAvailableID, owner, balance, rank));
            _nextAvailableID++;
        }

        public void Suspend(IBankAccount account)
        {
            account.Status = AccountStatus.Suspended;
        }

        public void Close(IBankAccount account)
        {
            account.Status =
                account.Balance == 0 ?
                AccountStatus.Closed :
                throw new AccountStatusException("Unable to close an account with non-zero balance.");
        }

        public void LoadBase()
        {
            _setOfAccounts = new HashSet<IBankAccount>();

            foreach (IBankAccount account in _accountStorage.LoadAccountBase())
            {
                _setOfAccounts.Add(account);
                _nextAvailableID = Math.Max(account.ID, _nextAvailableID);
            }

            _nextAvailableID++;
        }

        public void SaveBase() => _accountStorage.SaveAccountBase(_setOfAccounts);
    }
}
