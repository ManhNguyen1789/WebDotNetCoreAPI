using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.DTO.ProductDTO;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class ProductRepository : IProductRepository,
    IProductAddRepository,
    IProductGetIdRepository,
    IProductUpdateRepository,
    IProductPatchRepository,
    IProductDeleteRepository
    {
        private readonly MyDbContext _context;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Products>> GetAllAsync()
        {

            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Products> GetByIdAsync(int id)
        {
            // use automapping
            return await _context.Products.Where(p => p.Id == id).Include(p => p.Category).FirstOrDefaultAsync();
            // use mapping DTO
            /*return await _context.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    CreatedDate = p.CreatedDate,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name
                })
                .FirstOrDefaultAsync();*/
        }

        public async Task<DeleteProductDto> GetProductByIdForDeleteAsync(int id)
        {
            return await _context.Products
                .Where(p => p.Id == id)
                .Select(p => new DeleteProductDto
                {
                    Id = p.Id
                })
                .FirstOrDefaultAsync();
        }

        /*public void Add(Product product)
        {
            _context.Products.Add(product);
        }*/

        //Add new product to database
        //async khai báo hàm bất đồng bộ, giúp không bị block khi quá tải
        public async Task AddAsync(Products product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        // Update product
        public async Task UpdateAsync(Products product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        // Update product patch
        public async Task PatchAsync(Products product)
        {
            _context.Products.Update(product); // Entity đang được tracking thì chỉ cần Save
            await _context.SaveChangesAsync();
        }

        // Delete product by ID
        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
