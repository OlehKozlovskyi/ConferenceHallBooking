using AutoMapper;
using ConferenceHallBooking.Application.Abstractions;
using ConferenceHallBooking.Application.DTOs.Requests;
using ConferenceHallBooking.Application.DTOs.Responses;
using ConferenceHallBooking.Domain.Entitities;

namespace ConferenceHallBooking.Application.Services
{
    public class BookingHallService(
        IConferenceHallRepository conferenceHallRepository,
        IMapper mapper)
    {
        public async Task<ConferenceHallResponse> CreateConferanceHallAsync(CreateConferenceHallRequest request, CancellationToken ct = default)
        {
            //Implement validation of request
            if (request == null)
                throw new Exception("Request cannot be null");

            var newHall = mapper.Map<Hall>(request);
            var result = await conferenceHallRepository.AddConferenceHallAsync(newHall);
            var response = mapper.Map<ConferenceHallResponse>(result);

            return response;
        }
    }
}
