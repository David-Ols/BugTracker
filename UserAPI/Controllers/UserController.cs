using System;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Services.Interfaces;

namespace UserAPI.Controllers
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

        [HttpGet]
        [Route("/user/{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var user = _userService.GetById(id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest($"Failed to get user for id: {id}!");
            }

        }
    }
}

