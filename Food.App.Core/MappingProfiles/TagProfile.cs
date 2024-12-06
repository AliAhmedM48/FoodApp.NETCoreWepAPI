using AutoMapper;
using Food.App.Core.Entities;
using Food.App.Core.ViewModels.Tags;

namespace Food.App.Core.MappingProfiles
{
    internal class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag,TagViewModel>()
                .ForMember(dst=>dst.TotalAssociatedRecipes , opt=>opt.MapFrom(src=>src.Tags.Count()));
        }
    }
}
