namespace ConferenceHallBooking.Application.Abstractions.Other
{
    public interface IBookingPricingCalculator
    {
        decimal CalculateBookingPrice(DateTime startTime, DateTime endTime, decimal basePrice);
    }
}