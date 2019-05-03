using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities.BankAccount;

namespace BLL.Interface.Interfaces
{
    public interface IBonusCalculator
    {
        void ApplyWithdrawalBonus(Account account, decimal amount);

        void ApplyDepositBonus(Account account, decimal amount);
    }
}
