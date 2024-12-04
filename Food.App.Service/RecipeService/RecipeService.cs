using Food.App.Core.Entities;
using Food.App.Core.Interfaces;

namespace Food.App.Service.RecipeService;
public class RecipeService
{
    private readonly IRepository<Recipe> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RecipeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GetRepository<Recipe>();

    }
    //public async Task<ResponseViewModel<int>> GetAll()
    //{
    //    var getAll = _repository.GetAll();
    //    return new SuccessResponseViewModel<IEnumerable<UserViewModel>>(SuccessCode.UsersRetrieved, userViewModels);
    //}
}
