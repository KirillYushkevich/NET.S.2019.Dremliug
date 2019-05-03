using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Interfaces;

namespace BLL.Interface.Entities.BankAccountOwner
{
    public class AccountOwner : IEquatable<AccountOwner>
    {
        public AccountOwner(string uid, string firstName, string lastName)
        {
            Uid = uid;
            FirstName = firstName;
            LastName = lastName;
        }

        public string Uid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        #region Equals and GetHashCode
        public static bool operator ==(AccountOwner owner1, AccountOwner owner2)
        {
            return EqualityComparer<AccountOwner>.Default.Equals(owner1, owner2);
        }

        public static bool operator !=(AccountOwner owner1, AccountOwner owner2)
        {
            return !(owner1 == owner2);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as AccountOwner);
        }

        public bool Equals(AccountOwner other)
        {
            return other != null && this.GetType() == other.GetType() &&
                   this.Uid == other.Uid;
        }

        public override int GetHashCode()
        {
            return Uid.GetHashCode();
        }
        #endregion
    }
}
