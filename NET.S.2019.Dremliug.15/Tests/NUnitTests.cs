using System.Linq;
using BLL.Exceptions;
using BLL.Interface.Entities.BankAccount;
using BLL.Interface.Entities.BankAccountOwner;
using BLL.Interface.Interfaces;
using DependencyResolver;
using Ninject;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class NUnitTests
    {
        private IKernel _testResolver;

        private IKernel TestResolver { get => _testResolver; set => _testResolver = value; }

        private IAccountControlService TestService { get; set; }

        private AccountOwner TestAccountOwner { get; set; }

        private Account TestAccount { get; set; }

        [SetUp]
        public void Setup()
        {
            ResolverConfigurator.Configure(ref _testResolver, useFake: true);
            TestService = TestResolver.Get<IAccountControlService>();
            TestAccountOwner = new AccountOwner("TestUid00", "TestFirstName00", "TestLastName00");
            TestAccount = new Account("TestAccNumber00", TestAccountOwner, 0, AccountRank.Base);
        }

        #region Deposit test
        [Test]
        public void DepositFundsTest()
        {
            TestService.DepositFunds(TestAccount, 5);

            Assert.AreEqual(5M, TestAccount.Balance);
        }

        [Test]
        public void DepositFunds_ThrowsExceptionOnNegativeAmount()
        {
            Assert.Throws<AccountControlServiceException>(() => TestService.DepositFunds(TestAccount, -5));
        }
        #endregion

        #region Withdraw test
        [Test]
        public void WithdrawFundsTest()
        {
            TestService.WithdrawFunds(TestAccount, 5);

            Assert.AreEqual(-5M, TestAccount.Balance);
        }

        [Test]
        public void WithdrawFunds_ThrowsExceptionOnNegativeAmount()
        {
            Assert.Throws<AccountControlServiceException>(() => TestService.WithdrawFunds(TestAccount, -5));
        }
        #endregion

        #region CreateNew test
        [Test]
        public void CreateNewTest()
        {
            var expected = TestAccount;

            var actual = TestService.CreateNew(TestAccountOwner, AccountRank.Base);

            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region SuspendTest
        [Test]
        public void SuspendTest()
        {
            TestService.Suspend(TestAccount);

            Assert.AreEqual(AccountStatus.Suspended, TestAccount.Status);
        }
        #endregion

        #region Close
        [Test]
        public void CloseTest()
        {
            TestService.Close(TestAccount);

            Assert.AreEqual(AccountStatus.Closed, TestAccount.Status);
        } 
        #endregion
    }
}
