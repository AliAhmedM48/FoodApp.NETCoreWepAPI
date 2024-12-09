using Azure;
using Food.App.Core.Entities;
using Food.App.Core.Enums;
using Food.App.Core.Interfaces;
using Food.App.Core.Interfaces.Services;
using Food.App.Core.MappingProfiles;
using Food.App.Core.ViewModels;
using Food.App.Core.ViewModels.Response;
using Food.App.Core.ViewModels.Tags;
using Food.App.Core.ViewModels.Users;

namespace Food.App.Service;
public class UserService : IUserService
{
    private readonly IRepository<User> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasherService _passwordHasherService;


    public UserService(IUnitOfWork unitOfWork, IPasswordHasherService passwordHasherService)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GetRepository<User>();
        _passwordHasherService = passwordHasherService;
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

    

    public async Task<ResponseViewModel<int>> Create(UserCreateViewModel viewModel)
    {
        var isRepatedUserName = _repository.GetAll(e=>e.Username==viewModel.Username).Any();
        if (isRepatedUserName)
        {
            return new FailureResponseViewModel<int>(ErrorCode.UserNameExist);
        }

        var isRepatedEmail = _repository.GetAll(e=>e.Email==viewModel.Email).Any();
        if (isRepatedEmail)
        {
            return new FailureResponseViewModel<int>(ErrorCode.UserEmailExist);
        }

        var user = viewModel.Map<User>();
        user.Password = _passwordHasherService.HashPassord(user.Password);
        user.Role = Role.User;
        //var person = new Person
        //{
        //    Id = user.Id,
        //};
        await _repository.AddAsync(user);
        var isSaved = await _unitOfWork.SaveChangesAsync();
        if (isSaved == 0)
        {
            return new FailureResponseViewModel<int>(ErrorCode.DataBaseError);
        }
        return new SuccessResponseViewModel<int>(SuccessCode.UserCreated);


    }


    
}
