using Food.App.Core.Interfaces.Services;
using Food.App.Core.ViewModels;
using Food.App.Core.ViewModels.Response;
using Food.App.Core.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

namespace Food.App.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<ResponseViewModel<List<UserViewModel>>> GetAllUsers()
    {
        var responseViewModel = _userService.GetAllUsers();
        if (!responseViewModel.IsSuccess)
        {
            return BadRequest(responseViewModel);
        }
        return Ok(responseViewModel);
    }

    [HttpGet("{id}")]
    public ActionResult<ResponseViewModel<UserDetailsViewModel>> GetUserDetails([FromRoute] int id)
    {
        var responseViewModel = _userService.GetUserDetailsById(id);
        if (!responseViewModel.IsSuccess)
        {
            return NotFound(responseViewModel);
        }
        return Ok(responseViewModel);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseViewModel<bool>>> DeleteUser([FromRoute] int id)
    {
        var responseViewModel = await _userService.DeleteUserByIdAsync(id);
        if (!responseViewModel.IsSuccess)
        {
            return NotFound(responseViewModel);
        }
        return Ok(responseViewModel);
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(UserCreateViewModel viewModel)
    {
        // Call the use case to register the user
        var isCreated =  await _userService.Create(viewModel);
        if (!isCreated.IsSuccess)
        {
            return BadRequest(isCreated);
        }

        return Ok(isCreated);
    }

    

}
