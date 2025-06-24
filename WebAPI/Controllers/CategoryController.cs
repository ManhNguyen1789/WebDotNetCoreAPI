using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO.CategoryDTO;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // => Route sẽ là: /api/category
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _service;

        public CategoryController(CategoryService service)
        {
            _service = service;
        }
        // GET: /api/category
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAllAsync();
            return Ok(categories);
        }

        // GET: /api/category/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(new CategoryDto { Id = category.Id, Name = category.Name });
        }

        // POST: /api/category
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO dto)
        {
            var category = new Categories
            {
                Name = dto.Name
            };

            await _service.AddAsync(category);
            return Ok(new CategoryDto { Id = category.Id, Name = category.Name });
        }

        // PUT: /api/category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto dto)
        {
            if (id != dto.Id) return BadRequest("ID không khớp");

            var category = new Categories { Id = dto.Id, Name = dto.Name };
            await _service.UpdateAsync(category);
            return NoContent();
        }

        // DELETE: /api/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
