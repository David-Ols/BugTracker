using BugAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BugAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Bug> Get()
    {
        var bugs = new List<Bug>()
        {
            new Bug{
                Id = Guid.NewGuid(),
                Description = "Description",
                OpenedOn = DateTime.Now,
                PublicId = "Bug-1",
                Status = "Status",
                Title = "Title"
            }
        };

        return bugs;
    }
}

