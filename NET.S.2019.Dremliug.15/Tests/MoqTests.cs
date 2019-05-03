using BLL.ControlServices;
using BLL.Exceptions;
using BLL.Interface.Entities.BankAccount;
using BLL.Interface.Entities.BankAccountOwner;
using BLL.Interface.Interfaces;
using DAL.Interface.Interfaces;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MoqTests
    {
        private IAccountControlService TestService { get; set; }

        private AccountOwner TestAccountOwner { get; set; }

        private Account TestAccount { get; set; }

        [SetUp]
        public void Setup()
        {
            TestAccountOwner = new Mock<AccountOwner>("TestUid00", "TestFirstName00", "TestLastName00").Object;

            TestAccount = new Mock<Account>("TestAccNumber00", TestAccountOwner, 0M, AccountRank.Base).Object;

            var mockRepo = new Mock<IAccountRepository>();
            var mockNumGen = new Mock<INumberGenerator>();
            mockNumGen.Setup(numGen => numGen.Generate()).Returns("TestAccNumber00");
            var mockBonusCalc = new Mock<IBonusCalculator>();

            TestService = new Mock<AccountControlService>(mockRepo.Object, mockNumGen.Object, mockBonusCalc.Object).Object;
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
            var expected = new Account("TestAccNumber00", new AccountOwner("TestUid00", "TestFirstName00", "TestLastName00"), 0M, AccountRank.Base);

            var actual = TestService.CreateNew(new AccountOwner("TestUid01", "TestFirstName01", "TestLastName01"), AccountRank.Base);

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
