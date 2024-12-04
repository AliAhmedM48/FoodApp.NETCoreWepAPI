using Food.App.Core.ViewModels;
using Food.App.Core.ViewModels.Response;

namespace Food.App.Core.Interfaces.Services;
public interface IUserService
{
    ResponseViewModel<IEnumerable<UserViewModel>> GetAllUsers();
    ResponseViewModel<UserDetailsViewModel> GetUserDetailsById(int id);
    Task<ResponseViewModel<bool>> DeleteUserByIdAsync(int id);
    //TODO: Implement method GetAllFavoriteRecipesByUserId
    //IEnumerable<RecipeViewModel> GetAllFavoriteRecipesByUserId(int id);
}
