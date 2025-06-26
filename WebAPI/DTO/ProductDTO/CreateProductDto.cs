using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO.ProductDTO
{
    // DTO when create new (POST)
    public class CreateProductDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Range(0, 1000000)]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }

    // DTO return data (GET)
    public class ProductDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Range(0, 1000000)]
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }

        public int CategoryId { get; set; }         
        public string CategoryName { get; set; }   
    }

    // DTO when update (PUT)
    public class UpdateProductDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Range(0, 1000000)]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }


    // DTO when update patch (PATCH)
    public class UpdatePatchDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Range(0, 1000000)]
        public decimal Price { get; set; }
    }

    // DTO when get product for (DELETE)
    public class DeleteProductDto
    {
        public int Id { get; set; }
    }
}
