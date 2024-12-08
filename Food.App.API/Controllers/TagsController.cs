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

        [HttpPost]
        public async Task<ActionResult> Create(TagCreateViewModel viewModel)
        {
            var tag = await _tagService.Create(viewModel);
            if (tag.IsSuccess)
            {
                return Ok(tag);
            }
            return BadRequest(tag);
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int TagID)
        {
            var isDeleted = await _tagService.DeleteTag(TagID);
            if (isDeleted.IsSuccess)
            {
                return Ok(isDeleted);
            }
            return BadRequest(isDeleted);
        }

        [HttpPut]
        public async Task<ActionResult> Update(TagUpdateViewModel viewModel)
        {
            var isUpdated = await _tagService.UpdateTag(viewModel);
            if (isUpdated.IsSuccess)
            {
                return Ok(isUpdated);
            }
            return BadRequest(isUpdated);
        }
        [HttpGet("Details")]
        public async Task<ActionResult<ResponseViewModel<IEnumerable<TagDetailsViewModel>>>> GetRecipes(int tagId)
        {
            var recipesViewModel = await _tagService.GetDetails(tagId);

            //var recipesViewModel =_recipeTagService.GetDetails(tagId);
            if (recipesViewModel.IsSuccess)
            {
                return Ok(recipesViewModel);
            }
            return NotFound(recipesViewModel);

        }
    }
}
