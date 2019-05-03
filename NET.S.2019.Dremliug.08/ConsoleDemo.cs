using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    public class ConsoleDemo
    {
        public static void Main()
        {
            // Use any option. 
            
            // Test with Storage and BonusCalculator creation.
            // IAccountControlService service = new AccountControlService(new AccountStorageBinaryFile(), new BonusCalculator((acc, isAddOp) => 0));
            
            // Test with demo classes.
            IAccountControlService service = new AccountControlService(new DemoStorage(), new DemoBonusCalculator());

            service.CreateNew(new AccountOwner("Firstname01", "Lastname01"), AccountRank.Base);
            service.CreateNew(new AccountOwner("Firstname02", "Lastname02"), AccountRank.Gold);
            service.CreateNew(new AccountOwner("Firstname03", "Lastname03"), AccountRank.Silver);
            service.CreateNew(new AccountOwner("Firstname04", "Lastname04"), AccountRank.Base);

            var allAccounts = service.ShowBase();

            foreach (var acc in allAccounts)
            {
                service.AddFunds(acc, 100);
            }

            Console.WriteLine("\n 100 was deposited to each account\n");

            foreach (var acc in allAccounts)
            {
                Console.WriteLine(ShowInfo(acc));
            }

            Console.WriteLine("\n 100 was withdrawed from each account \n");

            foreach (var acc in allAccounts)
            {
                service.WithdrawFunds(acc, 10);
            }

            foreach (var acc in allAccounts)
            {
                Console.WriteLine(ShowInfo(acc));
            }

            Console.WriteLine("\n End of demo.");

            string ShowInfo(IBankAccount account)
            {
                return $"[N:{account.ID }, Owner:{account.Owner.FirstName} {account.Owner.LastName}, B: {account.Balance}, Rank: {account.Rank}]";
            }
        }

        // Replases BonusCalculator creation.
        private class DemoBonusCalculator : IBonusCalculator
        {
            public void UpdateBonusPoints(IBankAccount account, bool isBalanceIncreased)
            {
                account.BonusPoints += (int)account.Rank * (int)account.Balance / 10 * (isBalanceIncreased ? 1 : -1);
            }
        }

        // If you want to test without creating a binary file.
        private class DemoStorage : IAccountStorage
        {
            public IEnumerable<IBankAccount> LoadAccountBase() => Enumerable.Empty<IBankAccount>();

            public void SaveAccountBase(IEnumerable<IBankAccount> accountBase)
            {
                // silently do nothing;
            }
        }
    }
}
