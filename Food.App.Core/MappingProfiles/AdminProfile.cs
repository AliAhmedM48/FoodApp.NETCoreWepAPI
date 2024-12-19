using AutoMapper;
using Food.App.Core.Entities;
using Food.App.Core.ViewModels.Authentication;

namespace Food.App.Core.MappingProfiles
{
    internal class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<AdminCreateViewModel, User>();
        }
    }
}
