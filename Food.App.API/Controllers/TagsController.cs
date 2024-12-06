using Food.App.Core.Interfaces.Services;
using Food.App.Core.ViewModels.Response;
using Food.App.Core.ViewModels.Tags;
using Microsoft.AspNetCore.Mvc;

namespace Food.App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public ActionResult<ResponseViewModel<IEnumerable<TagViewModel>>> Get()
        {
            var tagsViewModel =  _tagService.GetAllTags();
            if (tagsViewModel.IsSuccess)
            {
                return Ok(tagsViewModel);
            }
            return NotFound(tagsViewModel);
        }

    }
}
