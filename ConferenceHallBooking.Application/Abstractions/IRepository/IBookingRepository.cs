using ConferenceHallBooking.Domain.Entitities;

namespace ConferenceHallBooking.Application.Abstractions.IRepository
{
    public interface IBookingRepository
    {
        Task<Booking> AddBookingAsync(Booking newBooking, CancellationToken ct = default);
    }
}