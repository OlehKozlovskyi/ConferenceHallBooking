namespace ConferenceHallBooking.Application.DTOs.Responses
{
    public record ConferenceHallResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = null!;

        public int Capacity { get; init; }

        public decimal BasePricePerHour { get; init; }

        public IEnumerable<AmentitiesResponse> Amenities { get; init; } = [];
    }
}
