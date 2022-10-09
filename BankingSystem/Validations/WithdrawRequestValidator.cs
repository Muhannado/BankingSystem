using BankingSystem.Managers.Contracts;
using BankingSystem.Requests;
using FluentValidation;

namespace BankingSystem.Validations;

public class WithdrawRequestValidator : AbstractValidator<BalanceRequest>
{
    private const int MinBalance = 100;
    private const double MaxWithdrawPercentage = 0.9;

    public WithdrawRequestValidator(IAccountManager accountManager)
    {
        RuleFor(x => x.AccountId).NotEmpty();
        RuleFor(x => x.AccountId).MustAsync(async (accountId, cancellationToken) =>
        {
            var account = await accountManager.GetAsync(accountId);
            return account != null;
        }).WithMessage(x => $"Account {x.AccountId} was not found");

        RuleFor(x => x).MustAsync(async (request, cancellationToken) =>
        {
            var account = await accountManager.GetAsync(request.AccountId);
            var currentBalance = account?.Balance ?? 0;
            var balanceAfterWithdraw = currentBalance - request.Amount;
            return balanceAfterWithdraw >= MinBalance;
        }).WithMessage(x => $"Account balance cannot be less than {MinBalance}");

        RuleFor(x => x).MustAsync(async (request, cancellationToken) =>
        {
            var account = await accountManager.GetAsync(request.AccountId);
            var currentBalance = account?.Balance ?? 0;
            return request.Amount / currentBalance < MaxWithdrawPercentage;
        }).WithMessage(x => $"Cannot withdraw more than {MaxWithdrawPercentage*100}% of account balance");
    }
}