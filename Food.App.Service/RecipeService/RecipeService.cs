using AutoMapper.QueryableExtensions;
using Food.App.Core.Entities;
using Food.App.Core.Enums;
using Food.App.Core.FileSetting;
using Food.App.Core.Helpers;
using Food.App.Core.Interfaces;
using Food.App.Core.Interfaces.Services;
using Food.App.Core.MappingProfiles;
using Food.App.Core.ViewModels.Recipe;
using Food.App.Core.ViewModels.Recipe.Create;
using Food.App.Core.ViewModels.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Food.App.Service.RecipeService;
public class RecipeService : IRecipeService
{
    private readonly IRepository<Recipe> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public RecipeService(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GetRepository<Recipe>();
        _imageService = imageService;
    }




    public async Task<ResponseViewModel<PageList<RecipeViewModel>>> GetAll(RecipeParams recipeParams)
    {
        var query = _repository.AsQuerable();
        if (!recipeParams.RecipeName.IsNullOrEmpty())
        {
            query = query.Where(r => r.Name.Contains(recipeParams.RecipeName));
        }
        if (recipeParams.CategoryId > 0)
        {
            query = query.Where(r => r.CategoryId == recipeParams.CategoryId);
        }
        if (recipeParams.TagId > 0)
        {
            query = query.Where(r => r.RecipeTags.Any(x => x.TagId == recipeParams.TagId));
        }
        if (recipeParams.RecipePrice > 0)
        {
            query = query.Where(r => r.Price == recipeParams.RecipePrice);
        }

        var result = query.ProjectTo<RecipeViewModel>();
        var paginatedResult = await PageList<RecipeViewModel>.CreateAsync(result, recipeParams.PageNumber, recipeParams.PageSize);
        foreach (var item in paginatedResult)
        {
            item.ImagePath = $"{FileSettings.BaseImageUrl}{FileSettings.RecipeImageFolder}/{item.ImagePath}";
        }
        return new SuccessResponseViewModel<PageList<RecipeViewModel>>(SuccessCode.RecipesRetrieved, paginatedResult);
    }
    public async Task<ResponseViewModel<PageList<ReciptDetailsViewModel>>> GetAllForAdmin(RecipeParams recipeParams)
    {
        var query = _repository.AsQuerable();
        if (!recipeParams.RecipeName.IsNullOrEmpty())
        {
            query = query.Where(r => r.Name.Contains(recipeParams.RecipeName));
        }
        if (recipeParams.CategoryId > 0)
        {
            query = query.Where(r => r.CategoryId == recipeParams.CategoryId);
        }
        if (recipeParams.TagId > 0)
        {
            query = query.Where(r => r.RecipeTags.Any(x => x.TagId == recipeParams.TagId));
        }
        if (recipeParams.RecipePrice > 0)
        {
            query = query.Where(r => r.Price == recipeParams.RecipePrice);
        }

        var result = query.ProjectTo<ReciptDetailsViewModel>();
        var paginatedResult = await PageList<ReciptDetailsViewModel>.CreateAsync(result, recipeParams.PageNumber, recipeParams.PageSize);
        foreach (var item in paginatedResult)
        {
            item.ImagePath = $"{FileSettings.BaseImageUrl}{FileSettings.RecipeImageFolder}/{item.ImagePath}";
        }
        return new SuccessResponseViewModel<PageList<ReciptDetailsViewModel>>(SuccessCode.RecipesRetrieved, paginatedResult);
    }

    public ResponseViewModel<RecipeViewModel> GetById(int id)
    {
        var query = _repository.GetAll(u => u.Id == id);
        var recipeViewModel = query.ProjectToForFirstOrDefault<RecipeViewModel>();

        if (recipeViewModel is null)
        {
            return new FailureResponseViewModel<RecipeViewModel>(ErrorCode.RecipeNotFound);
        }
        recipeViewModel.ImagePath = $"{FileSettings.BaseImageUrl}{FileSettings.RecipeImageFolder}/{recipeViewModel.ImagePath}";
        return new SuccessResponseViewModel<RecipeViewModel>(SuccessCode.RecipesRetrieved, recipeViewModel);

    }
    public async Task<ResponseViewModel<int>> Create(CreateRecipeViewModel model)
    {
        var isRecipeExist = await _repository.AnyAsync(x => x.Name == model.Name);
        if (isRecipeExist)
        {
            return new FailureResponseViewModel<int>(ErrorCode.RecipeAlreadyExist);
        }
        var getImagePath = await _imageService.UploadImage(model.ImageFile, FileSettings.RecipeImageFolder);
        var receipe = new Recipe
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            ImagePath = getImagePath,
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
    public async Task<ResponseViewModel<int>> Update(UpdateRecipeViewModel model)
    {

        var isReciptExist = await _repository.DoesEntityExistAsync(model.RecipeId);
        if (isReciptExist)
        {
            var receipt = new Recipe
            {
                Id = model.RecipeId,
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId
            };

            _repository.SaveInclude(receipt, x => x.Name, x => x.Description, x => x.CategoryId);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResponseViewModel<int>(SuccessCode.RecipeUpdated, data: model.RecipeId);

        }
        else
        {
            return new FailureResponseViewModel<int>(ErrorCode.DataBaseError);
        }
    }
    public async Task<ResponseViewModel<int>> UpdateRecipeImage(UpdateReciptImageViewModel model)
    {
        var isReciptExist = await _repository.DoesEntityExistAsync(model.Id);
        if (isReciptExist)
        {
            var result = await _unitOfWork.GetRepository<Recipe>().GetByIdAsync(model.Id);
            var getImagePath = await _imageService.UploadImage(model.ImageFile, FileSettings.RecipeImageFolder);
            _imageService.DeleteOlderImage(result.ImagePath, FileSettings.RecipeImageFolder);
            var receipt = new Recipe
            {
                Id = model.Id,
                ImagePath = getImagePath,
            };

            _repository.SaveInclude(receipt, x => x.ImagePath);
            await _unitOfWork.SaveChangesAsync();
        }
        return new SuccessResponseViewModel<int>(SuccessCode.RecipeUpdated, data: model.Id);

    }
    public async Task<ResponseViewModel<int>> Delete(int id)
    {
        var isRecipeExist = await _unitOfWork.GetRepository<Recipe>()
                                             .AnyAsync(x => x.Id == id && !x.IsDeleted);
        if (isRecipeExist)
        {
            var recipe = new Recipe
            {
                Id = id,
                IsDeleted = true,
            };

            _unitOfWork.GetRepository<Recipe>().SaveInclude(recipe, x => x.IsDeleted);

            var saveResult = await _unitOfWork.SaveChangesAsync() > 0;
            var isRecipeHasTag = await _unitOfWork.GetRepository<RecipeTag>()
                                                  .AnyAsync(x => x.RecipeId == id);
            bool recipeTagsUpdated = false;
            if (isRecipeHasTag)
            {
                recipeTagsUpdated = _unitOfWork.GetRepository<RecipeTag>()
                           .GetAll()
                           .Where(x => x.RecipeId == id)
                           .ExecuteUpdate(s => s.SetProperty(b => b.IsDeleted, true)) > 0;

            }
            if (saveResult && isRecipeHasTag && recipeTagsUpdated || saveResult && !isRecipeHasTag)
            {
                return new SuccessResponseViewModel<int>(SuccessCode.RecipeDeleted);
            }
            else
            {
                return new FailureResponseViewModel<int>(ErrorCode.DataBaseError);

            }
        }
        else
        {
            return new FailureResponseViewModel<int>(ErrorCode.RecipeNotFound);

        }
    }

}

