namespace ConferenceHallBooking.Application.DTOs.Responses
{
    public record BookingResponse
    {
        public Guid BookingId { get; init; }

        public string ConferenceHallName { get; init; } = string.Empty;

        public DateTime StartTime { get; init; }

        public DateTime EndTime { get; init; }

        public decimal TotalPrice { get; init; }
    }
}
