using System.Collections.Generic;
using System.Linq;
using BLL.Exceptions;
using BLL.Interface.Entities.BankAccount;
using BLL.Interface.Entities.BankAccountOwner;
using BLL.Interface.Exceptions;
using BLL.Interface.Interfaces;
using DAL.Interface.Interfaces;

namespace BLL.ControlServices
{
    public class AccountControlService : IAccountControlService
    {
        private HashSet<Account> _setOfAccounts = new HashSet<Account>();
        private IAccountRepository _accountRepository;
        private INumberGenerator _numberGenerator;
        private IBonusCalculator _bonusCalculator;

        public AccountControlService(IAccountRepository accountRepository, INumberGenerator numberGenerator, IBonusCalculator bonusCalculator)
        {
            _accountRepository = accountRepository;
            _numberGenerator = numberGenerator;
            _bonusCalculator = bonusCalculator;
            LoadBase();
        }

        public IEnumerable<Account> DisplayAllAccounts { get => _setOfAccounts.AsEnumerable(); }

        public void DepositFunds(Account account, decimal amount)
        {
            if (amount <= 0)
            {
                throw new AccountControlServiceException($"Deposint amount must be positive: {amount}");
            }

            account.Balance += amount;
            _bonusCalculator.ApplyDepositBonus(account, amount);
        }

        public void WithdrawFunds(Account account, decimal amount)
        {
            if (amount <= 0)
            {
                throw new AccountControlServiceException($"Withdrawal amount must be positive: {amount}");
            }

            account.Balance -= amount;
            _bonusCalculator.ApplyWithdrawalBonus(account, amount);
        }

        public Account CreateNew(AccountOwner owner, AccountRank rank, decimal balance = 0)
        {
            Account newAccount = new Account(_numberGenerator.Generate(), owner, balance, rank);
            _setOfAccounts.Add(newAccount);

            return newAccount;
        }

        public void Suspend(Account account)
        {
            account.Status = AccountStatus.Suspended;
        }

        public void Close(Account account)
        {
            account.Status =
                account.Balance == 0 ?
                AccountStatus.Closed :
                throw new AccountStatusException("Unable to close an account with non-zero balance.");
        }

        public void LoadBase()
        {
            _setOfAccounts = _accountRepository.LoadAccountBase().Select(x => Mappers.DALMapper.MapToAccountEntity(x, _accountRepository.LoadAccountOwner(x.OwnerUid))).ToHashSet();
        }

        public void SaveBase() => _accountRepository.SaveAccountBase(_setOfAccounts.Select(x => Mappers.DALMapper.MapFromAccountEntity(x)));
    }
}
