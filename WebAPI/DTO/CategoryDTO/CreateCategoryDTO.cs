using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO.CategoryDTO
{
    // DTO user for request create new category
    public class CreateCategoryDTO
    {
        public string Name { get; set; } // only get category name form client
    }
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
