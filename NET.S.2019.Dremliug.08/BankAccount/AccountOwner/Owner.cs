using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class AccountOwner : IAccountOwner, IEquatable<AccountOwner>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public AccountOwner(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        #region Equals and GetHashCode
        public override bool Equals(object obj)
        {
            return Equals(obj as AccountOwner);
        }

        public bool Equals(AccountOwner other)
        {
            return other != null &&
                   (FirstName, LastName) == (other.FirstName, other.LastName);
        }

        public override int GetHashCode()
        {
            return (FirstName, LastName).GetHashCode();
        }

        public static bool operator ==(AccountOwner owner1, AccountOwner owner2)
        {
            return EqualityComparer<AccountOwner>.Default.Equals(owner1, owner2);
        }

        public static bool operator !=(AccountOwner owner1, AccountOwner owner2)
        {
            return !(owner1 == owner2);
        } 
        #endregion
    }
}
