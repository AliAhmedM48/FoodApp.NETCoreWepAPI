using AutoMapper;
using Food.App.Core.Entities;
using Food.App.Core.ViewModels;

namespace Food.App.Core.MappingProfiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserViewModel>();
        CreateMap<User, UserDetailsViewModel>();
    }
}
