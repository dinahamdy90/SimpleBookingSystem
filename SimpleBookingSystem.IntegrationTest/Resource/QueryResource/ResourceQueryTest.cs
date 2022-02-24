using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Http;

namespace SimpleBookingSystem.IntegrationTest.Resource
{
    public class ResourceQueryTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        public ResourceQueryTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }


        [Fact]
        public async Task GetResources_ReturnOk()
        {
            // Arrange
            var url = $"api/Resource";
            var client = _factory.ConfigureTest().CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);

        }
    }
}