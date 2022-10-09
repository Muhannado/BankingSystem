using BankingSystem.Data.Models;

namespace BankingSystem.Managers.Contracts;

public interface IUsersManager
{
    /// <summary>
    /// Get user
    /// </summary>
    /// <param name="id"></param>
    /// <returns>User if found, otherwise null</returns>
    Task<User?> GetAsync(int id);
   
    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<User>> GetAllAsync();
}