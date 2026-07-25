using ConferenceHallBooking.Application.Abstractions.IServices;
using ConferenceHallBooking.Application.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceHallBooking.Api.Controllers
{
    [ApiController]
    [Route("api/conference-halls")]
    public class ConferenceHallBookingController : ControllerBase
    {
        private readonly IConferenceHallService _conferenceHallService;
        private readonly IBookingService _bookingService;

        public ConferenceHallBookingController(
            IConferenceHallService conferenceHallService,
            IBookingService bookingService)
        {
            _conferenceHallService = conferenceHallService;
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateConferenceHall(CreateConferenceHallRequest request, CancellationToken ct = default)
        {
            var response = await _conferenceHallService.CreateConferanceHallAsync(request, ct);

            return Ok(response);
        }

        [HttpPut("{hallId:guid}")]
        public async Task<IActionResult> UpdateConferenceHall(UpdateConferenceHallRequest request, CancellationToken ct = default)
        {
            var response = await _conferenceHallService.UpdateConferenceHallAsync(request, ct);

            return Ok(response);
        }

        [HttpDelete("{hallId:guid}")]
        public async Task<IActionResult> DeleteConferenceHall(Guid hallId, CancellationToken ct = default)
        {
            await _conferenceHallService.DeleteConferenceHallAsync(hallId, ct);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> SeacrhAvailableConferenceHalls(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] int requiredCapicity,
            CancellationToken ct = default)
        {
            var reponse = await _conferenceHallService.SearchAvailableConferenceHallsAsync(startDate, endDate, requiredCapicity, ct);

            return Ok(reponse);
        }

        [HttpPost("booking")]
        public async Task<IActionResult> CreateBooking(CreateBookingRequest request, CancellationToken ct = default)
        {
            var response = await _bookingService.CreateBookingAsync(request, ct);
            return Ok(response);
        }
    }
}
