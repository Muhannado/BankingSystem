using BankingSystem.Data.Models;

namespace BankingSystem.Data.Mock;

public static class UsersMock
{
    public static IEnumerable<User> Users = new List<User>
    {
        new() { FirstName = "John", LastName = "Miller" },
        new() { FirstName = "Bob", LastName = "Smith" },
        new() { FirstName = "Carina", LastName = "Wolf" }
    };
}