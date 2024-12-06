using Food.App.Core.Helpers;
using Food.App.Core.ViewModels.Recipe;
using Food.App.Core.ViewModels.Recipe.Create;
using Food.App.Core.ViewModels.Response;

namespace Food.App.Core.Interfaces.Services;
public interface IRecipeService
{
    Task<ResponseViewModel<PageList<RecipeViewModel>>> GetAll(RecipeParams recipeParams);
    ResponseViewModel<RecipeViewModel> GetById(int id);
    Task<ResponseViewModel<int>> Create(UpdateRecipeViewModel model);
    Task<ResponseViewModel<PageList<RecipeViewModel>>> Delete(int id);


}
