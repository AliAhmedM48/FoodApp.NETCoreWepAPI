using Food.App.Core.Enums;
using Food.App.Core.Helpers;
using Food.App.Core.Interfaces.Services;
using Food.App.Core.ViewModels.Recipe;
using Food.App.Core.ViewModels.Recipe.Create;
using Food.App.Core.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;

namespace Food.App.API.Controllers;
[Route("api/recipes/")]
[ApiController]
public class RecipesController : ControllerBase
{
    private readonly IRecipeService _recipeService;

    public RecipesController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }
    [HttpGet]
    public async Task<ActionResult<ResponseViewModel<PageList<RecipeViewModel>>>> GetAllRecipes([FromQuery] RecipeParams recipeParams)
    {
        var result = await _recipeService.GetAll(recipeParams);
        return Ok(result);
    }
    [HttpGet("{id}")]
    public ActionResult<ResponseViewModel<RecipeViewModel>> GetById(int id)
    {
        var result = _recipeService.GetById(id);
        return Ok(result);
    }
    [HttpPost]
    public async Task<ActionResult> Create(UpdateRecipeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);

        }
        var result = await _recipeService.Create(model);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        if (result.ErrorCode != ErrorCode.DataBaseError)
        {
            return BadRequest(result);
        }
        return StatusCode((int)ErrorCode.DataBaseError, result.Message);

    }
}
