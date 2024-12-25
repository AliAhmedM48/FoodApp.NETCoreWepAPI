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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    private readonly JWT _jwt;

    public AuthenticationService(IUnitOfWork unitOfWork, IOptions<JWT> jwt, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _jwt = jwt.Value;
        _emailService = emailService;
    }
    public async Task<ResponseViewModel<bool>> DeleteUserByIdAsync(int id)
    {
        var isExist = await _unitOfWork.GetRepository<User>().DoesEntityExistAsync(id);

        if (!isExist)
        {
            return new FailureResponseViewModel<bool>(ErrorCode.UserNotFound);
        }

        _unitOfWork.GetRepository<User>().Delete(new User { Id = id });
        await _unitOfWork.SaveChangesAsync();
        return new SuccessResponseViewModel<bool>(SuccessCode.UserDeleted);
    }

    public ResponseViewModel<IEnumerable<UserViewModel>> GetAllUsers()
    {
        var users = _unitOfWork.GetRepository<User>().GetAll();
        var userViewModels = users.ProjectTo<UserViewModel>().ToList();
        return new SuccessResponseViewModel<IEnumerable<UserViewModel>>(SuccessCode.UsersRetrieved, userViewModels);
    }

    public ResponseViewModel<UserDetailsViewModel> GetUserDetailsById(int id)
    {
        var users = _unitOfWork.GetRepository<User>().GetAll(u => u.Id == id);
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
    public async Task<ResponseViewModel<AuthModel>> ResetPassword(ResetPasswordViewModel viewModel)
    {
        if (viewModel.NewPassword != viewModel.ConfirmPassword)
        {
            return new FailureResponseViewModel<AuthModel>(ErrorCode.IncorrectPassword);
        }
        if (viewModel.NewPassword.Length > 6)
        {
            return new FailureResponseViewModel<AuthModel>(ErrorCode.IncorrectPassword);

        }
        var userExists = await _unitOfWork.GetRepository<User>().AnyAsync(x => x.Email == viewModel.Email);
        if (userExists)
        {
            var getUser = await _unitOfWork.GetRepository<User>()
                                                .AsQuerable()
                                                .Where(a => a.Email == viewModel.Email)
                                                .FirstOrDefaultAsync();

            var doesPassCorrect = PasswordHasherService.ValidatePassword(viewModel.OldPassword, getUser!.Password);
            if (doesPassCorrect)
            {
                var newPass = PasswordHasherService.HashPassord(viewModel.NewPassword);

                var user = new User
                {
                    Id = getUser.Id,
                    Password = newPass,
                };

                _unitOfWork.GetRepository<User>().SaveInclude(user, a => a.Password);
                var result = await _unitOfWork.SaveChangesAsync() > 0;
                return result ? new SuccessResponseViewModel<AuthModel>(SuccessCode.ChangePasswordUpdated) : new FailureResponseViewModel<AuthModel>(ErrorCode.DataBaseError);

            }
            else
            {
                return new FailureResponseViewModel<AuthModel>(ErrorCode.IncorrectPassword);

            }
        }
        return new FailureResponseViewModel<AuthModel>(ErrorCode.DataBaseError);

    }
    public async Task<ResponseViewModel<int>> ForgetPassword(string email)
    {
        var userExists = await _unitOfWork.GetRepository<User>().AnyAsync(x => x.Email == email);
        if (userExists)
        {
            var getUser = await _unitOfWork.GetRepository<User>()
                                                .AsQuerable()
                                                .Where(a => a.Email == email)
                                                .FirstOrDefaultAsync();

            var resetCode = new Random().Next(100000, 999999).ToString();

            var user = new User
            {
                Id = getUser.Id,
                PasswordResetCode = resetCode,
                PasswordResetCodeExpiration = DateTime.UtcNow.AddMinutes(1),
            };

            _unitOfWork.GetRepository<User>().SaveInclude(user, a => a.PasswordResetCode, a => a.PasswordResetCodeExpiration);
            var result = await _unitOfWork.SaveChangesAsync() > 0;


            await _emailService.SendEmailAsync(user.Email, "Password Reset Code",
                                                                 $"Your password reset code is: {resetCode}. This code is valid for 1 minute.");
            var response = new SuccessResponseViewModel<int>(SuccessCode.ChangePasswordUpdated);

            response.CustomMessage = "Your password has been reset successfully please review your email ";
            return response;

        }
        return new FailureResponseViewModel<int>(ErrorCode.DataBaseError);

    }
    public async Task<ResponseViewModel<AuthModel>> VerifyResetCode(VerifyCodeViewModel model)
    {
        var getUser = await _unitOfWork.GetRepository<User>().AnyAsync(x => x.Email == model.Email);

        var resetRequest = await _unitOfWork.GetRepository<User>().AsQuerable()
                                                              .FirstOrDefaultAsync(x => x.Email == model.Email &&
                                                              x.PasswordResetCode == model.Code &&
                                                              x.PasswordResetCodeExpiration > DateTime.UtcNow);

        if (resetRequest == null)
        {
            return new FailureResponseViewModel<AuthModel>(ErrorCode.IncorrectPassword);
        }
        var newPass = PasswordHasherService.HashPassord(model.Password);

        var user = new User
        {
            Id = resetRequest.Id,
            Password = newPass,
        };

        _unitOfWork.GetRepository<User>().SaveInclude(user, a => a.Password);
        var result = await _unitOfWork.SaveChangesAsync() > 0;
        return result ? new SuccessResponseViewModel<AuthModel>(SuccessCode.ChangePasswordUpdated) : new FailureResponseViewModel<AuthModel>(ErrorCode.DataBaseError);
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
