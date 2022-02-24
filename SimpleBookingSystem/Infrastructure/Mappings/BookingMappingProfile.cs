using AutoMapper;
using SimpleBookingSystem.Infrastructure.Domain;
using SimpleBookingSystem.Models;

namespace SimpleBookingSystem.Infrastructure.Mappings
{
    public class BookingMappingProfile : Profile
    {
        public BookingMappingProfile()
        {
            CreateMap<CreateBookingRequestModel, Booking>();
        }
    }
}
