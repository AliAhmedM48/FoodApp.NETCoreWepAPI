using AutoMapper;
using Food.App.Core.Entities;
using Food.App.Core.ViewModels.Recipe;

namespace Food.App.Core.MappingProfiles;
public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<Recipe, RecipeViewModel>().ReverseMap();

    }

}


