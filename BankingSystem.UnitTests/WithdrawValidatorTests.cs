using BankingSystem.Requests;
using BankingSystem.Validations;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace BankingSystem.UnitTests;

public class WithdrawValidatorTests : TestsBase
{
    private WithdrawRequestValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new WithdrawRequestValidator(AccountManager);
    }

    [TestCase(1, 500)]
    public async Task WithdrawSuccessTest(int userId, int amount)
    {
        var user = await UsersManager.GetAsync(userId);
        Assert.IsNotNull(user);

        var account = await AccountManager.CreateAsync(user, 10000);
        Assert.IsNotNull(account);

        var model = new BalanceRequest
        {
            AccountId = account.Id,
            Amount = amount
        };

        var result = await _validator.TestValidateAsync(model);
        Assert.IsTrue(result.IsValid);
    }

    [TestCase(1, 50)]
    public async Task Withdraw_InvalidBalance_FailTest(int userId, int amount)
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
        result.ShouldHaveAnyValidationError();
    }

    [TestCase(1, 10545)]
    public async Task Withdraw_InvalidAmount_FailTest(int userId, int amount)
    {
        var user = await UsersManager.GetAsync(userId);
        Assert.IsNotNull(user);

        var account = await AccountManager.CreateAsync(user, 11100);
        Assert.IsNotNull(account);

        var model = new BalanceRequest
        {
            AccountId = account.Id,
            Amount = amount
        };

        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveAnyValidationError();
    }
}