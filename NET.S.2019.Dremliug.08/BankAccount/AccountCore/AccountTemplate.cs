using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class AccountTemplate : IEquatable<AccountTemplate>, IBankAccount
    {
        #region Properties
        public AccountStatus Status
        {
            get => Status;
            set
            {
                Status = (Enum.IsDefined(typeof(AccountStatus), value)) ? value : throw new AccountStatusException("Status is not defined");
            }
        }

        public ulong ID { get; set; }

        public IAccountOwner Owner
        {
            get => Owner;
            set => Owner = value ?? throw new AccountOwnerException("Owner must not be null");
        }

        public decimal Balance { get; set; }

        public int BonusPoints { get; set; }

        public AccountRank Rank { get => Rank; set => Rank = (Enum.IsDefined(typeof(AccountRank), value)) ? value : throw new AccountRankException("Rank is not defined"); }
        #endregion

        #region Constructors
        public AccountTemplate(ulong id, IAccountOwner owner, decimal balance, AccountRank rank)
        {
            Status = AccountStatus.Active;
            ID = id;
            Owner = owner;
            Balance = balance;
            BonusPoints = 0;
            Rank = rank;
        }
        #endregion

        #region Equals and GetHashCode
        public override bool Equals(object obj)
        {
            return Equals(obj as AccountTemplate);
        }

        public bool Equals(AccountTemplate other)
        {
            return other != null && ID == ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public static bool operator ==(AccountTemplate account1, AccountTemplate account2)
        {
            return EqualityComparer<AccountTemplate>.Default.Equals(account1, account2);
        }

        public static bool operator !=(AccountTemplate account1, AccountTemplate account2)
        {
            return !(account1 == account2);
        } 
        #endregion
    }
}