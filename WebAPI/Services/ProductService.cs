using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
            var product = await _productGetIdRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<DeleteProductDto> GetProductByIdForDeleteAsync(int id)
        {
            return await _productGetIdRepository.GetProductByIdForDeleteAsync(id);
        }

        public Task AddProductAsync(Products product) 
        {
            return _productAddRepository.AddAsync(product); 
        }

        public async Task<Task> UpdateProductAsync(int id, [FromBody] UpdateProductDto dto)
        {
            // Check existing Product
            var existingProduct = await GetProductById(id);
            /*if (existingProduct == null)
                return NotFound("Không tìm thấy sản phẩm");*/
            // Update with mapping DTO
            var pt = new Products
            {
                Id = id,
                Name = dto.Name,
                Price = dto.Price,
                CreatedDate = existingProduct.CreatedDate,
                CategoryId = (int)existingProduct.CategoryId
            };
            return _productUpdateRepository.UpdateAsync(pt);
        }

        public Task DeleteProductAsync(int id)
        {
            return _productDeleteRepository.DeleteAsync(id);
        }
    }
}
