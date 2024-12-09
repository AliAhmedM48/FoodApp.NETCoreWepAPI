using AutoMapper;
using Food.App.Core.Entities;
using Food.App.Core.ViewModels;
using Food.App.Core.ViewModels.Users;

namespace Food.App.Core.MappingProfiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserViewModel>();
        CreateMap<User, UserDetailsViewModel>();
        CreateMap<UserCreateViewModel, User>();
    }
}
