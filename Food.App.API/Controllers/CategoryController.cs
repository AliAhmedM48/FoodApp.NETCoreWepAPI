using Food.App.Core.Interfaces;
using Food.App.Core.Interfaces.Services;
using Food.App.Core.ViewModels.Categories;
using Food.App.Core.ViewModels.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Food.App.API.Controllers
{
    [Route("api/categories/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public ICategoryService service;
        public IUnitOfWork unitOfWork;

        public CategoryController(ICategoryService service, IUnitOfWork unitOfWork)
        {
            this.service = service;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]

        public ActionResult<ResponseViewModel<IEnumerable<CategoryViewModel>>> GetCategories()
        {
            var categories = service.GetCategories();

            return Ok(categories.Result);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ResponseViewModel<CategoryViewModelInclude>>> GetCategoryInclude(int id)
        {
            var categories = await service.GetCategoriesInclude(id);

            if (categories.IsSuccess)
            {
                return Ok(categories);
            }

            return NotFound(categories);
        }

        [Authorize]
        [HttpPost]

        public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryViewModel category)
        {
            var Added = await service.CreateCategory(category);
            if (Added.IsSuccess)
            {
                await unitOfWork.SaveChangesAsync();
                return Ok("Category is added successfully");
            }

            return BadRequest("Category is not added");
        }

        [Authorize]
        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteCategory(int id)
        {
            var Deleted = await service.DeleteCategory(id);

            if (Deleted.IsSuccess)
            {
                await unitOfWork.SaveChangesAsync();
                return Ok("Category is deleted successfully ");
            }

            return BadRequest("Category is not deleted");
        }
    }
}
