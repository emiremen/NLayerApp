using AutoMapper;
using Microsoft.AspNetCore.Http;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repository;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class ProductServiceWithNoCaching : Service<Product, ProductDto>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductServiceWithNoCaching(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productService, IMapper mapper) : base(repository, unitOfWork)
        {
            _productRepository = productService;
        }

        public async Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto productCreateDto)
        {
            var entity = base._mapper.Map<Product>(productCreateDto);
            await _productRepository.AddAsync(entity);
            await base._unitOfWork.CommitAsync();
            var newDto = base._mapper.Map<ProductDto>(entity);
            return CustomResponseDto<ProductDto>.Success(StatusCodes.Status200OK,newDto);

        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProdutsWithCategory()
        {
            var product = await _productRepository.GetProdutsWithCategoryAsync();
            var productsDto = base._mapper.Map<List<ProductWithCategoryDto>>(product);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
                var entity = base._mapper.Map<Product>(productUpdateDto);
                _productRepository.Update(entity);
                await base._unitOfWork.CommitAsync();
                return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
            
        }
    }
}
