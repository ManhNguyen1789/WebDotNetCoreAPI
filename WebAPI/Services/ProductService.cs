using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO.ProductDTO;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Services
{
    public class ProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IProductAddRepository _productAddRepository;
        private readonly IProductGetIdRepository _productGetIdRepository;
        private readonly IProductUpdateRepository _productUpdateRepository;
        private readonly IProductDeleteRepository _productDeleteRepository;

        public ProductService(IProductRepository productRepository, IProductAddRepository productAddRepository, IProductGetIdRepository productGetIdRepository, IProductUpdateRepository productUpdateRepository, IProductDeleteRepository productDeleteRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productAddRepository = productAddRepository;
            _productGetIdRepository = productGetIdRepository;
            _productUpdateRepository = productUpdateRepository;
            _productDeleteRepository = productDeleteRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            return await _productGetIdRepository.GetByIdAsync(id);
        }

        public async Task<DeleteProductDto> GetProductByIdAsync(int id)
        {
            return await _productGetIdRepository.GetProductByIdAsync(id);
        }

        public Task AddProductAsync(Products product) 
        {
            return _productAddRepository.AddAsync(product); 
        }

        public Task UpdateProductAsync(Products product)
        {
            return _productUpdateRepository.UpdateAsync(product);
        }

        public Task DeleteProductAsync(int id)
        {
            return _productDeleteRepository.DeleteAsync(id);
        }
    }
}
