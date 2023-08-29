using Microsoft.AspNetCore.Mvc;

namespace ExampleAspNetCoreApplication;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly WeatherContext weatherContext;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherContext weatherContext)
    {
        _logger = logger;
        this.weatherContext = weatherContext;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        /// This code is here just to make sure that the InMemory DBContext customizaxtion works
        weatherContext.Forecasts.Add(new WeatherForecast
        {
            Date = DateTime.Now
        });
        await weatherContext.SaveChangesAsync();
        var arr = weatherContext.Forecasts.ToArray();

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}