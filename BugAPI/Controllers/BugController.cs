using System;
using BugAPI.Entities;
using BugAPI.Models;
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

        [HttpPost]
        [Route("/bug")]
        public IActionResult Post([FromBody] CreateBug bug)
        {
            try
            {
                var newBug = _bugService.Create(bug);

                if (newBug == null) return BadRequest($"Failed to create bug!");

                return Ok(newBug);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest($"Failed to create bug!");
            }

        }

        [HttpPut]
        [Route("/bug")]
        public IActionResult Put([FromBody] BugUpdate bug)
        {
            try
            {
                var isUpdated = _bugService.Update(bug);

                return Ok(isUpdated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest($"Failed to create bug!");
            }

        }
    }
}

