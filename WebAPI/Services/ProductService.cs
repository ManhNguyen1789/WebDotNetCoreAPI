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
        private readonly IProductPatchRepository _productUpdatePatchRepository;
        private readonly IProductDeleteRepository _productDeleteRepository;

        public ProductService(IProductRepository productRepository, IProductAddRepository productAddRepository, IProductGetIdRepository productGetIdRepository, IProductUpdateRepository productUpdateRepository, IProductPatchRepository productPatchRepository, IProductDeleteRepository productDeleteRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productAddRepository = productAddRepository;
            _productGetIdRepository = productGetIdRepository;
            _productUpdateRepository = productUpdateRepository;
            _productUpdatePatchRepository = productPatchRepository;
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

        public async Task<bool> UpdateProductAsync(int id, [FromBody] UpdateProductDto dto)
        {
            // Check existing Product
            var existingProduct = await _productGetIdRepository.GetByIdAsync(id);
            if (existingProduct == null) return false;
            existingProduct.Name = dto.Name;
            existingProduct.Price = dto.Price;
            existingProduct.CategoryId = dto.CategoryId;
            
            await _productUpdateRepository.UpdateAsync(existingProduct);
            return true;
        }

        public async Task<bool> PatchProductAsync(int id, UpdatePatchDto dto)
        {
            var existing = await _productGetIdRepository.GetByIdAsync(id);
            if (existing == null) return false;

            if (!string.IsNullOrEmpty(dto.Name)) existing.Name = dto.Name;
            existing.Price = dto.Price;

            await _productUpdatePatchRepository.PatchAsync(existing);
            return true;
        }

        public Task DeleteProductAsync(int id)
        {
            return _productDeleteRepository.DeleteAsync(id);
        }
    }
}
