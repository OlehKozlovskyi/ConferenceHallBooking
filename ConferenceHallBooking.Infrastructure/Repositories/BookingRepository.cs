using ConferenceHallBooking.Application.Abstractions.IRepository;
using ConferenceHallBooking.Domain.Entitities;
using ConferenceHallBooking.Infrastructure.Persistance;

namespace ConferenceHallBooking.Infrastructure.Repositories
{
    public class BookingRepository(ApplicationDbContext dbContext) : IBookingRepository
    {
        public async Task<Booking> AddBookingAsync(Booking newBooking, CancellationToken ct = default)
        {
            var result = await dbContext.Bookings.AddAsync(newBooking);
            await dbContext.SaveChangesAsync(ct);

            return result.Entity;
        }
    }
}
