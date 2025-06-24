using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Products
    {
        public int Id { get; set; }                        // Khóa chính
        public string Name { get; set; }                   // Tên sản phẩm
        public decimal Price { get; set; }                 // Giá sản phẩm
        public DateTime CreatedDate { get; set; }          // Ngày tạo

        public int CategoryId { get; set; }                // FK: mã danh mục (ràng buộc)
        public Categories Category { get; set; }             // Navigation: truy xuất tên danh mục
    }
}
