using ConferenceHallBooking.Domain.Entitities;

namespace ConferenceHallBooking.Application.Abstractions
{
    public interface IConferenceHallRepository
    {
        Task<Hall> AddConferenceHallAsync(Hall newHall, CancellationToken ct = default);
        Task<Hall> ApplyUpdateConferenceHallAsync(Guid hallId, CancellationToken ct = default);
        Task<Booking> CreateBookingAsync(Booking newBooking, CancellationToken ct = default);
        Task<List<Hall>> GetAllConferenceHallsAsync(CancellationToken ct = default);
        Task<Hall?> GetConferenceHallByIdAsNoTrackingAsync(Guid hallId, CancellationToken ct = default);
        Task<Hall?> GetConferenceHallByIdAsync(Guid hallId, CancellationToken ct = default);
        Task<bool> TryDeleteConferenceHallAsync(Guid hallId, CancellationToken ct = default);
        void RemoveAmenities(IEnumerable<Amenities> amenities);
        Task<IEnumerable<Hall>> GetAvailableConferenceHallsAsync(DateTime requestedStart, DateTime requestedEnd, int requiredCapacity, CancellationToken ct = default);
    }
}