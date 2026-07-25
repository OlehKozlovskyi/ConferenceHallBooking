using ConferenceHallBooking.Application.Abstractions.Other;

namespace ConferenceHallBooking.Application.Strategies
{
    public class MorningDiscountPricingStrategy : IPricingStrategy
    {
        private static readonly TimeSpan RangeStart = TimeSpan.FromHours(6);
        private static readonly TimeSpan RangeEnd = TimeSpan.FromHours(9);
        private const decimal DiscountRate = 0.10m;

        public bool IsApplicable(TimeSpan startBookingTime, TimeSpan endBookingTime) =>
            startBookingTime >= RangeStart && endBookingTime <= RangeEnd;

        public decimal CalculatePrice(decimal basePrice, TimeSpan duration) =>
            basePrice * (decimal)duration.TotalHours * (1 - DiscountRate);
    }
}
