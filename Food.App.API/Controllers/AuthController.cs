using Food.App.Core.Interfaces.Services;
using Food.App.Core.ViewModels;
using Food.App.Core.ViewModels.Authentication;
using Food.App.Core.ViewModels.Response;
using Food.App.Core.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Food.App.API.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register(UserCreateViewModel viewModel)
        {
            var isCreated = await _authenticationService.RegisterAsync(viewModel);
            if (!isCreated.IsSuccess)
            {
                return BadRequest(isCreated);
            }

            return Ok(isCreated);
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            var correctLogin = await _authenticationService.LoginAsync(loginViewModel);
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
        [HttpPut("reset-password"), Authorize]
        public async Task<ActionResult<ResponseViewModel<bool>>> ResetPassword([FromBody] ResetPasswordViewModel viewModel)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email).Value;
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                return BadRequest();

            }
            viewModel.Email = userEmail;
            var responseViewModel = await _authenticationService.ResetPassword(viewModel);
            if (!responseViewModel.IsSuccess)
            {
                return NotFound(responseViewModel);
            }
            return Ok(responseViewModel);
        }
        [HttpPut("forget-password")]
        public async Task<ActionResult<ResponseViewModel<bool>>> ForgetPassowrd(string email)
        {

            var responseViewModel = await _authenticationService.ForgetPassword(email);
            if (!responseViewModel.IsSuccess)
            {
                return NotFound(responseViewModel);
            }
            return Ok(responseViewModel);
        }
        [HttpPut("verify-code")]
        public async Task<ActionResult<ResponseViewModel<bool>>> VerifyResetCode(VerifyCodeViewModel model)
        {

            var responseViewModel = await _authenticationService.VerifyResetCode(model);
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
