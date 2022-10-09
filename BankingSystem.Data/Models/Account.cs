using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Data.Models;

public class Account
{
    public int Id { get; set; }

    public float Balance { get; set; }
    
    [Required]
    public User User { get; set; }
}