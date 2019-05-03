using BLL.ControlServices;
using BLL.Interface.Entities.BankAccountBonusCalculator;
using BLL.Interface.Entities.BankAccountNumberGenerator;
using BLL.Interface.Interfaces;
using DAL.Interface.Interfaces;
using DAL.Repositories;

namespace DependencyResolver.ConfigModules
{
    public class FakeModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IAccountControlService>().To<AccountControlService>().InSingletonScope();
            Bind<INumberGenerator>().ToConstructor(_ => new NumberGenerator(BLL.Fake.Data.NumberGenerator)).InSingletonScope();
            Bind<IBonusCalculator>().ToConstructor(_ => new BonusCalculator(BLL.Fake.Data.BonusCalculator)).InSingletonScope();
            Bind<IAccountRepository>().ToConstructor(_ => new DictionaryRepository(DAL.Fake.Storage.AccountBase, DAL.Fake.Storage.AccountOwnerBase)).InSingletonScope();
        }
    }
}
