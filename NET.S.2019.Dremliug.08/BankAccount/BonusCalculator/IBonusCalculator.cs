using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal interface IBonusCalculator
    {
        void UpdateBonusPoints(IBankAccount account, bool isBalanceIncreased); 
    }
}
