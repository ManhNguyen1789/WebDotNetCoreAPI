using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebAPI.Tests.Tests
{
    public class ProductPatchTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductPatchTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PatchProduct_WithValidData_ShouldReturnOkAndUpdate()
        {
            // Arrange: chuẩn bị dữ liệu giả lập gửi lên API PATCH
            var productId = 2;
            var payload = new
            {
                Name = "Giày thể thao mới"
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8,
                "application/json");

            // Act: gọi API PATCH như thật
            var response = await _client.PatchAsync($"/api/products/{productId}", content);

            // Assert: kiểm tra kết quả trả về có mã 200 và đúng dữ liệu
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var body = await response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(body);

            string actualName = json.data.name;
            actualName.Should().Be("Giày thể thao mới");
        }
    }
}
