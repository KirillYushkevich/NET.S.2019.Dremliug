using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    public abstract class AccountTemplate
    {
        internal bool IsActive { get; set; }
        internal ulong ID { get; set; }
        internal Owner Owner { get; set; }
        internal decimal Balance { get; set; }
        internal int Bonus { get; set; }
    }
}
