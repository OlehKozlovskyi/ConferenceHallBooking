using AutoMapper;
using ConferenceHallBooking.Application.Abstractions.IRepository;
using ConferenceHallBooking.Application.Abstractions.IServices;
using ConferenceHallBooking.Application.Abstractions.Other;
using ConferenceHallBooking.Application.DTOs.Requests;
using ConferenceHallBooking.Application.DTOs.Responses;
using ConferenceHallBooking.Domain.Entitities;

namespace ConferenceHallBooking.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IConferenceHallRepository _conferenceHallRepository;
        private readonly IBookingPricingCalculator _bookingPricingCalculator;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingService(
            IConferenceHallRepository conferenceHallRepository,
            IBookingPricingCalculator bookingPricingCalculator,
            IBookingRepository bookingRepository,
            IMapper mapper)
        {
            _conferenceHallRepository = conferenceHallRepository;
            _bookingPricingCalculator = bookingPricingCalculator;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<BookingResponse> CreateBookingAsync(CreateBookingRequest bookingRequest, CancellationToken ct = default)
        {
            if (bookingRequest == null)
                throw new ArgumentNullException(nameof(bookingRequest), "Booking request cannot be null.");

            var newBooking = _mapper.Map<Booking>(bookingRequest);
            var existingHall = await _conferenceHallRepository.GetConferenceHallByIdAsNoTrackingAsync(bookingRequest.HallId, ct);
            var costOfAmenities = existingHall!.Amenities
                ?.Where(a => bookingRequest.AmenityIds.Contains(a.Id))
                .Sum(a => a.Price) ?? 0;

            var bookingPrice = _bookingPricingCalculator.CalculateBookingPrice(newBooking.StartTime, newBooking.EndTime, existingHall.PricePerHour);
            newBooking.TotalPrice = bookingPrice + costOfAmenities;

            var result = await _bookingRepository.AddBookingAsync(newBooking, ct);
            var response = _mapper.Map<BookingResponse>(result);

            return response;
        }
    }
}
