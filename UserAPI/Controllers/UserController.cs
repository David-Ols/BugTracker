using System;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Entities;
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

        [HttpGet]
        [Route("/user")]
        public IActionResult Get()
        {
            try
            {
                var users = _userService.GetAll();

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest($"Failed to get all users!");
            }
        }

        [HttpPost]
        [Route("/user")]
        public IActionResult Post([FromBody] string name)
        {
            try
            {
                var user = _userService.Create(name);

                if (user == null) return BadRequest($"Failed to create user for name: {name}");

                return Ok(user);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest($"Failed to create user for name: {name}");
            }

        }

        [HttpPut]
        [Route("/user")]
        public IActionResult Put([FromBody] User user)
        {
            try
            {
                var isUpdated = _userService.Update(user);

                return Ok(isUpdated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest($"Failed to update user for name: {user.Name}");
            }

        }
    }
}

