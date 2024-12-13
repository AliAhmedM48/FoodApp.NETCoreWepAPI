using Food.App.Core.Enums;
using Food.App.Core.ViewModels;
using Food.App.Core.ViewModels.Authentication;
using Food.App.Core.ViewModels.Recipe.Create;
using Food.App.Core.ViewModels.Response;
using Food.App.Core.ViewModels.Users;

namespace Food.App.Core.Interfaces.Services;
public interface IauthenticationService
{
    ResponseViewModel<IEnumerable<UserViewModel>> GetAllUsers();
    ResponseViewModel<UserDetailsViewModel> GetUserDetailsById(int id);
    Task<ResponseViewModel<bool>> DeleteUserByIdAsync(int id);
    //TODO: Implement method GetAllFavoriteRecipesByUserId
    //IEnumerable<RecipeViewModel> GetAllFavoriteRecipesByUserId(int id);

    Task<ResponseViewModel<int>> CreateUser(UserCreateViewModel viewModel);
    Task<ResponseViewModel<int>> CreateAdmin(AdminCreateViewModel viewModel);
    Task<ResponseViewModel<bool>> Login(LoginViewModel loginViewModel,Role role);


}
