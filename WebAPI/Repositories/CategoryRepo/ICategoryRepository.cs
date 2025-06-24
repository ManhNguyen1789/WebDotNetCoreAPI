using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Repositories.CategoryRepo
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Categories>> GetAllAsync();
        Task<Categories> GetByIdAsync(int id);
        Task AddAsync(Categories category);
        Task UpdateAsync(Categories category);
        Task DeleteAsync(int id);
    }
}
