namespace ConferenceHallBooking.Domain.Entitities
{
    public class Booking
    {
        public Guid Id { get; set; }

        public Guid HallId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int NumberOfAttendees { get; set; }

        public decimal TotalPrice { get; set; }

        public Hall Hall { get; set; } = null!;
    }
}