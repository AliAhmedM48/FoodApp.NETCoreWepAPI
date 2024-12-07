using Food.App.Core.ViewModels.Categories;
using Food.App.Core.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.App.Core.Interfaces.Services
{
    public interface ICategoryService
    {
        public Task<ResponseViewModel<IEnumerable<CategoryViewModel>>> GetCategories();

        public Task<ResponseViewModel<bool>> CreateCategory(CreateCategoryViewModel command);

        public Task<ResponseViewModel<CategoryViewModelInclude>> GetCategoriesInclude(int id);

        public Task<ResponseViewModel<bool>> DeleteCategory(int id);
    }
}
