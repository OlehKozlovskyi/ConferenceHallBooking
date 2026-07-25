namespace ConferenceHallBooking.Domain.Entitities
{
    public class Hall
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public int Capacity { get; set; }

        public decimal PricePerHour { get; set; }

        public ICollection<Amenities> Amenities { get; set; } = [];

        // Navigation property for bookings
        public ICollection<Booking> Bookings { get; set; } = [];
    }
}
