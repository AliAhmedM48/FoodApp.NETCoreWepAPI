using AutoMapper;
using Food.App.Core.Entities;
using Food.App.Core.ViewModels.Tags;

namespace Food.App.Core.MappingProfiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagViewModel>()
                .ForMember(dst => dst.TotalAssociatedRecipes, opt => opt.MapFrom(src => src.Tags.Count()));


            CreateMap<RecipeTag, TagDetailsViewModel>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Tag.Name))
                .ForMember(dst => dst.Recipes, opt => opt.MapFrom(src => src.Tag.Tags.Select(rt => rt.Recipe.Name).ToList()));
                
            CreateMap<TagCreateViewModel, Tag>();

            CreateMap<TagUpdateViewModel, Tag>();

        }
    }
}
