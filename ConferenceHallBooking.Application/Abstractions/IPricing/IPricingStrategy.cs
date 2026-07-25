namespace ConferenceHallBooking.Application.Abstractions.Other
{
    public interface IPricingStrategy
    {
        bool IsApplicable(TimeSpan startBookingTime, TimeSpan endBookingTime);

        decimal CalculatePrice(decimal basePrice, TimeSpan duration);
    }
}
