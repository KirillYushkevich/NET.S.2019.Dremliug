using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal interface IAccountOwner
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
