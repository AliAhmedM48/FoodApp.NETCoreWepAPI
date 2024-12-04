using Food.App.Core.Entities;
using Food.App.Core.Enums;
using Food.App.Core.Interfaces;
using Food.App.Core.Interfaces.Services;
using Food.App.Core.MappingProfiles;
using Food.App.Core.ViewModels;
using Food.App.Core.ViewModels.Response;

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
    public async Task<ResponseViewModel<bool>> DeleteUserByIdAsync(int id)
    {
        var isExist = await _repository.DoesEntityExistAsync(id);

        if (!isExist)
        {
            return new FailureResponseViewModel<bool>(ErrorCode.UserNotFound);
        }

        _repository.Delete(new User { Id = id });
        await _unitOfWork.SaveChangesAsync();
        return new SuccessResponseViewModel<bool>(SuccessCode.UserDeleted);
    }

    public ResponseViewModel<IEnumerable<UserViewModel>> GetAllUsers()
    {
        var users = _repository.GetAll();
        var userViewModels = users.ProjectTo<UserViewModel>().ToList();
        return new SuccessResponseViewModel<IEnumerable<UserViewModel>>(SuccessCode.UsersRetrieved, userViewModels);
    }

    public ResponseViewModel<UserDetailsViewModel> GetUserDetailsById(int id)
    {
        var users = _repository.GetAll(u => u.Id == id);
        var userDetailsViewModel = users.ProjectToForFirstOrDefault<UserDetailsViewModel>();

        if (userDetailsViewModel == null)
        {
            return new FailureResponseViewModel<UserDetailsViewModel>(ErrorCode.UserNotFound);

        }

        return new SuccessResponseViewModel<UserDetailsViewModel>(SuccessCode.UserDetailsRetrieved, userDetailsViewModel);
    }
}
