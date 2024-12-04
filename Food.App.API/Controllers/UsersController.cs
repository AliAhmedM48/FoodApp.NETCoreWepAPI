using Food.App.Core.Interfaces.Services;
using Food.App.Core.ViewModels;
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
    public ActionResult<List<UserViewModel>> GetAllUsers()
    {
        var userViewModels = _userService.GetAllUsers().ToList();
        return Ok(userViewModels);
    }

    [HttpGet("{id}")]
    public ActionResult<UserDetailsViewModel> GetAllUsers([FromRoute] int id)
    {
        var userDetailsViewModel = _userService.GetUserDetailsById(id);
        return Ok(userDetailsViewModel);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        await _userService.DeleteUserByIdAsync(id);
        return Ok();
    }

}
