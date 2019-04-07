using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    public class Owner
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Owner(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
