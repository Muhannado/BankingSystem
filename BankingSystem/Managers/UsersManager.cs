using BankingSystem.Data;
using BankingSystem.Data.Models;
using BankingSystem.Managers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Managers;

public class UsersManager : IUsersManager
{
    private readonly BankingSystemContext _context;
    private readonly ILogger<UsersManager> _logger; // Todo: check where logs are located

    public UsersManager(
        BankingSystemContext context,
        ILogger<UsersManager> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<User?> GetAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            return user;
        }

        _logger.LogWarning($"User with Id {id} was not found");
        return null;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }
}