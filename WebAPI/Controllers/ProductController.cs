using AutoMapper;
using Microsoft.AspNetCore.Mvc; // use for create Controller in ASP.NET Core
using System;
using System.Threading.Tasks;
using WebAPI.DTO.ProductDTO;

// Import namespace 
using WebAPI.Models;   // include model Product
using WebAPI.Services; // include class process business ProductService

namespace WebAPI.Controllers 
{
    // setup route: API will access via path /api/product
    [Route("api/[controller]")]

    // Xác định đây là một API Controller, support auto validate model and return error standard REST
    [ApiController]
    public class ProductController : ControllerBase // Controller don't use View, chuyên processing API
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
             return Ok(new { message = "add product succsess", data = product });
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
            return Ok(new { message = "Add product succsess", data = product});
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto dto)
        {
            try
            {
                bool status = await _productService.UpdateProductAsync(id, dto);
                if(status == true)
                {
                    return Ok(new
                    {
                        message = "Updated product succsess",
                        data = await _productService.GetProductById(id)
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        message = "Not found product!"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erorr when update: {ex}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProduct(int id, [FromBody] UpdatePatchDto dto)
        {
            try
            {
                var result = await _productService.PatchProductAsync(id, dto);
                if (!result)
                    return NotFound("Not found product");

                return Ok(new 
                { 
                    message = "Update successed", 
                    data = await _productService.GetProductById(id)
                });
                        
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error server: {ex.Message}");
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
                    return NotFound("Not found the product");
                await _productService.DeleteProductAsync(id);
                return Ok(new
                {
                    message = "Delete product succsess"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error when delete: {ex.Message}");
            }
            
        }


        // note create and add project test Unit with xunit test by terminal
        /*dotnet new xunit -n WebAPI.Tests : create project test use framework xUnit
        dotnet sln add WebAPI.Tests/WebAPI.Tests.csproj : add project test to solution .sln
        dotnet add WebAPI.Tests reference WebAPI/WebAPI.csproj: Để test gọi được sang project WebAPI, bạn cần thêm tham chiếu,
        */
    }
}
