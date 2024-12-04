using Food.App.Core.Helpers;
using Food.App.Core.ViewModels.Recipe;
using Food.App.Core.ViewModels.Response;

namespace Food.App.Core.Interfaces.Services;
public interface IRecipeService
{
    Task<ResponseViewModel<PageList<RecipeViewModel>>> GetAll(RecipeParams recipeParams);
}
