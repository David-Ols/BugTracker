using System;
using BugAPI.Models;
using BugAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BugAPI.Controllers
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
        [Route("/bugStatus")]
        public IActionResult HttpPut([FromBody] BugStatusUpdate request)
        {
            try
            {
                var isUpdated = _bugService.UpdateBugStatus(request);

                return Ok(isUpdated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest($"Failed to update bug status!");
            }

        }
    }
}

