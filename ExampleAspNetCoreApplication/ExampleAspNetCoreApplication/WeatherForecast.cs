using System.Text.Json.Serialization;

namespace ExampleAspNetCoreApplication;

public class WeatherForecast
{
    // This is required to make the InMemory DBContext customizaxtion works.
    // A primary key is needed.
    // The JsonIgnore attribute is only to avoid it being returned by the API.
    [JsonIgnore]
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}