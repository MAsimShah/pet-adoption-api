using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetAdoption.API.Interfaces;
using PetAdoption.Application.Interfaces;

namespace PetAdoption.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController(IDashboardService _dashboardService, IUserContextService _userContext) : ControllerBase
    {
        [HttpGet("get-stats")]
        public async Task<IActionResult> GetStatsAsync()
        {
            var userId = _userContext.UserId;

            var stats = await _dashboardService.GetStatsAsync(userId, _userContext.IsAdmin);

            return Ok(stats);
        }
    }
}