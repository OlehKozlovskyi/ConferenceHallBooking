using ConferenceHallBooking.Application.Abstractions.IRepository;

namespace ConferenceHallBooking.Application.Services
{
    public class BookingService(IBookingRepository bookingRepository)
    {
        public async Task CreateBookingAsync()
        {
            throw new NotImplementedException();
        }
    }
}
