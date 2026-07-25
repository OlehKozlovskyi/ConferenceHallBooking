using System.ComponentModel.DataAnnotations;

namespace ConferenceHallBooking.Application.DTOs.Requests
{
    public record AmenitiesRequest
    {
        public Guid? Id { get; init; }

        [Required]
        public string Name { get; init; } = null!;

        public decimal Price { get; init; }
    }
}
