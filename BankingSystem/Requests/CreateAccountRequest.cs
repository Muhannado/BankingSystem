namespace BankingSystem.Requests;

public class CreateAccountRequest
{
    public int UserId { get; set; }

    public int DepositAmount { get; set; }
}