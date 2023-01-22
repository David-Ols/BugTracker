using System;
using BugTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BugController : ControllerBase
    {
        private readonly ILogger<BugController> _logger;
        private readonly IBugService _bugService;

        public BugController(ILogger<BugController> logger, IBugService bugService)
		{
            _logger = logger;
            _bugService = bugService;
		}

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var bugs = await _bugService.GetAllBugs();
            return Ok(bugs);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Guid publicId)
        {
            var id = publicId;
            return Ok();
        }
    }
}

