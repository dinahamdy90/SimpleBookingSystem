using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleBookingSystem.Infrastructure.Domain;
using SimpleBookingSystem.Infrastructure.ErrorCodes;
using SimpleBookingSystem.Infrastructure.Extensions;
using SimpleBookingSystem.Infrastructure.Contracts;
using SimpleBookingSystem.Infrastructure.Utlities;
using SimpleBookingSystem.Models;
using System.Net;

namespace SimpleBookingSystem.Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly SimpleBookingContext _context;
        private readonly IMapper _mapper;

        public BookingService(SimpleBookingContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Booking> CreateBookingAsync(CreateBookingRequestModel createBookingRequestModel)
        {
            await ValidateBooking(createBookingRequestModel);

            var booking = _mapper.Map<Booking>(createBookingRequestModel);

          return  await _context.PostAsync(booking);
        }
        private async Task ValidateBooking(CreateBookingRequestModel createBookingRequestModel)
        {
            var selectedResource = await _context.Resource.Include(x=>x.Booking).FirstOrDefaultAsync(x => x.Id == createBookingRequestModel.ResourceId);
            if (selectedResource == null)
                throw new ApiException(HttpStatusCode.BadRequest, BookingErrorCodes.InvalidResource.ToString());

            if(selectedResource.Booking != null && selectedResource.Booking.Any())
            {
                if (selectedResource.Booking.Any(x => DateUtility.DatesOverlap(x.DateFrom, x.DateTo, createBookingRequestModel.DateFrom, createBookingRequestModel.DateTo)))
                    throw new ApiException(HttpStatusCode.Conflict, BookingErrorCodes.DatesConflicts.ToString());

                if(selectedResource.Quantity - selectedResource.Booking.Sum(x=>x.BookedQuantity) < createBookingRequestModel.BookedQuantity)
                    throw new ApiException(HttpStatusCode.BadRequest, BookingErrorCodes.QuantityUnavailable.ToString());
            }
        }
    }
}
