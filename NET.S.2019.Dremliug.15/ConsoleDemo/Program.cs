using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities.BankAccount;
using BLL.Interface.Entities.BankAccountOwner;
using BLL.Interface.Interfaces;
using DependencyResolver;
using Ninject;

namespace ConsoleDemo
{
    public static class Program
    {
        private static readonly IKernel DemoResolver;

        static Program()
        {
            ResolverConfigurator.Configure(ref DemoResolver, useFake: true);
        }

        public static void Main(string[] args)
        {
            IAccountControlService service = DemoResolver.Get<IAccountControlService>();

            service.CreateNew(new AccountOwner("TestUid01", "Firstname01", "Lastname01"), AccountRank.Base);
            service.CreateNew(new AccountOwner("TestUid02", "Firstname02", "Lastname02"), AccountRank.Gold);
            service.CreateNew(new AccountOwner("TestUid03", "Firstname03", "Lastname03"), AccountRank.Silver);
            service.CreateNew(new AccountOwner("TestUid04", "Firstname04", "Lastname04"), AccountRank.Base);

            var allAccounts = service.DisplayAllAccounts;

            foreach (var acc in allAccounts)
            {
                service.DepositFunds(acc, 100);
            }

            Console.WriteLine("\n 100 was deposited to each account. \n");

            foreach (var acc in allAccounts)
            {
                Console.WriteLine(AccountInfo(acc));
            }

            Console.WriteLine("\n 10 was withdrawed from each account. \n");

            foreach (var acc in allAccounts)
            {
                service.WithdrawFunds(acc, 10);
            }

            foreach (var acc in allAccounts)
            {
                Console.WriteLine(AccountInfo(acc));
            }

            Console.WriteLine("\n End of demo. ");

            string AccountInfo(Account account)
            {
                return $"[N:{account.Number }, Owner:{account.Owner.FirstName} {account.Owner.LastName}, B: {account.Balance}, Rank: {account.Rank}]";
            }
        }
    }
}
