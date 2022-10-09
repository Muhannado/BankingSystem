using BankingSystem.Validations;
using NUnit.Framework;
using BankingSystem.Requests;
using FluentValidation.TestHelper;

namespace BankingSystem.UnitTests;

public class AccountValidatorTests : TestsBase
{
    private CreateAccountRequestValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new CreateAccountRequestValidator(UsersManager);
    }

    [TestCase(1, 100)]
    public async Task CreateAccountSuccessTest(int userId, int depositAmount)
    {
        var model = new CreateAccountRequest
        {
            UserId = userId,
            DepositAmount = depositAmount
        };

        var result = await _validator.TestValidateAsync(model);
        Assert.IsTrue(result.IsValid);
    }

    [TestCase(0, 50)]
    public async Task CreateAccount_UserNotFound_FailTest(int userId, int depositAmount)
    {
        var model = new CreateAccountRequest
        {
            UserId = userId,
            DepositAmount = depositAmount
        };

        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(request => request.UserId);
    }

    [TestCase(1, 50)]
    public async Task CreateAccount_DepositInvalid_FailTest(int userId, int depositAmount)
    {
        var model = new CreateAccountRequest
        {
            UserId = userId,
            DepositAmount = depositAmount
        };

        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(request => request.DepositAmount);
    }
}