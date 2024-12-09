using Food.App.Core.Interfaces.Services;
using Food.App.Core.ViewModels.Admin;
using Food.App.Core.ViewModels.Users;
using Food.App.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Food.App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpPost("RegisterAdmin")]
        public async Task<ActionResult> Register(AdminCreateViewModel viewModel)
        {
            var isCreated = await _adminService.Create(viewModel);
            if (!isCreated.IsSuccess)
            {
                return BadRequest(isCreated);
            }

            return Ok(isCreated);
        }
    }
}
