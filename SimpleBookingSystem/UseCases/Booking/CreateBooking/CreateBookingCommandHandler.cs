using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SimpleBookingSystem.Infrastructure.Contracts;
using SimpleBookingSystem.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBookingSystem.UseCases.Booking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, CreateBookingResponseModel>
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public CreateBookingCommandHandler(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CreateBookingResponseModel> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingService.CreateBookingAsync(request.CreateBookingRequestModel);
            return new CreateBookingResponseModel(booking.Id);
        }
    }
}
