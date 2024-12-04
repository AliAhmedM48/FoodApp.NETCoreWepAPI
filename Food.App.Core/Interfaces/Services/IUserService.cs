using Food.App.Core.ViewModels;

namespace Food.App.Core.Interfaces.Services;
public interface IUserService
{
    IEnumerable<UserViewModel> GetAllUsers();
    UserDetailsViewModel GetUserDetailsById(int id);
    Task DeleteUserByIdAsync(int id);
    //TODO: Implement method GetAllFavoriteRecipesByUserId
    //IEnumerable<RecipeViewModel> GetAllFavoriteRecipesByUserId(int id);
}
