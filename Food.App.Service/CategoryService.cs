using AutoMapper;
using AutoMapper.QueryableExtensions;
using Food.App.Core.Entities;
using Food.App.Core.Enums;
using Food.App.Core.Interfaces;
using Food.App.Core.Interfaces.Services;
using Food.App.Core.MappingProfiles;
using Food.App.Core.ViewModels.Categories;
using Food.App.Core.ViewModels.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.App.Service
{
    public class CategoryService : ICategoryService
    {
        public IUnitOfWork unitOfWork;
        public IMapper mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ResponseViewModel<IEnumerable<CategoryViewModel>>> GetCategories() 
        {
            var categories =  unitOfWork.GetRepository<Category>().GetAll();
            List<CategoryViewModel> categoriesviewmodel =await categories.ProjectTo<CategoryViewModel>().ToListAsync();

            return new SuccessResponseViewModel<IEnumerable<CategoryViewModel>>(SuccessCode.CategoriesRetrieved, categoriesviewmodel);
        }

        public async Task<ResponseViewModel<bool>> CreateCategory(CreateCategoryViewModel command) 
        {
            var repo = unitOfWork.GetRepository<Category>();
            bool categoryexist =await repo.GetAll(c=> c.Name == command.Name).AnyAsync();

            if (categoryexist) 
            {
              return  new FailureResponseViewModel<bool>(ErrorCode.CategoryWithSameNameFound);
            }

            if (Validate(command.Name)) 
            {
                return new FailureResponseViewModel<bool>(ErrorCode.ValidationError);
            }

            await repo.AddAsync(mapper.Map<Category>());

            return new SuccessResponseViewModel<bool>(SuccessCode.CategoryCreated,true);
        }

        public async Task<ResponseViewModel<CategoryViewModelInclude>> GetCategoriesInclude(int id)
        {
            var Category = await unitOfWork.GetRepository<Category>().
                GetAllWithInclude(c => c.Include(c => c.Recipes)).Where(c =>!c.IsDeleted).FirstOrDefaultAsync();
                


            if (Category == null) 
            {
                return new FailureResponseViewModel<CategoryViewModelInclude>(ErrorCode.CategoryNotFound);
            }


            return new SuccessResponseViewModel<CategoryViewModelInclude>(SuccessCode.CategoriesRetrieved, mapper.Map<CategoryViewModelInclude>());
        }

        public async Task<ResponseViewModel<bool>> DeleteCategory(int id) 
        {
           var categoryfound = await unitOfWork.GetRepository<Category>().GetByIdAsync(id);

            if (categoryfound is null) 
            {
                return new FailureResponseViewModel<bool>(ErrorCode.CategoryNotFound);
            }
            categoryfound.IsDeleted = true;
            return new SuccessResponseViewModel<bool>(SuccessCode.CategoryUpdated, true);
        }

        public bool Validate(string name) 
        {
            if (string.IsNullOrEmpty(name)|| name.Length > 50) 
            {
                return false;
            }

            foreach(char letter in name) 
            {
                if(letter == ' ') 
                {
                    continue;
                }
                if(!char.IsLetter(letter)) 
                {
                    return false;
                }
            }

            return true;
        }

    }
}
