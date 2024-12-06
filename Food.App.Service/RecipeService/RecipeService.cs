using AutoMapper.QueryableExtensions;
using Food.App.Core.Entities;
using Food.App.Core.Enums;
using Food.App.Core.Helpers;
using Food.App.Core.Interfaces;
using Food.App.Core.Interfaces.Services;
using Food.App.Core.MappingProfiles;
using Food.App.Core.ViewModels.Recipe;
using Food.App.Core.ViewModels.Recipe.Create;
using Food.App.Core.ViewModels.Response;

namespace Food.App.Service.RecipeService;
public class RecipeService : IRecipeService
{
    private readonly IRepository<Recipe> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RecipeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GetRepository<Recipe>();

    }



    public Task<ResponseViewModel<PageList<RecipeViewModel>>> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseViewModel<PageList<RecipeViewModel>>> GetAll(RecipeParams recipeParams)
    {
        var query = _repository.GetAll()
                               .ProjectTo<RecipeViewModel>();
        var paginatedResult = await PageList<RecipeViewModel>.CreateAsync(query, recipeParams.PageNumber, recipeParams.PageSize);

        return new SuccessResponseViewModel<PageList<RecipeViewModel>>(SuccessCode.RecipesRetrieved, paginatedResult);
    }

    public ResponseViewModel<RecipeViewModel> GetById(int id)
    {
        var query = _repository.GetAll(u => u.Id == id);
        var recipeViewModel = query.ProjectToForFirstOrDefault<RecipeViewModel>();
        if (recipeViewModel is null)
        {
            return new FailureResponseViewModel<RecipeViewModel>(ErrorCode.RecipeNotFound);
        }
        return new SuccessResponseViewModel<RecipeViewModel>(SuccessCode.RecipesRetrieved, recipeViewModel);

    }
    public async Task<ResponseViewModel<int>> Create(UpdateRecipeViewModel model)
    {
        var isRecipeExist = await _repository.AnyAsync(x => x.Name == model.Name);
        if (isRecipeExist)
        {
            return new FailureResponseViewModel<int>(ErrorCode.RecipeAlreadyExist);
        }
        var receipe = new Recipe
        {
            Name = model.Name,
            Description = model.Description,
            ImagePath = model.ImagePath,
            CreatedAt = DateTime.UtcNow,
            CategoryId = model.CategoryId,
            RecipeTags = model.TagIds.Select(x => new RecipeTag
            {
                TagId = x
            }).ToList(),
        };
        await _repository.AddAsync(receipe);
        var result = await _unitOfWork.SaveChangesAsync() > 0;
        return result ? new SuccessResponseViewModel<int>(SuccessCode.RecipeCreated, data: receipe.Id)
                       : new FailureResponseViewModel<int>(ErrorCode.DataBaseError);

    }

}
