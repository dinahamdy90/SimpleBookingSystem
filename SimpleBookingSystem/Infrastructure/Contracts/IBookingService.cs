using SimpleBookingSystem.Infrastructure.Domain;
using SimpleBookingSystem.Models;

namespace SimpleBookingSystem.Infrastructure.Contracts
{
    public interface IBookingService
    {
        Task<Booking> CreateBookingAsync(CreateBookingRequestModel createBookingRequestModel);
    }
}
