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

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            return await _context.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    CreatedDate = p.CreatedDate,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DeleteProductDto> GetProductByIdAsync(int id)
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

        // Thêm sản phẩm mới vào database
        //async khai báo hàm bất đồng bộ, giúp không bị block khi quá tải
        public async Task AddAsync(Products product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        // Cập nhật thông tin sản phẩm
        public async Task UpdateAsync(Products product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        // Xóa sản phẩm theo ID
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
