using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SimpleBookingSystem.Models;
using System;
using System.Net.Http;
using System.Text; 
using System.Threading.Tasks;
using Xunit;

namespace SimpleBookingSystem.IntegrationTest.Booking
{
    public class CreateBookingTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        public CreateBookingTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        [Theory]
        [InlineData("2022-04-01", "2022-04-29", 2, 10)]
        public async Task CreateBooking_ValidModel_ReturnAccepted(string dateFrom, string dateTo, int resourceId, int bookedQuantity)
        {
            // Arrange
            var client = _factory.ConfigureTest().CreateClient();
            var createBookingRequestModel = new CreateBookingRequestModel()
            {
                DateFrom = DateTime.Parse(dateFrom),
                DateTo= DateTime.Parse(dateTo),
                ResourceId = resourceId,
                BookedQuantity = bookedQuantity
            };
            var payload = JsonConvert.SerializeObject(createBookingRequestModel);
            var content = new StringContent(payload, encoding: UTF8Encoding.UTF8, "application/json");
            var url = "api/Booking";

            // Act
            var response = await client.PostAsync(url, content);

            // Assert
            Assert.Equal(StatusCodes.Status202Accepted, (int)response.StatusCode);
        }

        [Theory]
        [InlineData("2022-04-01", "2022-04-29", 2, 1000)]
        [InlineData("2022-01-01", "2022-04-29", 2, 1)]
        [InlineData("2022-01-01", "2022-04-29", 2, 0)]
        public async Task CreateBooking_ValidModel_ReturnBadRequest(string dateFrom, string dateTo, int resourceId, int bookedQuantity)
        {
            // Arrange
            var client = _factory.ConfigureTest().CreateClient();
            var createBookingRequestModel = new CreateBookingRequestModel()
            {
                DateFrom = DateTime.Parse(dateFrom),
                DateTo = DateTime.Parse(dateTo),
                ResourceId = resourceId,
                BookedQuantity = bookedQuantity
            };
            var payload = JsonConvert.SerializeObject(createBookingRequestModel);
            var content = new StringContent(payload, encoding: UTF8Encoding.UTF8, "application/json");
            var url = "api/Booking";

            // Act
            var response = await client.PostAsync(url, content);

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);
        }

    }
}