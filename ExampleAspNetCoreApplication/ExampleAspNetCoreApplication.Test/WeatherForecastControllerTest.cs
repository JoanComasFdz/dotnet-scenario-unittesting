using AutoFixture.Xunit2;
using FluentAssertions;
using JoanComas.ScenarioUnitTesting.AspNetCore;
using NSubstitute;
using Xunit;

namespace ExampleAspNetCoreApplication.Test;
/// <summary>
/// It contains 3 tests to make sure that each test has its own DbContext instances.
/// </summary>
public class WeatherForecastControllerTest
{
    [Theory, AutoData]
    public async void Get_Returns_All(ControllerScenario<WeatherForecastController> scenario)
    {
        scenario.ControllerContext().HttpContext.User.Identity?.Name.Returns("User1");
        
        var forecast = await scenario.When().Get();

        forecast.Should().NotBeNull().And.HaveCount(5);

        scenario.Dependency<WeatherContext>().Forecasts.Should().HaveCount(1);
    }

    [Theory, AutoData]
    public async void Get_Returns_All_2(ControllerScenario<WeatherForecastController> scenario)
    {
        scenario.ControllerContext().HttpContext.User.Identity?.Name.Returns("User1");

        var forecast = await scenario.When().Get();

        forecast.Should().NotBeNull().And.HaveCount(5);

        scenario.Dependency<WeatherContext>().Forecasts.Should().HaveCount(1);
    }


    [Theory, AutoData]
    public async void Get_Returns_All_3(ControllerScenario<WeatherForecastController> scenario)
    {
        scenario.ControllerContext().HttpContext.User.Identity?.Name.Returns("User1");

        var forecast = await scenario.When().Get();

        forecast.Should().NotBeNull().And.HaveCount(5);

        scenario.Dependency<WeatherContext>().Forecasts.Should().HaveCount(1);
    }
}