using ConferenceHallBooking.Application.DTOs.Requests;
using ConferenceHallBooking.Application.DTOs.Responses;

namespace ConferenceHallBooking.Application.Abstractions.IServices
{
    public interface IConferenceHallService
    {
        Task<ConferenceHallResponse> CreateConferanceHallAsync(CreateConferenceHallRequest request, CancellationToken ct = default);
        Task<bool> DeleteConferenceHallAsync(Guid hallId, CancellationToken ct = default);
        Task<IEnumerable<ConferenceHallResponse>> SearchAvailableConferenceHallsAsync(DateTime startDate, DateTime endDate, int requiredCapacity, CancellationToken ct = default);
        Task<ConferenceHallResponse> UpdateConferenceHallAsync(UpdateConferenceHallRequest request, CancellationToken ct = default);
    }
}