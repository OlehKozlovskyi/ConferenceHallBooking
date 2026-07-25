namespace ConferenceHallBooking.Application.DTOs.Responses
{
    public record AmentitiesResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = null!;

        public decimal Price { get; init; }
    }
}
