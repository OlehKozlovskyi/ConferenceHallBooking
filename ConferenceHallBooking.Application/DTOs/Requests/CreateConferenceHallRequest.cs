using System.ComponentModel.DataAnnotations;

namespace ConferenceHallBooking.Application.DTOs.Requests
{
    public record CreateConferenceHallRequest
    {
        [Required]
        public string Name { get; init; } = null!;

        public int Capacity { get; init; }

        public decimal BasePricePerHour { get; init; }

        public IEnumerable<AmenitiesRequest> Amenities { get; init; } = [];
    }
}
