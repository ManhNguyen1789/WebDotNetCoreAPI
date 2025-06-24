// Import các namespace cần thiết từ ASP.NET Core và Entity Framework Core
using Microsoft.AspNetCore.Builder;              // Xử lý pipeline middleware (app.UseX)
using Microsoft.AspNetCore.Hosting;              // Quản lý môi trường chạy (development, production)
using Microsoft.Extensions.Configuration;        // Truy cập cấu hình từ appsettings.json
using Microsoft.Extensions.DependencyInjection;  // Đăng ký các dịch vụ DI
using Microsoft.Extensions.Hosting;              // Quản lý lifecycle của ứng dụng
using Microsoft.EntityFrameworkCore;             // Cần thiết để cấu hình DbContext sử dụng EF Core

// Import namespace nội bộ của bạn
using WebAPI.Data;         // Chứa lớp MyDbContext
using WebAPI.Repositories; // Chứa interface và implementation cho repository
using WebAPI.Services;     // Chứa lớp ProductService
using WebAPI.Repositories.CategoryRepo;
using WebAPI.Middlewares;

namespace WebAPI
{
    public class Startup
    {
        // Constructor: khởi tạo đối tượng cấu hình từ appsettings.json hoặc môi trường
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Biến chứa cấu hình, được truyền vào qua dependency injection
        public IConfiguration Configuration { get; }

        // Phương thức này được gọi bởi runtime để đăng ký các service cho DI container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); // Đăng ký dịch vụ MVC Controller để hỗ trợ Web API

            // Đăng ký IProductRepository với ProductRepository theo vòng đời "scoped"
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAddRepository, ProductRepository>();
            services.AddScoped<IProductGetIdRepository, ProductRepository>();
            services.AddScoped<IProductUpdateRepository, ProductRepository>();
            services.AddScoped<IProductDeleteRepository, ProductRepository>();

            // Đăng ký lớp ProductService, sẽ được inject tự động ở các Controller
            services.AddScoped<ProductService>();

            // Đăng ký ICategoryRepository với CategoryRepository theo vòng đời "scoped"
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            // Đăng ký lớp CategoryService, sẽ được inject tự động ở các Controller
            services.AddScoped<CategoryService>();

            // Đăng ký lớp AutoMapper, sẽ được inject tự động ở các Controller
            services.AddAutoMapper(typeof(MappingProfile));

            // Đăng ký DbContext với cấu hình chuỗi kết nối từ appsettings.json
            // options.UseSqlServer: sử dụng SQL Server làm database provider
            services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // Phương thức này cấu hình middleware xử lý các HTTP request (HTTP pipeline)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Nếu đang ở môi trường Development, hiển thị trang lỗi chi tiết
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Tự động chuyển hướng HTTP sang HTTPS
            app.UseHttpsRedirection();

            //Thêm middleware custom của bạn tại đây:
            app.UseMiddleware<RequestLoggingMiddleware>();

            // Thiết lập middleware định tuyến
            app.UseRouting();

            // Kích hoạt middleware kiểm tra ủy quyền (Authorization)
            app.UseAuthorization();

            // Cấu hình endpoint cho Controller, cho phép truy cập các action trong API
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Tự động ánh xạ các Controller có attribute route
            });
        }
    }
}
