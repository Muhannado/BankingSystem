using BankingSystem.Managers.Contracts;
using BankingSystem.Requests;
using FluentValidation;

namespace BankingSystem.Validations;

public class DepositRequestValidator : AbstractValidator<BalanceRequest>
{
    private const int MaxDepositAmount = 10000;

    public DepositRequestValidator(IAccountManager accountManager)
    {
        RuleFor(x => x.AccountId).NotEmpty();
        RuleFor(x => x.AccountId).MustAsync(async (accountId, cancellationToken) =>
        {
            var account = await accountManager.GetAsync(accountId);
            return account != null;
        }).WithMessage("Account was not found");

        RuleFor(x => x.Amount).LessThanOrEqualTo(MaxDepositAmount);
    }
}