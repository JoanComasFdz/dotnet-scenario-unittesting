using Microsoft.EntityFrameworkCore;

namespace ExampleAspNetCoreApplication;

public class SecondContext : DbContext
{
    public SecondContext(DbContextOptions<SecondContext> options) : base(options) { }
    public DbSet<SecondForecast> SecondForecasts { get; set; }

    public SecondContext()
    {
        // Necessary for unit testing, unfrotunately.
    }
}
