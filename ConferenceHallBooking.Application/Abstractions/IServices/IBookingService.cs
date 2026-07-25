using ConferenceHallBooking.Application.DTOs.Requests;
using ConferenceHallBooking.Application.DTOs.Responses;

namespace ConferenceHallBooking.Application.Abstractions.IServices
{
    public interface IBookingService
    {
        Task<BookingResponse> CreateBookingAsync(CreateBookingRequest bookingRequest, CancellationToken ct = default);
    }
}