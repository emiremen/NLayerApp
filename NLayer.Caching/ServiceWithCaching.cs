using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repository;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Repositories;
using NLayer.Service.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Caching
{
    public class ServiceWithCaching<TEntity, TDto> : IService<TEntity, TDto> where TEntity : BaseEntity where TDto : BaseDto
    {
        private readonly string CacheKey;
        protected readonly IMemoryCache _memoryCache;
        private readonly IGenericRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public ServiceWithCaching(string cacheKey, IMapper mapper, IMemoryCache memoryCache, IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            CacheKey = cacheKey;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfWork = unitOfWork;

            if (!_memoryCache.TryGetValue(CacheKey, out _))
            {
                _memoryCache.Set(CacheKey, _repository.GetAll());
            }
        }

        public async Task<CustomResponseDto<TDto>> AddAsync(TDto dto)
        {
            TEntity newEntity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            TDto newDto = _mapper.Map<TDto>(newEntity);
            await CacheAllProducts();
            return CustomResponseDto<TDto>.Success(StatusCodes.Status200OK,newDto);
        }

        public async Task<CustomResponseDto<IEnumerable<TDto>>> AddRangeAsync(IEnumerable<TDto> dtos)
        {
            IEnumerable<TEntity> newEntities = _mapper.Map<IEnumerable<TEntity>>(dtos);

            await _repository.AddRangeAsync(newEntities);
            await _unitOfWork.CommitAsync();

            var newDtos = _mapper.Map<IEnumerable<TDto>>(newEntities);
            return CustomResponseDto<IEnumerable<TDto>>.Success(StatusCodes.Status200OK,newDtos);
        }

        public async Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            bool isExist = await _repository.AnyAsync(expression);
            return CustomResponseDto<bool>.Success(StatusCodes.Status200OK, isExist);
        }

        public async Task<CustomResponseDto<IEnumerable<TDto>>> GetAllAsync()
        {
            IEnumerable<TEntity> entities =  _memoryCache.Get<IEnumerable<TEntity>>(CacheKey);
            IEnumerable<TDto> dtos = _mapper.Map<IEnumerable<TDto>>(entities);
            return CustomResponseDto<IEnumerable<TDto>>.Success(StatusCodes.Status200OK, dtos);
        }

        public async Task<CustomResponseDto<TDto>> GetByIdAsync(int id)
        {
            TEntity entity = _memoryCache.Get<List<TEntity>>(CacheKey).FirstOrDefault(x => x.Id == id);
            TDto dto = _mapper.Map<TDto>(entity);
            return CustomResponseDto<TDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id)
        {
            TEntity entity = await _repository.GetByIdAsync(id);
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<int> ids)
        {
            List<TEntity> entities = await _repository.Where(x=> ids.Contains(x.Id)).ToListAsync();
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(TDto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> expression)
        {
            List<TEntity> entities = await _repository.Where(expression).ToListAsync();
            IEnumerable<TDto> dtos = _mapper.Map<IEnumerable<TDto>>(entities);
            return CustomResponseDto<IEnumerable<TDto>>.Success(StatusCodes.Status200OK, dtos);
        }

        public async Task CacheAllProducts()
        {
            _memoryCache.Set(CacheKey, await _repository.GetAll().ToListAsync());
        }
    }
}
