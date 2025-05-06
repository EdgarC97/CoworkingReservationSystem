using CoworkingReservationSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoworkingReservationSystem.Controllers
{
    [ApiController]
    [Route("api/audits")]
    [Authorize]
    public class AuditController : ControllerBase
    {
        private readonly IAuditService _auditService;

        public AuditController(IAuditService auditService)
        {
            _auditService = auditService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAudits()
        {
            try
            {
                var result = await _auditService.GetAllAuditsAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAuditsByUserId(int userId)
        {
            try
            {
                var result = await _auditService.GetAuditsByUserIdAsync(userId);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpGet("entity/{entityName}")]
        public async Task<IActionResult> GetAuditsByEntityName(string entityName)
        {
            try
            {
                var result = await _auditService.GetAuditsByEntityNameAsync(entityName);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request" });
            }
        }
    }
}