using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities.BankAccountOwner;
using BLL.Interface.Exceptions;

namespace BLL.Interface.Entities.BankAccount
{
    /// <summary>
    /// Represents bank account.
    /// </summary>
    public class Account : IEquatable<Account>
    {
        #region fields
        private AccountStatus _status;
        private string _number;
        private AccountOwner _owner;
        private decimal _balance;
        private int _bonusPoints;
        private AccountRank _rank; 
        #endregion

        #region Constructors
        public Account(string number, AccountOwner owner, decimal balance, AccountRank rank)
        {
            Status = AccountStatus.Active;
            Number = number;
            Owner = owner;
            Balance = balance;
            BonusPoints = 0;
            Rank = rank;
        }
        #endregion

        #region Properties
        public AccountStatus Status { get => _status; set => _status = Enum.IsDefined(typeof(AccountStatus), value) ? value : throw new AccountStatusException($"Status {value} is not defined"); }

        public string Number { get => _number; set => _number = value; }

        public AccountOwner Owner { get => _owner; set => _owner = value ?? throw new AccountOwnerException("Owner must not be null"); }

        public decimal Balance { get => _balance; set => _balance = value; }

        public int BonusPoints { get => _bonusPoints; set => _bonusPoints = value; }

        public AccountRank Rank { get => _rank; set => _rank = Enum.IsDefined(typeof(AccountRank), value) ? value : throw new AccountRankException($"Rank {value} is not defined"); }
        #endregion

        #region Equals and GetHashCode
        public static bool operator ==(Account account1, Account account2)
        {
            return EqualityComparer<Account>.Default.Equals(account1, account2);
        }

        public static bool operator !=(Account account1, Account account2)
        {
            return !(account1 == account2);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Account);
        }

        public bool Equals(Account other)
        {
            return other != null && Number == Number;
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }
        #endregion
    }
}
