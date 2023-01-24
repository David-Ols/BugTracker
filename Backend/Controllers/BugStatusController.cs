using System;
using BugTracker.Models;
using BugTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BugStatusController : ControllerBase
    {
        private readonly ILogger<BugStatusController> _logger;
        private readonly IBugService _bugService;

        public BugStatusController(ILogger<BugStatusController> logger, IBugService bugService)
		{
            _logger = logger;
            _bugService = bugService;
		}


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateBugStatus request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Status)) return BadRequest("Status can not be empty!");
                if (request.BugId == Guid.Empty) return BadRequest("Bug Id is not valid!");

                var isUpdated = await _bugService.UpdateBugStatus(request);
                return Ok(isUpdated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest($"Failed to update user {user.Name}.");
            }
        }
    }
}

