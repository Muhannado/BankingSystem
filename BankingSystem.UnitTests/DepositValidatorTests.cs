using BankingSystem.Requests;
using BankingSystem.Validations;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace BankingSystem.UnitTests;

public class DepositValidatorTests : TestsBase
{
    private DepositRequestValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new DepositRequestValidator(AccountManager);
    }

    [TestCase(1, 10000)]
    public async Task DepositSuccessTest(int userId, int amount)
    {
        var user = await UsersManager.GetAsync(userId);
        Assert.IsNotNull(user);

        var account = await AccountManager.CreateAsync(user, 100);
        Assert.IsNotNull(account);

        var model = new BalanceRequest
        {
            AccountId = account.Id,
            Amount = amount
        };

        var result = await _validator.TestValidateAsync(model);
        Assert.IsTrue(result.IsValid);
    }

    [TestCase(1, 10001)]
    public async Task Deposit_InvalidAmount_FailTest(int userId, int amount)
    {
        var user = await UsersManager.GetAsync(userId);
        Assert.IsNotNull(user);

        var account = await AccountManager.CreateAsync(user, 100);
        Assert.IsNotNull(account);

        var model = new BalanceRequest
        {
            AccountId = account.Id,
            Amount = amount
        };

        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(r => r.Amount);
    }

    [Test]
    public async Task Deposit_InvalidAccount_FailTest()
    {
        var model = new BalanceRequest
        {
            AccountId = 0,
            Amount = 100
        };

        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(r => r.AccountId);
    }
}