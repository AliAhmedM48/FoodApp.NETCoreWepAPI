using Food.App.Core.Interfaces.Services;
using Food.App.Core.ViewModels;
using Food.App.Core.ViewModels.Authentication;
using Food.App.Core.ViewModels.Response;
using Food.App.Core.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

namespace Food.App.API.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IauthenticationService _authenticationService;
        public AuthController(IauthenticationService authenticationService)
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
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            var correctLogin = await _authenticationService.Login(loginViewModel);
            if (!correctLogin.IsSuccess)
            {
                return BadRequest(correctLogin);
            }
            return Ok(correctLogin);
        }

        [HttpPost("login-admin")]
        public async Task<ActionResult> LoginAdmin(LoginViewModel loginViewModel)
        {
            var correctLogin = await _authenticationService.Login(loginViewModel);
            if (!correctLogin.IsSuccess)
            {
                return BadRequest(correctLogin);
            }
            return Ok(correctLogin);
        }



        [HttpGet]
        public ActionResult<ResponseViewModel<List<UserViewModel>>> GetAllUsers()
        {
            var responseViewModel = _authenticationService.GetAllUsers();
            if (!responseViewModel.IsSuccess)
            {
                return BadRequest(responseViewModel);
            }
            return Ok(responseViewModel);
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseViewModel<UserDetailsViewModel>> GetUserDetails([FromRoute] int id)
        {
            var responseViewModel = _authenticationService.GetUserDetailsById(id);
            if (!responseViewModel.IsSuccess)
            {
                return NotFound(responseViewModel);
            }
            return Ok(responseViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseViewModel<bool>>> DeleteUser([FromRoute] int id)
        {
            var responseViewModel = await _authenticationService.DeleteUserByIdAsync(id);
            if (!responseViewModel.IsSuccess)
            {
                return NotFound(responseViewModel);
            }
            return Ok(responseViewModel);
        }


    }
}
