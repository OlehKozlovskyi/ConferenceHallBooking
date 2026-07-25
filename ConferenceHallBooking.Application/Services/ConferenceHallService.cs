using AutoMapper;
using ConferenceHallBooking.Application.Abstractions.IRepository;
using ConferenceHallBooking.Application.Abstractions.IServices;
using ConferenceHallBooking.Application.DTOs.Requests;
using ConferenceHallBooking.Application.DTOs.Responses;
using ConferenceHallBooking.Domain.Entitities;

namespace ConferenceHallBooking.Application.Services
{
    public class ConferenceHallService(
        IConferenceHallRepository conferenceHallRepository,
        IMapper mapper) : IConferenceHallService
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

        public async Task<ConferenceHallResponse> UpdateConferenceHallAsync(UpdateConferenceHallRequest request, CancellationToken ct = default)
        {
            //Implement validation of request
            if (request == null)
                throw new Exception("Request cannot be null");

            var existingHall = await conferenceHallRepository.GetConferenceHallByIdAsync(request.Id, ct);

            if (existingHall == null)
                throw new Exception($"Conference hall with ID {request.Id} not found");

            existingHall.Name = request.Name;
            existingHall.PricePerHour = request.BasePricePerHour;
            existingHall.Capacity = request.Capacity;
            existingHall.Amenities = MergeAmenities(request.Amenities, existingHall.Amenities);

            var updatedHall = await conferenceHallRepository.ApplyUpdateConferenceHallAsync(existingHall.Id, ct);
            var response = mapper.Map<ConferenceHallResponse>(updatedHall);
            return response;
        }

        public async Task<bool> DeleteConferenceHallAsync(Guid hallId, CancellationToken ct = default)
        {
            var existingHall = await conferenceHallRepository.GetConferenceHallByIdAsync(hallId, ct);

            if (existingHall == null)
                throw new Exception($"Conference hall with ID {hallId} not found");

            var response = await conferenceHallRepository.TryDeleteConferenceHallAsync(hallId, ct);
            return response;
        }

        public async Task<IEnumerable<ConferenceHallResponse>> SearchAvailableConferenceHallsAsync(
            DateTime startDate,
            DateTime endDate,
            int requiredCapacity,
            CancellationToken ct = default)
        {
            var utcStart = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
            var utcEnd = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

            //Implement validation of request
            if (startDate >= endDate)
                throw new Exception("Start date must be earlier than end date");

            var availableHalls = await conferenceHallRepository.GetAvailableConferenceHallsAsync(utcStart, utcEnd, requiredCapacity, ct);
            var response = mapper.Map<IEnumerable<ConferenceHallResponse>>(availableHalls);

            return response;
        }

        private ICollection<Amenities> MergeAmenities(
            IEnumerable<AmenitiesRequest> incomigAmenities,
            IEnumerable<Amenities> existingAmenities)
        {
            //Implement validation of request
            if (incomigAmenities == null)
                throw new Exception("Request cannot be null");

            var existingAmenitiesMap = existingAmenities.ToDictionary(a => a.Id, a => a);
            var incomingAmenitiesIds = incomigAmenities.Select(a => a.Id).ToHashSet();
            var result = new List<Amenities>();

            foreach (var incomingAmenity in incomigAmenities)
            {
                if (incomingAmenity.Id != null && existingAmenitiesMap.TryGetValue(incomingAmenity.Id.Value, out var existingAmenity))
                {
                    existingAmenity.Name = incomingAmenity.Name;
                    existingAmenity.Price = incomingAmenity.Price;
                    result.Add(existingAmenity);
                }
                else
                {
                    var newAmenity = mapper.Map<Amenities>(incomingAmenity);
                    result.Add(newAmenity);
                }
            }

            return result;
        }
    }
}
