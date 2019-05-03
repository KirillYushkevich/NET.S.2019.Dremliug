using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Fake
{
    public static class Data
    {
        private static int testIncrementer = 0;

        public static Func<string> NumberGenerator { get; } = () => $"NewTestAcc{testIncrementer++:00}";

        public static Func<decimal, decimal, int, int> BonusCalculator { get; } = (balance, amount, rank) => (int)(balance * 100M / amount) * rank;
    }
}
