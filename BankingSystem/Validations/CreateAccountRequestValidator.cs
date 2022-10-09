using BankingSystem.Managers.Contracts;
using BankingSystem.Requests;
using FluentValidation;

namespace BankingSystem.Validations;

public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
{
    private const int MinBalance = 100;

    public CreateAccountRequestValidator(IUsersManager usersManager)
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.UserId).MustAsync(async (userId, cancellationToken) =>
        {
            var user = await usersManager.GetAsync(userId);
            return user != null;
        }).WithMessage(x => $"User with Id {x.UserId} was not found");

        RuleFor(x => x.DepositAmount).GreaterThanOrEqualTo(MinBalance);
    }
}