using Microsoft.AspNetCore.Http;
using SimpleBookingSystem.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SimpleBookingSystem.UnitTest.Booking
{
    public class CreateBookingTest
    {
        [Theory]
        [InlineData("2022-04-01", "2022-04-29", 2, 10)]
        public async Task CreateBooking_ReturnCreatedBooking(string dateFrom, string dateTo, int resourceId, int bookedQuantity)
        {
            //Arrange
            var createBookingRequestModel = new CreateBookingRequestModel()
            {
                DateFrom = DateTime.Parse(dateFrom),
                DateTo = DateTime.Parse(dateTo),
                ResourceId = resourceId,
                BookedQuantity = bookedQuantity
            };
            var bookingService = TestFactories.BookingServiceTestFactory();

            // Act
            var createdBooking = await bookingService.CreateBookingAsync(createBookingRequestModel);

            // Assert
            Assert.NotNull(createdBooking);
            Assert.True(createdBooking.Id > 0);
        }

    }
}
