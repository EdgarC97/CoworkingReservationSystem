using CoworkingReservationSystem.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoworkingReservationSystem.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public UserController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("{id}/bookings")]
        public async Task<IActionResult> GetUserBookings(int id)
        {
            try
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                // Only allow users to see their own bookings
                if (currentUserId != id)
                {
                    return Forbid();
                }

                var result = await _bookingService.GetUserBookingsAsync(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request" });
            }
        }
    }
}