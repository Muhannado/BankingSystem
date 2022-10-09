using NUnit.Framework;

namespace BankingSystem.UnitTests
{
    public class AccountManagerTests : TestsBase
    {
        [Order(1)]
        [TestCase(1, 100)]
        public async Task CreateAccountTest(int userId, int depositAmount)
        {
            var user = await UsersManager.GetAsync(userId);
            Assert.IsNotNull(user);

            var account = await AccountManager.CreateAsync(user, depositAmount);

            Assert.IsNotNull(account);
            Assert.AreEqual(account.User, user);
            Assert.AreEqual(account.Balance, depositAmount);
        }

        [Order(2)]
        [TestCase(1, 10000)]
        public async Task DepositTest(int accountId, int depositAmount)
        {
            var account = await AccountManager.GetAsync(accountId);
            Assert.IsNotNull(account);

            var oldBalance = account.Balance;
            var newBalance = await AccountManager.DepositAsync(account, depositAmount);
            Assert.AreEqual(oldBalance + depositAmount, newBalance);
        }

        [Order(3)]
        [TestCase(1, 10000)]
        public async Task WithdrawTest(int accountId, int depositAmount)
        {
            var account = await AccountManager.GetAsync(accountId);
            Assert.IsNotNull(account);
            
            var oldBalance = account.Balance;
            var newBalance = await AccountManager.WithdrawAsync(account, depositAmount);
            Assert.AreEqual(oldBalance - depositAmount, newBalance);
        }

        [Order(4)]
        [TestCase(1)]
        public async Task DeleteTest(int accountId)
        {
            var account = await AccountManager.GetAsync(accountId);
            Assert.IsNotNull(account);
            
            var deletedAccount = await AccountManager.DeleteAsync(account);
            Assert.IsNotNull(deletedAccount);
        }
    }
}