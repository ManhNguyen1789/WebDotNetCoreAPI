// Import các thư viện cần thiết
using AutoMapper;
using Microsoft.AspNetCore.Mvc; // Dùng cho việc tạo Controller trong ASP.NET Core
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IMapper _mapper;

        // Constructor để truyền dependency ProductService vào (Dependency Injection)
        public ProductController(ProductService productService, IMapper mapper)
        {
            _productService = productService; // Gán đối tượng được inject vào biến dùng nội bộ
            _mapper = mapper;
        }

        // API GET: Lấy danh sách tất cả sản phẩm
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                // Gọi phương thức từ service và trả về kết quả với mã HTTP 200 (OK)
                var products = await _productService.GetAllProducts();
                if (products == null) return NotFound();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi get products: {ex}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound();
            var dto = _mapper.Map<ProductDto>(product);
            return Ok(dto);
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
            return Ok(new { message = "Thêm sản phẩm thành công", data = product });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto dto)
        {
            // Check existing Product
            var existingProduct = await _productService.GetProductById(id);
            if (existingProduct == null)
                return NotFound("Không tìm thấy sản phẩm");
            try
            {
                var product = new Products
                {
                    Id = id,
                    Name = dto.Name,
                    Price = dto.Price,
                    CreatedDate = existingProduct.CreatedDate,
                    CategoryId = dto.CategoryId
                };
                // Update
                await _productService.UpdateProductAsync(product);
                return Ok(new
                {
                    message = "Cập nhật sản phẩm thành công",
                    data = product
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật: {ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // Kiểm tra sản phẩm tồn tại
            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
                return NotFound("Không tìm thấy sản phẩm");
            try
            {
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
