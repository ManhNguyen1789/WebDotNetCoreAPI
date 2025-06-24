using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Repositories.CategoryRepo;

namespace WebAPI.Services
{
    // class service include logic comunicate
    public class CategoryService
    {
        // call Interface Repository
        private readonly ICategoryRepository _repository;

        // khoi tao, inject ICategoryRepository qua Dependency injection
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        // Phương thức bất đồng bộ: lấy danh sách tất cả danh mục
        public Task<IEnumerable<Categories>> GetAllAsync()
        => _repository.GetAllAsync(); // Gọi đến repository, không xử lý gì thêm

        // Phương thức bất đồng bộ: lấy danh mục theo ID
        public Task<Categories> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id); // Ủy quyền việc tìm ID cho repository

        // Phương thức bất đồng bộ: thêm danh mục mới
        public Task AddAsync(Categories category)
            => _repository.AddAsync(category); // Gọi repository để lưu danh mục vào DB

        // Phương thức bất đồng bộ: cập nhật danh mục
        public Task UpdateAsync(Categories category)
            => _repository.UpdateAsync(category); // Gọi repository để cập nhật

        // Phương thức bất đồng bộ: xóa danh mục theo ID
        public Task DeleteAsync(int id)
            => _repository.DeleteAsync(id); // Gọi repository để xóa danh mục
    }
}
