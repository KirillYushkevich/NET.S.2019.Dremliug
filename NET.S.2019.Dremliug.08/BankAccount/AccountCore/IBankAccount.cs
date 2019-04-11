using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal interface IBankAccount
    {
        AccountStatus Status { get; set; }

        ulong ID { get; set; }

        IAccountOwner Owner { get; set; }

        decimal Balance { get; set; }

        AccountRank Rank { get; set; }

        int BonusPoints { get; set; }
    }
}
