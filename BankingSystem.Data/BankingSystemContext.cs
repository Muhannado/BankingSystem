using Microsoft.EntityFrameworkCore;
using BankingSystem.Data.Models;

namespace BankingSystem.Data;

public class BankingSystemContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }

    public BankingSystemContext(DbContextOptions<BankingSystemContext> options)
        : base(options)
    {
    }
}