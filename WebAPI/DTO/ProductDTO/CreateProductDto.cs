using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO.ProductDTO
{
    // DTO khi tạo mới (POST)
    public class CreateProductDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Range(0, 1000000)]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }

    // DTO trả dữ liệu ra ngoài (GET)
    public class ProductDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Range(0, 1000000)]
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }

        public int? CategoryId { get; set; }         // Liên kết đến danh mục
        public string? CategoryName { get; set; }    // Tên danh mục (từ navigation)
    }

    // DTO khi tạo mới (PUT)
    public class UpdateProductDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Range(0, 1000000)]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }

    // DTO khi tạo mới (DELETE)
    public class DeleteProductDto
    {
        public int Id { get; set; }
    }
}
