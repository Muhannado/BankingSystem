using BankingSystem.Data;
using BankingSystem.Data.Mock;
using BankingSystem.Managers;
using BankingSystem.Managers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BankingSystem.UnitTests;

[TestFixture]
public class TestsBase
{
    protected readonly IAccountManager AccountManager;
    protected readonly IUsersManager UsersManager;

    public TestsBase()
    {
        var options = new DbContextOptionsBuilder<BankingSystemContext>()
            .UseInMemoryDatabase(databaseName: "BankingSystemMemoryDb")
            .Options;

        var context = new BankingSystemContext(options);
        context.Database.EnsureCreated();
        if (!context.Users.Any())
        {
            context.Users.AddRange(UsersMock.Users);
            context.SaveChanges();
        }

        var accountLogger = new Mock<ILogger<AccountManager>>().Object;
        var usersLogger = new Mock<ILogger<UsersManager>>().Object;

        AccountManager = new AccountManager(context, accountLogger);
        UsersManager = new UsersManager(context, usersLogger);
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        var account = await AccountManager.GetAsync(1);
        await AccountManager.DeleteAsync(account);
    }
}