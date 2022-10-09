using BankingSystem.Data.Models;

namespace BankingSystem.Managers.Contracts;

public interface IAccountManager
{
    /// <summary>
    /// Get account
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns>Account if found, otherwise null</returns>
    Task<Account?> GetAsync(int accountId);

    /// <summary>
    /// Creates new account for user with initial deposit amount
    /// </summary>
    /// <param name="user"></param>
    /// <param name="depositAmount"></param>
    /// <returns>Created account, or null if failed to create account</returns>
    Task<Account?> CreateAsync(User user, int depositAmount);

    /// <summary>
    /// Deposits amount to account
    /// </summary>
    /// <param name="account"></param>
    /// <param name="amount"></param>
    /// <returns>New account balance, otherwise null if operation failed</returns>
    Task<float?> DepositAsync(Account account, int amount);

    /// <summary>
    /// Withdraws amount from account
    /// </summary>
    /// <param name="account"></param>
    /// <param name="amount"></param>
    /// <returns>New account balance, otherwise null if operation failed</returns>
    Task<float?> WithdrawAsync(Account account, int amount);

    /// <summary>
    /// Delete account
    /// </summary>
    /// <param name="account"></param>
    /// <returns>Deleted account, otherwise null if failed to delete account</returns>
    Task<Account?> DeleteAsync(Account account);
}