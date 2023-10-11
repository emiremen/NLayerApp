using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repository;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using NLayer.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Caching
{
    public class ProductServiceWithCaching : ServiceWithCaching<Product, ProductDto>, IProductService
    {
        private const string CacheProductKey = "productsCache";
        private readonly IProductRepository _productRepository;
        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IGenericRepository<Product> genericRepository, IProductRepository productRepository, IUnitOfWork unitOfWork) : base(CacheProductKey, mapper, memoryCache, genericRepository, unitOfWork)
        {
            _productRepository = productRepository;

            if (!_memoryCache.TryGetValue(CacheProductKey, out _))
            {
                _memoryCache.Set(CacheProductKey, _productRepository.GetProdutsWithCategoryAsync().Result);
            }
        }

        public async Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto productCreateDto)
        {
            var entity = base._mapper.Map<Product>(productCreateDto);
            await _productRepository.AddAsync(entity);
            await base._unitOfWork.CommitAsync();
            var newDto = base._mapper.Map<ProductDto>(entity);
            await base.CacheAllProducts();
            return CustomResponseDto<ProductDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProdutsWithCategory()
        {
            var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
            var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoryDto>>(products).ToList();
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsWithCategoryDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var entity = base._mapper.Map<Product>(productUpdateDto);
            _productRepository.Update(entity);
            await base._unitOfWork.CommitAsync();
            await CacheAllProducts();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}
