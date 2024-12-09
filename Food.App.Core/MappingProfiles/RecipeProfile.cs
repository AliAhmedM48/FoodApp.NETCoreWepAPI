using AutoMapper;
using Food.App.Core.Entities;
using Food.App.Core.ViewModels.Recipe;

namespace Food.App.Core.MappingProfiles;
public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<Recipe, RecipeViewModel>().ReverseMap();
        CreateMap<Recipe, ReciptDetailsViewModel>()
                                                .ForMember(des => des.CategoryName, opt =>
                                                 opt.MapFrom(a => a.Category.Name))
                                                .ForMember(des => des.Tags, opt => opt.MapFrom(x => x.RecipeTags.Select(x => x.Tag.Name)));

    }

}


