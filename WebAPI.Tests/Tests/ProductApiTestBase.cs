using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Tests.Tests
{
    // File: TestUtils/ProductApiTestBase.cs
    public class ProductApiTestBase : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient _client;

        public ProductApiTestBase(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }
    }

}
