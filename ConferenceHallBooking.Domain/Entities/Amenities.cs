namespace ConferenceHallBooking.Domain.Entitities
{
    public class Amenities
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public Guid HallId { get; set; }

        public Hall Hall { get; set; } = null!;
    }
}
