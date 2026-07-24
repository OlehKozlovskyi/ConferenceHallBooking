using System.ComponentModel.DataAnnotations;

namespace ConferenceHallBooking.Application.DTOs.Requests
{
    public record AmenitiesRequest
    {
        [Required]
        public string Name { get; init; } = null!;

        public decimal Price { get; init; }
    }
}
