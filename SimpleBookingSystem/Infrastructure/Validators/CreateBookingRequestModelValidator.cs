using FluentValidation;
using SimpleBookingSystem.Infrastructure.ErrorCodes;
using SimpleBookingSystem.Models;

namespace SimpleBookingSystem.Infrastructure.Validators
{
    public class CreateBookingRequestModelValidator : AbstractValidator<CreateBookingRequestModel>
    {
        public CreateBookingRequestModelValidator()
        {
            RuleFor(x => x.DateFrom).NotNull().NotEmpty().GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage(BookingErrorCodes.InvalidDateFrom.ToString());
            RuleFor(x => x.DateTo).NotNull().NotEmpty().GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage(BookingErrorCodes.InvalidDateTo.ToString());
            RuleFor(x => x.BookedQuantity).GreaterThan(0).WithMessage(BookingErrorCodes.InvalidQuantity.ToString());
            RuleFor(x => x.ResourceId).GreaterThan(0).WithMessage(BookingErrorCodes.InvalidResource.ToString());

            RuleFor(x => x.DateTo).GreaterThan(x=>x.DateFrom).WithMessage(BookingErrorCodes.DateFromLessThanDateTo.ToString());

        }
    }
}