using Microsoft.EntityFrameworkCore;

namespace ExampleAspNetCoreApplication;

public class WeatherContext : DbContext
{
    public WeatherContext(DbContextOptions<WeatherContext> options) : base(options) { }
    public DbSet<WeatherForecast> Forecasts { get; set; }

    public WeatherContext()
    {
        // Necessary for unit testing, unfrotunately.
    }
}
