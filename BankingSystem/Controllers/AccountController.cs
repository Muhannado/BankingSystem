using BankingSystem.Managers.Contracts;
using BankingSystem.Requests;
using BankingSystem.Validations;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUsersManager _usersManager;
        private readonly IAccountManager _accountManager;

        public AccountController(
            IUsersManager usersManager,
            IAccountManager accountManager)
        {
            _usersManager = usersManager;
            _accountManager = accountManager;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateAccountRequest request)
        {
            var validator = new CreateAccountRequestValidator(_usersManager);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ValidationProblem(validationResult.GetModelStateDictionary());
            }

            var user = await _usersManager.GetAsync(request.UserId);
            if (user == null)
            {
                return new NotFoundObjectResult($"Could not find user with Id {request.UserId}");
            }

            var account = await _accountManager.CreateAsync(user, request.DepositAmount);
            if (account == null)
            {
                return new BadRequestObjectResult($"Failed to create account for user {request.UserId}");
            }
            
            return new OkObjectResult(account);
        }
        
        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit(BalanceRequest request)
        {
            var validator = new DepositRequestValidator(_accountManager);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ValidationProblem(validationResult.GetModelStateDictionary());
            }

            var account = await _accountManager.GetAsync(request.AccountId);
            if (account == null)
            {
                return new NotFoundObjectResult($"Could not find account with Id {request.AccountId}");
            }

            var newBalance = await _accountManager.DepositAsync(account, request.Amount);
            if (newBalance == null)
            {
                return new BadRequestObjectResult(
                    $"Failed to deposit {request.Amount}$ to Account {request.AccountId}");
            }

            return new OkObjectResult(
                $"Deposited {request.Amount}$ to Account {request.AccountId}. New Balance is {newBalance}$");
        }

        [HttpPost("Withdraw")]
        public async Task<IActionResult> Withdraw(BalanceRequest request)
        {
            var validator = new WithdrawRequestValidator(_accountManager);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ValidationProblem(validationResult.GetModelStateDictionary());
            }

            var account = await _accountManager.GetAsync(request.AccountId);
            if (account == null)
            {
                return new NotFoundObjectResult($"Could not find account with Id {request.AccountId}");
            }

            var newBalance = await _accountManager.WithdrawAsync(account, request.Amount);
            if (newBalance == null)
            {
                return new BadRequestObjectResult(
                    $"Failed to withdraw {request.Amount}$ from Account {request.AccountId}");
            }

            return new OkObjectResult(
                $"Withdrawn {request.Amount}$ from Account {request.AccountId}. New Balance is {newBalance}$");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int accountId)
        {
            var account = await _accountManager.GetAsync(accountId);
            if (account == null)
            {
                return new NotFoundObjectResult($"Could not find account {accountId}");
            }

            var deletedAccount = await _accountManager.DeleteAsync(account);
            if (deletedAccount == null)
            {
                return new BadRequestObjectResult($"Failed to delete account {accountId}");
            }

            return new OkObjectResult($"Account {accountId} was successfully deleted");
        }
    }
}