using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [ValidateFilterAttribute]
    public class CategoriesController : CustomBaseController
    {

        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // api/categories/GetSingleCategoryWithProducts/2
        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetSingleCategoryWithProducts(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryWithProductsAsync(categoryId));
        }
    }
}
