using ConferenceHallBooking.Application.Abstractions.IRepository;
using ConferenceHallBooking.Domain.Entitities;
using ConferenceHallBooking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace ConferenceHallBooking.Infrastructure.Repositories
{
    public class ConferenceHallRepository(ApplicationDbContext dbContext) : IConferenceHallRepository
    {
        public async Task<Hall> AddConferenceHallAsync(Hall newHall, CancellationToken ct = default)
        {
            var result = await dbContext.Halls.AddAsync(newHall);

            await dbContext.SaveChangesAsync(ct);
            return result.Entity;
        }

        public async Task<Hall?> GetConferenceHallByIdAsync(Guid hallId, CancellationToken ct = default)
            => await dbContext.Halls
            .Include(h => h.Amenities)
            .FirstOrDefaultAsync(h => h.Id == hallId, ct);

        public async Task<List<Hall>> GetAllConferenceHallsAsync(CancellationToken ct = default)
            => await dbContext.Halls.ToListAsync(ct);

        public async Task<Hall?> GetConferenceHallByIdAsNoTrackingAsync(Guid hallId, CancellationToken ct = default)
            => await dbContext.Halls
            .AsNoTracking()
            .Include(h => h.Amenities)
            .FirstOrDefaultAsync(h => h.Id == hallId, ct);

        public async Task<Hall> ApplyUpdateConferenceHallAsync(Guid hallId, CancellationToken ct = default)
        {
            await dbContext.SaveChangesAsync(ct);
            var updatedEntity = await GetConferenceHallByIdAsNoTrackingAsync(hallId);

            return updatedEntity!;
        }

        public async Task<bool> TryDeleteConferenceHallAsync(Guid hallId, CancellationToken ct = default)
        {
            var rowsAffected = await dbContext.Halls
                .Where(h => h.Id == hallId)
                .ExecuteDeleteAsync(ct);

            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Hall>> GetAvailableConferenceHallsAsync(
            DateTime requestedStart,
            DateTime requestedEnd,
            int requiredCapacity,
            CancellationToken ct = default)
        {
            var result = await dbContext.Halls
                .Include(h => h.Amenities)
                .Where(h => h.Capacity >= requiredCapacity)
                .Where(h => !h.Bookings.Any(b =>
                    b.StartTime < requestedEnd &&
                    b.EndTime > requestedStart))
                .ToListAsync(ct);

            return result;
        }

        public void RemoveAmenities(IEnumerable<Amenities> amenities) => dbContext.Amenities.RemoveRange(amenities);
    }
}
