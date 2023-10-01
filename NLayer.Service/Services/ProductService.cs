using AutoMapper;
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
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productService, IMapper mapper) : base(repository, unitOfWork)
        {
            _productRepository = productService;
            _mapper = mapper;
        }

        public async Task<List<ProductWithCategoryDto>> GetProdutsWithCategory()
        {
            var product = await _productRepository.GetProdutsWithCategoryAsync();
            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(product);
            return productsDto;
        }
    }
}
