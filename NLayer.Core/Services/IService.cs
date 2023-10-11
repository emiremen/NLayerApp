﻿using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IService<TEntity,TDto> where TEntity : BaseEntity where TDto : BaseDto
    {
        Task<CustomResponseDto<IEnumerable<TDto>>> GetAllAsync();
        Task<CustomResponseDto<TDto>> GetByIdAsync(int id);
        Task<CustomResponseDto<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> expression);
        Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<TEntity, bool>> expression);
        Task<CustomResponseDto<TDto>> AddAsync(TDto dto);
        Task<CustomResponseDto<IEnumerable<TDto>>> AddRangeAsync(IEnumerable<TDto> dtos);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(TDto dto);
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id);
        Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<int> ids);
    }
}
