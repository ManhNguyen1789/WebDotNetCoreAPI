// Import các thư viện cần thiết
using AutoMapper;
using Microsoft.AspNetCore.Mvc; // Dùng cho việc tạo Controller trong ASP.NET Core
using System;
using System.Threading.Tasks;
using WebAPI.DTO.ProductDTO;

// Import namespace của các lớp do bạn định nghĩa
using WebAPI.Models;   // Chứa model Product
using WebAPI.Services; // Chứa lớp xử lý nghiệp vụ ProductService

namespace WebAPI.Controllers // Không gian tên cho Controller
{
    // Thiết lập route cơ bản: API sẽ truy cập qua đường dẫn /api/product
    [Route("api/[controller]")]

    // Xác định đây là một API Controller, hỗ trợ tự động validate model và trả lỗi chuẩn REST
    [ApiController]
    public class ProductController : ControllerBase // Controller không dùng View, chuyên xử lý API
    {
        // Biến chỉ đọc để dùng các hàm xử lý nghiệp vụ từ lớp ProductService
        private readonly ProductService _productService;

        // Constructor để truyền dependency ProductService vào (Dependency Injection)
        public ProductController(ProductService productService)
        {
            _productService = productService; // Gán đối tượng được inject vào biến dùng nội bộ
        }

        // API GET: Get all products record
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                // call method from service and return result with code HTTP 200 (OK)
                var products = await _productService.GetAllProducts();
                if (products == null) return NotFound();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error when get products: {ex}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                if (product == null) return NotFound();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error when get product: {ex}");
            }
        }

        // API POST: Thêm một sản phẩm mới
        /* [HttpPost]
         public async Task<IActionResult> CreateProduct([FromBody] Product product)
         {
             // Gán ngày tạo nếu chưa có
             product.CreatedDate = DateTime.UtcNow;

             // Gọi service để thêm sản phẩm vào cơ sở dữ liệu
             await _productService.AddProductAsync(product);

             // Trả về phản hồi thành công
             return Ok(new { message = "Thêm sản phẩm thành công", data = product });
         }*/

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            var product = new Products
            {
                Name = dto.Name,
                Price = dto.Price,
                CreatedDate = DateTime.Now,
                CategoryId = dto.CategoryId
            };

            await _productService.AddProductAsync(product);
            // Trả về phản hồi thành công
            return Ok(new { message = "Add product thành công", data = product });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto dto)
        {
            try
            {
                await _productService.UpdateProductAsync(id, dto);
                return Ok(new
                {
                    message = "Updated product thành công",
                    data = await _productService.GetProductById(id)
            });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erorr when update: {ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                // Kiểm tra sản phẩm tồn tại
                var existingProduct = await _productService.GetProductByIdForDeleteAsync(id);
                if (existingProduct == null)
                    return NotFound("Không tìm thấy sản phẩm");
                await _productService.DeleteProductAsync(id);
                return Ok(new
                {
                    message = "Delete sản phẩm thành công"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi delete: {ex.Message}");
            }
            
        }
    }
}
