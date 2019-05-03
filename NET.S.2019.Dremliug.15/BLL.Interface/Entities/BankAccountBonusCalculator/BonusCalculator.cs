using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities.BankAccount;
using BLL.Interface.Interfaces;

namespace BLL.Interface.Entities.BankAccountBonusCalculator
{
    public class BonusCalculator : IBonusCalculator
    {
        private Func<decimal, decimal, int, int> _calculate;

        public BonusCalculator(Func<decimal, decimal, int, int> calculate)
        {
            _calculate = calculate;
        }

        public void ApplyDepositBonus(Account account, decimal amount)
        {
            account.BonusPoints += _calculate(account.Balance, amount, (int)account.Rank);
        }

        public void ApplyWithdrawalBonus(Account account, decimal amount)
        {
            account.BonusPoints -= _calculate(account.Balance, amount, (int)account.Rank);
        }
    }
}
