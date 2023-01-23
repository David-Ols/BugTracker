using System;
using BugTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BugListController : ControllerBase
    {
        private readonly ILogger<BugListController> _logger;
        private readonly IBugService _bugService;

        public BugListController(ILogger<BugListController> logger, IBugService bugService)
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
    }
}

