namespace ConferenceHallBooking.Application.DTOs.Requests
{
    public record UpdateConferenceHallRequest
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = null!;

        public int Capacity { get; init; }

        public decimal BasePricePerHour { get; init; }

        public IEnumerable<AmenitiesRequest> Amenities { get; init; } = [];
    }
}
