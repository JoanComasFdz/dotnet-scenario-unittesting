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

    // Two DbContext are used to make sure the ControllerScenario can handle that.
    private readonly WeatherContext weatherContext;
    private readonly SecondContext secondContext;

    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        WeatherContext weatherContext,
        SecondContext secondContext)
    {
        _logger = logger;
        this.weatherContext = weatherContext;
        this.secondContext = secondContext;
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
        var forecasts = weatherContext.Forecasts.ToArray();

        /// Make sure two DbContext work perfectly
        secondContext.SecondForecasts.Add(new SecondForecast { Name = "first second" });
        await secondContext.SaveChangesAsync();
        var secondForecasts = secondContext.SecondForecasts.ToArray();  

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}