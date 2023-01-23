using System;
using BugAPI.Entities;
using BugAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BugAPI.Controllers
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
        [Route("/bug")]
        public IActionResult Get()
        {
            try
            {
                var bugs = _bugService.GetAllBugs();

                return Ok(bugs);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest("Failed to get all bugs!");
            }

        }

        [HttpGet]
        [Route("/bug/{publicId}")]
        public IActionResult GetByPublicId(string publicId)
        {
            try
            {
                var bug = _bugService.GetByPublicId(publicId);

                return Ok(bug);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest("Failed to get all bugs!");
            }

        }
    }
}

