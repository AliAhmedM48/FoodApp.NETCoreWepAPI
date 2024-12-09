using Food.App.Core.Helpers;
using Food.App.Core.ViewModels.Recipe;
using Food.App.Core.ViewModels.Recipe.Create;
using Food.App.Core.ViewModels.Response;

namespace Food.App.Core.Interfaces.Services;
public interface IRecipeService
{
    Task<ResponseViewModel<PageList<RecipeViewModel>>> GetAll(RecipeParams recipeParams);
    Task<ResponseViewModel<PageList<ReciptDetailsViewModel>>> GetAllForAdmin(RecipeParams recipeParams);
    ResponseViewModel<RecipeViewModel> GetById(int id);
    Task<ResponseViewModel<int>> Create(CreateRecipeViewModel model);
    Task<ResponseViewModel<int>> Update(UpdateRecipeViewModel model);
    Task<ResponseViewModel<int>> UpdateRecipeImage(UpdateReciptImageViewModel model);
    Task<ResponseViewModel<int>> Delete(int id);

}
