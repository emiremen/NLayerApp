using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repository;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Caching
{
    public class CategoryServiceWithCaching : ServiceWithCaching<Category, CategoryDto>, ICategoryService
    {
        private const string CacheCategoryKey = "categoriesCache";
        private readonly ICategoryRepository _categoryRepository;
        public CategoryServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IGenericRepository<Category> genericRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : base(CacheCategoryKey, mapper, memoryCache, genericRepository, unitOfWork)
        {
            _categoryRepository = categoryRepository;

            if (!_memoryCache.TryGetValue(CacheCategoryKey, out _))
            {
                _memoryCache.Set(CacheCategoryKey, _categoryRepository.GetAll());
            }
        }

        public async Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryWithProductsAsync(int categoryId)
        {
            Category category = _memoryCache.Get<IEnumerable<Category>>(CacheCategoryKey).Where(x=>x.Id == categoryId).FirstOrDefault();
           var resultCategory = base._mapper.Map<CategoryWithProductsDto>(category);
            return CustomResponseDto<CategoryWithProductsDto>.Success(200, resultCategory);
        }
    }
}