using Food.App.Core.Entities;
using Food.App.Core.Interfaces;
using Food.App.Core.Interfaces.Services;
using Food.App.Core.MappingProfiles;
using Food.App.Core.ViewModels;

namespace Food.App.Service;
public class UserService : IUserService
{
    private readonly IRepository<User> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GetRepository<User>();

    }
    public async Task DeleteUserByIdAsync(int id)
    {
        var isExist = await _repository.DoesEntityExistAsync(id);

        if (!isExist)
        {
            // TODO: Implement ResponseViewModel to handle error details.
            Console.WriteLine($"User with id {id} is not found.");
        }

        _repository.Delete(new User { Id = id });
        await _unitOfWork.SaveChangesAsync();
    }

    public IEnumerable<UserViewModel> GetAllUsers()
    {
        var users = _repository.GetAll();
        var userViewModels = users.ProjectTo<UserViewModel>();
        return userViewModels.ToList();
    }

    public UserDetailsViewModel GetUserDetailsById(int id)
    {
        var users = _repository.GetAll(u => u.Id == id);
        var userViewModels = users.ProjectToForFirstOrDefault<UserDetailsViewModel>();
        return userViewModels;
    }
}
