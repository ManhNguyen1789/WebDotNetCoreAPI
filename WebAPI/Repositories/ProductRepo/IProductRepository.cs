using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO.ProductDTO;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetAllAsync();
    }
    public interface IProductGetIdRepository
    {
        Task<Products> GetByIdAsync(int id);

        // Get ProductByID for Delete
        Task<DeleteProductDto> GetProductByIdForDeleteAsync(int id);
    }

    public interface IProductAddRepository
    {
        Task AddAsync(Products product);                
    }

    public interface IProductUpdateRepository
    {
        Task UpdateAsync(Products product);            
    }

    public interface IProductDeleteRepository
    {
        Task DeleteAsync(int id);                      
    }
}
