using ConferenceHallBooking.Application.Abstractions.IServices;
using ConferenceHallBooking.Application.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceHallBooking.Api.Controllers
{
    [ApiController]
    [Route("api/conference-halls")]
    public class ConferenceHallBookingController(IConferenceHallService conferenceHallService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateConferenceHall(CreateConferenceHallRequest request, CancellationToken ct = default)
        {
            var response = await conferenceHallService.CreateConferanceHallAsync(request, ct);

            return Ok(response);
        }

        [HttpPut("{hallId:guid}")]
        public async Task<IActionResult> UpdateConferenceHall(UpdateConferenceHallRequest request, CancellationToken ct = default)
        {
            var response = await conferenceHallService.UpdateConferenceHallAsync(request, ct);

            return Ok(response);
        }

        [HttpDelete("{hallId:guid}")]
        public async Task<IActionResult> DeleteConferenceHall(Guid hallId, CancellationToken ct = default)
        {
            await conferenceHallService.DeleteConferenceHallAsync(hallId, ct);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> SeacrhAvailableConferenceHalls(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] int requiredCapicity,
            CancellationToken ct = default)
        {
            var reponse = await conferenceHallService.SearchAvailableConferenceHallsAsync(startDate, endDate, requiredCapicity, ct);

            return Ok(reponse);
        }
    }
}
