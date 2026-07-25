namespace ConferenceHallBooking.Application.DTOs.Requests
{
    public record CreateBookingRequest
    {
        public Guid HallId { get; init; }

        public DateTime StartDate { get; init; }

        public TimeSpan DurationsInHours { get; init; }

        public ICollection<Guid> AmenityIds { get; init; } = [];
    }
}
