using ConferenceHallBooking.Application.Abstractions.Other;

namespace ConferenceHallBooking.Application.Strategies
{
    public class EveningDiscountPricingStrategy : IPricingStrategy
    {
        private static readonly TimeSpan RangeStart = TimeSpan.FromHours(18);
        private static readonly TimeSpan RangeEnd = TimeSpan.FromHours(23);
        private const decimal DiscountRate = 0.20m;

        public bool IsApplicable(TimeSpan startBookingTime, TimeSpan endBookingTime) =>
            startBookingTime >= RangeStart && endBookingTime <= RangeEnd;

        public decimal CalculatePrice(decimal basePrice, TimeSpan duration) =>
            basePrice * (decimal)duration.TotalHours * (1 - DiscountRate);
    }
}
