using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Data.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
}