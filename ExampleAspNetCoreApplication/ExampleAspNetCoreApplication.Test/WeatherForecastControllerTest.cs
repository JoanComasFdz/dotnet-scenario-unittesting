using AutoFixture.Xunit2;
using FluentAssertions;
using JoanComas.ScenarioUnitTesting.AspNetCore;
using NSubstitute;
using Xunit;

namespace ExampleAspNetCoreApplication.Test;

public class WeatherForecastControllerTest
{
    [Theory, AutoData]
    public async void Get_Returns_All(ControllerScenario<WeatherForecastController> scenario)
    {
        scenario.ControllerContext().HttpContext.User.Identity?.Name.Returns("User1");
        
        var forecast = await scenario.When().Get();

        forecast.Should().NotBeNull().And.HaveCount(5);
    }
}