using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using TennisBookings.Merchandise.Api.Data.Dto;
using TennisBookings.Merchandise.Api.External.Database;
using TennisBookings.Merchandise.Api.IntegrationTests.Fakes;
using TennisBookings.Merchandise.Api.IntegrationTests.Models;
using Xunit;

namespace TennisBookings.Merchandise.Api.IntegrationTests.Controllers
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Startup> _factory;

        public ProductsControllerTests(WebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/products/");

            _client = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task GetAll_ReturnsExpectedArrayOfProducts()
        {
            var cloudDatabase = new FakeCloudDatabase(new[]
           {
                new ProductDto { StockCount = 200 },
                new ProductDto { StockCount = 500 },
                new ProductDto { StockCount = 300 }
            });

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<ICloudDatabase>(cloudDatabase);
                });
            }).CreateClient();

            var products = await client.GetFromJsonAsync<ExpectedProductModel[]>("");

            Assert.NotNull(products);
            Assert.Equal(3, products.Count());
        }

        [Fact]
        public async Task Get_ReturnsExpectedProduct()
        {
            var expectedId = Guid.NewGuid();

            var cloudDatabase = new FakeCloudDatabase(new[]
           {
                new ProductDto { Id = expectedId, Name = "EXPECTED" },
                new ProductDto { Id = Guid.NewGuid(), Name = "NOT_EXPECTED_1" },
                new ProductDto { Id = Guid.NewGuid(), Name = "NOT_EXPECTED_2" }
            });

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<ICloudDatabase>(cloudDatabase);
                });
            }).CreateClient();

            var product = await client.GetFromJsonAsync<ExpectedProductModel>($"{expectedId}");

            Assert.NotNull(product);
            Assert.Equal("EXPECTED", product.Name);
        }
    }
}
