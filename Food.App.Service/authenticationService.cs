using AutoMapper.QueryableExtensions;
using Food.App.Core.Entities;
using Food.App.Core.Enums;
using Food.App.Core.Interfaces;
using Food.App.Core.Interfaces.Services;
using Food.App.Core.MappingProfiles;
using Food.App.Core.ViewModels;
using Food.App.Core.ViewModels.Authentication;
using Food.App.Core.ViewModels.Response;
using Food.App.Core.ViewModels.Users;
using Food.App.Service.PasswordHasherServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Food.App.Service;
public class AuthenticationService : IAuthenticationService
{
    private readonly IRepository<User> _repository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly JWT _jwt;

    public AuthenticationService(IUnitOfWork unitOfWork, IOptions<JWT> jwt)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GetRepository<User>();
        _jwt = jwt.Value;

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
    public async Task<ResponseViewModel<AuthModel>> RegisterAsync(UserCreateViewModel viewModel)
    {
        var authModel = new AuthModel();

        var isRepatedUserName = await _unitOfWork.GetRepository<User>().AnyAsync(e => e.Username == viewModel.Username);
        if (isRepatedUserName)
        {
            return new FailureResponseViewModel<AuthModel>(ErrorCode.UserNameExist);
        }

        var isRepatedEmail = await _unitOfWork.GetRepository<User>().AnyAsync(e => e.Email == viewModel.Email);
        if (isRepatedEmail)
        {
            return new FailureResponseViewModel<AuthModel>(ErrorCode.UserEmailExist);
        }

        var user = viewModel.Map<User>();
        user.Password = PasswordHasherService.HashPassord(user.Password);
        user.Role = Role.User;

        await _unitOfWork.GetRepository<User>().AddAsync(user);
        var isSaved = await _unitOfWork.SaveChangesAsync() > 0;
        if (isSaved)
        {
            var UserRole = user.Role;

            var jwtSecurityToken = CreateJwtToken(user, UserRole);
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.IsAuthenticated = true;
            authModel.Email = user.Email;
            authModel.UserName = user.Username;

            return new SuccessResponseViewModel<AuthModel>(SuccessCode.LoginCorrectly, authModel);
        }
        return new FailureResponseViewModel<AuthModel>(ErrorCode.DataBaseError);

    }

    public async Task<ResponseViewModel<AuthModel>> LoginAsync(LoginViewModel loginViewModel)
    {
        var authModel = new AuthModel();


        var user = await _unitOfWork.GetRepository<User>().GetAll(e => e.Email == loginViewModel.Email).FirstOrDefaultAsync();

        if (user == null)
        {
            return new FailureResponseViewModel<AuthModel>(ErrorCode.UserNotFound);

        }

        var correctPassword = PasswordHasherService.ValidatePassword(loginViewModel.Password, user.Password);
        if (correctPassword)
        {
            var userRole = user.Role;

            var jwtSecurityToken = CreateJwtToken(user, userRole);
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.IsAuthenticated = true;
            authModel.Email = user.Email;
            authModel.UserName = user.Username;

            return new SuccessResponseViewModel<AuthModel>(SuccessCode.LoginCorrectly, authModel);

        }
        return new FailureResponseViewModel<AuthModel>(ErrorCode.IncorrectPassword);
    }

    private JwtSecurityToken CreateJwtToken(User User, Role role)
    {

        var roleClaims = new List<Claim>();

        var claims = new[]
        {
               new Claim(JwtRegisteredClaimNames.GivenName,$"{ User.Username} "),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(ClaimTypes.NameIdentifier,$"{ User.Id} "),
               new Claim(ClaimTypes.Role, ((int)role).ToString()),


        };

        var symtricSecruityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symtricSecruityKey, SecurityAlgorithms.HmacSha256);

        var symtricSecruityToken = new JwtSecurityToken
        (
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(_jwt.DurationInDays),
            signingCredentials: signingCredentials);

        return symtricSecruityToken;
    }
}
