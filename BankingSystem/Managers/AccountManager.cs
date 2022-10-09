using BankingSystem.Data;
using BankingSystem.Data.Models;
using BankingSystem.Managers.Contracts;

namespace BankingSystem.Managers;

public class AccountManager : IAccountManager
{
    private readonly BankingSystemContext _context;
    private readonly ILogger<AccountManager> _logger; // Todo: check where logs are located

    public AccountManager(
        BankingSystemContext context,
        ILogger<AccountManager> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Account?> GetAsync(int accountId)
    {
        var account = await _context.Accounts.FindAsync(accountId);
        if (account != null)
        {
            return account;
        }

        _logger.LogWarning($"Account {accountId} was not found");
        return null;
    }

    public async Task<Account?> CreateAsync(User user, int depositAmount)
    {

        var account = new Account
        {
            Balance = depositAmount,
            User = user
        };

        try
        {
            var createdAccount = await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return createdAccount.Entity;
        }
        catch (Exception e)
        {
            _logger.LogError($"Failed to create account for user {user.Id}", e);
            return null;
        }
    }

    public async Task<float?> DepositAsync(Account account, int amount)
    {
        account.Balance += amount;
        try
        {
            var updatedAccount = await UpdateAccountAsync(account);
            return updatedAccount.Balance;
        }
        catch (Exception e)
        {
            _logger.LogError($"Failed to deposit {amount}$ to account {account.Id}", e);
            return null;
        }
    }

    public async Task<float?> WithdrawAsync(Account account, int amount)
    {
        account.Balance -= amount;
        try
        {
            var updatedAccount = await UpdateAccountAsync(account);
            return updatedAccount.Balance;
        }
        catch (Exception e)
        {
            _logger.LogError($"Failed to withdraw {amount}$ from account {account.Id}", e);
            return null;
        }
    }

    public async Task<Account?> DeleteAsync(Account account)
    {
        try
        {
            var deletedAccount = _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return deletedAccount.Entity;
        }
        catch (Exception e)
        {
            _logger.LogError($"Failed to delete account {account.Id}", e);
            return null;
        }
    }

    private async Task<Account> UpdateAccountAsync(Account account)
    {
        var updatedAccount = _context.Accounts.Update(account);
        await _context.SaveChangesAsync();

        return updatedAccount.Entity;
    }
}