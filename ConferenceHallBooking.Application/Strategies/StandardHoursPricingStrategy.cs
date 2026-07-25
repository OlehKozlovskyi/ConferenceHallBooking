using ConferenceHallBooking.Application.Abstractions.Other;

namespace ConferenceHallBooking.Application.Strategies
{
    public class StandardHoursPricingStrategy : IPricingStrategy
    {
        private static readonly TimeSpan MorningStandardStart = TimeSpan.FromHours(9);
        private static readonly TimeSpan MorningStandardEnd = TimeSpan.FromHours(12);
        private static readonly TimeSpan AfternoonStandardStart = TimeSpan.FromHours(14);
        private static readonly TimeSpan AfternoonStandardEnd = TimeSpan.FromHours(18);

        public bool IsApplicable(TimeSpan startBookingTime, TimeSpan endBookingTime) =>
            startBookingTime >= MorningStandardStart && endBookingTime <= MorningStandardEnd ||
            (startBookingTime >= AfternoonStandardStart && endBookingTime <= AfternoonStandardEnd);

        public decimal CalculatePrice(decimal basePrice, TimeSpan duration) =>
            basePrice * (decimal)duration.TotalHours;
    }
}
