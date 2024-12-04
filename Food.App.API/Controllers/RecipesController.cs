using Food.App.Core.Helpers;
using Food.App.Core.Interfaces.Services;
using Food.App.Core.ViewModels.Recipe;
using Food.App.Core.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;

namespace Food.App.API.Controllers;
[Route("api/recipes")]
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
}
