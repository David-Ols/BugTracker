using System;
using System.Xml.Linq;
using BugTracker.Models;
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _userService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest("Failed to get all users.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            try
            {
                if (user == null) return BadRequest("Request model is not valid!");
                if (string.IsNullOrWhiteSpace(user.Name)) return BadRequest("New name is not valid!");
                if (user.Id == Guid.Empty) return BadRequest("User Id is not valid!");

                var isUpdated = await _userService.UpdateUser(user);
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

