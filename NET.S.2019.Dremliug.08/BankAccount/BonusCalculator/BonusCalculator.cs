using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    /// <summary>Encapsulates a bonus point calculator based on the desired calculation function.</summary>
    internal class BonusCalculator : IBonusCalculator
    {
        private Func<IBankAccount, bool, int> _calculatorFunction;

        /// <summary>Creates new <see cref="BonusCalculator"/> based on <see cref="calculatorFunction"/> provided.</summary>
        /// <param name="calculatorFunction"></param>
        public BonusCalculator(Func<IBankAccount, bool, int> calculatorFunction) => _calculatorFunction = calculatorFunction;

        /// <summary>Updates bonus points for account.</summary>
        /// <param name="currentBonusPoints"></param>
        public virtual void UpdateBonusPoints(IBankAccount account, bool isBalanceIncreased) => account.BonusPoints = _calculatorFunction(account, isBalanceIncreased);
    }
}
