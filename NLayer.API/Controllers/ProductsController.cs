using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{

    [ValidateFilterAttribute]
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IMapper mapper, IService<Product, ProductDto> service, IProductService productService)
        {
            _productService = productService;
        }

        // api/produts/GetProductsWithCategory
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreateActionResult(await _productService.GetProdutsWithCategory());
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return CreateActionResult(await _productService.GetAllAsync());
        }

        [ServiceFilter(typeof(NotFoundFilter<Product,ProductDto>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _productService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto productCreateDto)
        {
            return CreateActionResult(await _productService.AddAsync(productCreateDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            return CreateActionResult(await _productService.UpdateAsync(productUpdateDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product,ProductDto>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _productService.RemoveAsync(id));
        }

        [HttpPost("SaveRange")]
        public async Task<IActionResult> SaveRange(List<ProductDto> productCreateDtos)
        {
            return CreateActionResult(await _productService.AddRangeAsync(productCreateDtos));
        }

        [HttpPost("RemoveRange")]
        public async Task<IActionResult> RemoveAll(List<int> productIds)
        {
            return CreateActionResult(await _productService.RemoveRangeAsync(productIds));
        }

        [HttpPost("Any/{id}")]
        public async Task<IActionResult> Any(int id)
        {
            return CreateActionResult(await _productService.AnyAsync(x=>x.Id == id));
        }
    }
}
