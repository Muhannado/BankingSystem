using BankingSystem.Managers.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersManager _usersManager;

        public UsersController(IUsersManager usersManager)
        {
            _usersManager = usersManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return new OkObjectResult(await _usersManager.GetAllAsync());
        }
    }
}
