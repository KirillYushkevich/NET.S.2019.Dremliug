using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class AccountCore : IEquatable<AccountCore>, IBankAccount
    {
        #region fields
        private AccountStatus _status;
        private IAccountOwner _owner;
        private AccountRank _rank; 
        #endregion

        #region Constructors
        public AccountCore(ulong id, IAccountOwner owner, decimal balance, AccountRank rank)
        {
            Status = AccountStatus.Active;
            ID = id;
            Owner = owner;
            Balance = balance;
            BonusPoints = 0;
            Rank = rank;
        }
        #endregion

        #region Properties
        public AccountStatus Status
        {
            get => _status;
            set
            {
                _status = Enum.IsDefined(typeof(AccountStatus), value) ? value : throw new AccountStatusException("Status is not defined");
            }
        }

        public ulong ID { get; set; }

        public IAccountOwner Owner
        {
            get => _owner;
            set => _owner = value ?? throw new AccountOwnerException("Owner must not be null");
        }

        public decimal Balance { get; set; }

        public int BonusPoints { get; set; }

        public AccountRank Rank { get => _rank; set => _rank = Enum.IsDefined(typeof(AccountRank), value) ? value : throw new AccountRankException("Rank is not defined"); }
        #endregion

        #region Equals and GetHashCode
        public static bool operator ==(AccountCore account1, AccountCore account2)
        {
            return EqualityComparer<AccountCore>.Default.Equals(account1, account2);
        }

        public static bool operator !=(AccountCore account1, AccountCore account2)
        {
            return !(account1 == account2);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as AccountCore);
        }

        public bool Equals(AccountCore other)
        {
            return other != null && ID == ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
        #endregion
    }
}