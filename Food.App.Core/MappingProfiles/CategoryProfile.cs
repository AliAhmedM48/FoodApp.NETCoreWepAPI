using AutoMapper;
using Food.App.Core.Entities;
using Food.App.Core.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.App.Core.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            CreateMap<Category,CategoryViewModelInclude>().
            ForMember(dest=> dest.Recipes, opt=> opt.MapFrom(src=> src.Recipes)).
            ForSourceMember(src=>src.CreatedBy,opts=>opts.DoNotValidate()).
            ForSourceMember(src=> src.CreatedAt,opts=>opts.DoNotValidate()).
            ForSourceMember(src=>src.IsDeleted,opts=>opts.DoNotValidate()).
            ForSourceMember(src=>src.DeletedAt,opts=>opts.DoNotValidate()).
            ForSourceMember(src=>src.DeletedBy,opts=>opts.DoNotValidate());

            CreateMap<Category, CategoryViewModel>().
            ForSourceMember(src => src.Recipes, opt => opt.DoNotValidate()).
            ForSourceMember(src => src.CreatedBy, opts => opts.DoNotValidate()).
            ForSourceMember(src => src.CreatedAt, opts => opts.DoNotValidate()).
            ForSourceMember(src => src.IsDeleted, opts => opts.DoNotValidate()).
            ForSourceMember(src => src.DeletedAt, opts => opts.DoNotValidate()).
            ForSourceMember(src => src.DeletedBy, opts => opts.DoNotValidate());

            CreateMap<CreateCategoryViewModel, Category>().
            ForMember(dest=>dest.Recipes,opts=>opts.Ignore()).
            ForMember(src => src.CreatedBy, opts => opts.Ignore()).
            ForMember(src => src.CreatedAt, opts => opts.Ignore()).
            ForMember(src => src.IsDeleted, opts => opts.Ignore()).
            ForMember(src => src.DeletedAt, opts => opts.Ignore()).
            ForMember(src => src.DeletedBy, opts => opts.Ignore());


        }
    }
}
