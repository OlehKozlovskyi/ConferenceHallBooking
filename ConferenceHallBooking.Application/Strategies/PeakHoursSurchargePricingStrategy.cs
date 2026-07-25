using ConferenceHallBooking.Application.Abstractions.Other;

namespace ConferenceHallBooking.Application.Strategies
{
    public class PeakHoursSurchargePricingStrategy : IPricingStrategy
    {
        private static readonly TimeSpan RangeStart = TimeSpan.FromHours(12);
        private static readonly TimeSpan RangeEnd = TimeSpan.FromHours(14);
        private const decimal SurchargeRate = 0.15m;

        public bool IsApplicable(TimeSpan startBookingTime, TimeSpan endBookingTime) =>
            startBookingTime >= RangeStart && endBookingTime <= RangeEnd;

        public decimal CalculatePrice(decimal basePrice, TimeSpan duration) =>
            basePrice * (decimal)duration.TotalHours * (1 + SurchargeRate);
    }
}
