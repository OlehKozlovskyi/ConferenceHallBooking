using ConferenceHallBooking.Application.Abstractions.Other;

namespace ConferenceHallBooking.Application.Helpers
{
    public class BookingPricingCalculator(IEnumerable<IPricingStrategy> strategies) : IBookingPricingCalculator
    {
        public decimal CalculateBookingPrice(DateTime startTime, DateTime endTime, decimal basePrice)
        {
            var totalPrice = 0m;
            var segmentStartTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, startTime.Hour, 0, 0);
            var segmentEndTime = new DateTime(endTime.Year, endTime.Month, endTime.Day, endTime.Hour, 0, 0); ;

            while (segmentStartTime < segmentEndTime)
            {
                var segmentEnd = segmentStartTime.AddHours(1) > segmentEndTime
                ? segmentEndTime
                : segmentStartTime.AddHours(1);

                var segmentDuration = segmentEnd - segmentStartTime;

                var strategy = strategies.FirstOrDefault(s =>
                    s.IsApplicable(segmentStartTime.TimeOfDay, segmentEnd.TimeOfDay))
                    ?? throw new InvalidOperationException(
                        $"No pricing strategy found for time range {segmentStartTime.TimeOfDay}–{segmentEnd.TimeOfDay}");

                totalPrice += strategy.CalculatePrice(basePrice, segmentDuration);
                segmentStartTime = segmentEnd;
            }

            return totalPrice;
        }
    }
}
