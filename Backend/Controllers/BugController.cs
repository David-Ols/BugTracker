using System;
using BugTracker.Models;
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
        public async Task<IActionResult> Get(string publicId)
        {
            var bug = await _bugService.GetByPublicId(publicId);
            return Ok(bug);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBug bug)
        {
            try
            {
                if (bug == null) return BadRequest("Request object is null!");
                if(string.IsNullOrWhiteSpace(bug.Title)) return BadRequest("Title is required!");
                if (string.IsNullOrWhiteSpace(bug.Description)) return BadRequest("Description is required!");

                var newBug = await _bugService.CreateBug(bug);

                if (newBug == null) return BadRequest("Failed to create bug!");

                return Ok(newBug);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest($"Failed to create bug!");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BugUpdate request)
        {
            try
            {
                if (request == null) return BadRequest("Invalid request!");
                if(string.IsNullOrWhiteSpace(request.Status)) return BadRequest("Status is required!");
                if (string.IsNullOrWhiteSpace(request.Title)) return BadRequest("Title is required!");
                if (string.IsNullOrWhiteSpace(request.Description)) return BadRequest("Description is required");
                if (request.BugId == Guid.Empty) return BadRequest("Invalid bugId!");

                var isUpdated = await _bugService.Update(request);

                return Ok(isUpdated);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest($"Failed to update bug!");
            }
        }
    }
}

