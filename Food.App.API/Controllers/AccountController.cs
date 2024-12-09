using Food.App.Core.Interfaces.Services;
using Food.App.Core.ViewModels.Admin;
using Food.App.Core.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

namespace Food.App.API.Controllers
{
    [Route("api/account/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IauthenticationService _authenticationService;
        public AccountController(IauthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("register-admin")]
        public async Task<ActionResult> RegisterAdmin(AdminCreateViewModel viewModel)
        {
            var isCreated = await _authenticationService.CreateAdmin(viewModel);
            if (!isCreated.IsSuccess)
            {
                return BadRequest(isCreated);
            }

            return Ok(isCreated);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserCreateViewModel viewModel)
        {
            var isCreated = await _authenticationService.CreateUser(viewModel);
            if (!isCreated.IsSuccess)
            {
                return BadRequest(isCreated);
            }

            return Ok(isCreated);
        }
    }
}
