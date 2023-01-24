using System;
using BugTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
		{
            _logger = logger;
            _userService = userService;
		}

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string name)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(name))
                {
                    return BadRequest("Name can not be empty");
                }

                var user = await _userService.CreateUser(name);
                return Ok(user);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest($"Failed to create user for name: {name}");
            }

        }
    }
}

