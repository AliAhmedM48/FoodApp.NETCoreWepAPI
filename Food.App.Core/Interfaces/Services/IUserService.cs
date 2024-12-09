using Food.App.Core.ViewModels;
using Food.App.Core.ViewModels.Recipe.Create;
using Food.App.Core.ViewModels.Response;
using Food.App.Core.ViewModels.Users;

namespace Food.App.Core.Interfaces.Services;
public interface IUserService
{
    ResponseViewModel<IEnumerable<UserViewModel>> GetAllUsers();
    ResponseViewModel<UserDetailsViewModel> GetUserDetailsById(int id);
    Task<ResponseViewModel<bool>> DeleteUserByIdAsync(int id);
    //TODO: Implement method GetAllFavoriteRecipesByUserId
    //IEnumerable<RecipeViewModel> GetAllFavoriteRecipesByUserId(int id);

    Task<ResponseViewModel<int>> Create(UserCreateViewModel viewModel);

}
