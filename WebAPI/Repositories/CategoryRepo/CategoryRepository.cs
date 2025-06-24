using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Repositories.CategoryRepo
{
    public class CategoryRepository : ICategoryRepository
    {
       private readonly MyDbContext _context; // initial DbContext by DI
       public CategoryRepository(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }

        public async Task AddAsync(Categories category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if(category != null)
            {
                _context.Categories.Remove(category); // Remove out of DB
                await _context.SaveChangesAsync(); // Save status to BD
            }
        }

        public async Task<IEnumerable<Categories>> GetAllAsync()
        {
           return await _context.Categories.ToListAsync(); // Get all record from category
        }

        public async Task<Categories> GetByIdAsync(int id)
        {
           return await _context.Categories.FindAsync(id); // find record by id
        }

        public async Task UpdateAsync(Categories category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
