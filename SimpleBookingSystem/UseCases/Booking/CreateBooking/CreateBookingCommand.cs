using MediatR;
using SimpleBookingSystem.Models;

namespace SimpleBookingSystem.UseCases.Booking
{
    public class CreateBookingCommand : IRequest<CreateBookingResponseModel>
    {
        public CreateBookingRequestModel CreateBookingRequestModel { get; }
        public CreateBookingCommand(CreateBookingRequestModel createBookingRequestModel)
        {
            CreateBookingRequestModel = createBookingRequestModel;
        }
    }
}