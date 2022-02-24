using System;
using SimpleBookingSystem.Infrastructure;

namespace SimpleBookingSystem.IntegrationTest
{
    public static class Utilities
    {
        public static void InitializeTestDatabase(this SimpleBookingContext simpleBookingContext)
        {
            SeedBookingDb(simpleBookingContext);
            simpleBookingContext.SaveChanges();
        }

        /// <summary>
        /// This method is responsible for seeding the database with the records.
        /// </summary>
        /// <param name="simpleBookingContext">database context</param>
        public static void SeedBookingDb(SimpleBookingContext simpleBookingContext)
        {
            SeedResourceRecords(simpleBookingContext);
            SeedBookingRecords(simpleBookingContext);
        }
        private static void SeedResourceRecords(SimpleBookingContext simpleBookingContext)
        {
            simpleBookingContext.Resource.Add(new Infrastructure.Domain.Resource { Id = 1, Name = "Resource1", Quantity = 20 });
            simpleBookingContext.Resource.Add(new Infrastructure.Domain.Resource { Id = 2, Name = "Resource2", Quantity = 30 });
            simpleBookingContext.Resource.Add(new Infrastructure.Domain.Resource { Id = 3, Name = "Resource3", Quantity = 15 });
        }
        private static void SeedBookingRecords(SimpleBookingContext simpleBookingContext)
        {
            simpleBookingContext.Booking.Add(new Infrastructure.Domain.Booking { Id = 1, ResourceId = 1, DateFrom=DateTime.Parse("2022-05-01"), DateTo = DateTime.Parse("2022-05-10"), BookedQuantity=5 });
            simpleBookingContext.Booking.Add(new Infrastructure.Domain.Booking { Id = 2, ResourceId=2, DateFrom = DateTime.Parse("2022-05-01"), DateTo = DateTime.Parse("2022-05-10"), BookedQuantity = 20 });
            simpleBookingContext.Booking.Add(new Infrastructure.Domain.Booking { Id = 3, ResourceId=1, DateFrom = DateTime.Parse("2022-05-15"), DateTo = DateTime.Parse("2022-05-20"), BookedQuantity = 10 });
            simpleBookingContext.Booking.Add(new Infrastructure.Domain.Booking { Id = 4, ResourceId=1, DateFrom = DateTime.Parse("2022-06-01"), DateTo = DateTime.Parse("2022-06-10"), BookedQuantity = 5 });
            simpleBookingContext.Booking.Add(new Infrastructure.Domain.Booking { Id = 5, ResourceId = 3, DateFrom = DateTime.Parse("2022-05-01"), DateTo = DateTime.Parse("2022-05-10"), BookedQuantity = 5 });
            simpleBookingContext.Booking.Add(new Infrastructure.Domain.Booking { Id = 6, ResourceId=3, DateFrom = DateTime.Parse("2022-05-01"), DateTo = DateTime.Parse("2022-05-10"), BookedQuantity = 5 });

        }
       
    }
}