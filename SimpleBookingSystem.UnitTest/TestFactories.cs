

using AutoMapper;
using SimpleBookingSystem.Infrastructure.Services;

namespace SimpleBookingSystem.UnitTest
{
    public static class TestFactories
    {
        public static ResourceService ResourceServiceTestFactory()
        {
            var context = SimpleBookingContextMock.GetDBContext();
            var mapperMock = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(System.Reflection.Assembly.GetEntryAssembly(), typeof(Program).Assembly);
            });
            var mapper = mapperMock.CreateMapper();
            return new ResourceService(context, mapper);
        }
        public static BookingService BookingServiceTestFactory()
        {
            var context = SimpleBookingContextMock.GetDBContext();
            var mapperMock = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(System.Reflection.Assembly.GetEntryAssembly(), typeof(Program).Assembly);
            });
            var mapper = mapperMock.CreateMapper();
            return new BookingService(context, mapper);
        }
      

    }
}