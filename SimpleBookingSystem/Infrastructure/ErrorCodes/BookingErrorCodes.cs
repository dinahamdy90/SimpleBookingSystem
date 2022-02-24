namespace SimpleBookingSystem.Infrastructure.ErrorCodes
{
    public enum BookingErrorCodes
    {
        InvalidResource,
        DatesConflicts,
        QuantityUnavailable,
        InvalidDateFrom,
        InvalidDateTo,
        InvalidQuantity,
        DateFromLessThanDateTo
    }
}
