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
    public class CategoryServiceWithCaching : ICategoryService
    {
        private const string CacheCategoryKey = "categoriesCache";
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;

            if (!_memoryCache.TryGetValue(CacheCategoryKey, out _))
            {
                _memoryCache.Set(CacheCategoryKey, _categoryRepository.GetAll());
            }
        }

        public Task<Category> AddAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> AddRangeAsync(IEnumerable<Category> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<Category, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Category>>(CacheCategoryKey));
        }

        public Task<Category> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryWithProductsAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<Category> entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> Where(Expression<Func<Category, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}