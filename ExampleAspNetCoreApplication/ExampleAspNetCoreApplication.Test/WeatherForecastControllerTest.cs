using AutoFixture.Xunit2;
using ExampleAspNetCoreApplication.Controllers;
using FluentAssertions;
using JoanComas.ScenarioUnitTesting.AspNetCore;
using NSubstitute;
using Xunit;

namespace ExampleAspNetCoreApplication.Test
{
    public class WeatherForecastControllerTest
    {
        [Theory, AutoData]
        public void Get_Returns_All(ControllerScenario<WeatherForecastController> scenario)
        {
            scenario.ControllerContext().HttpContext.User.Identity?.Name.Returns("User1");
            
            var forecast = scenario.When().Get();

            forecast.Should().NotBeNull().And.HaveCount(5);
        }
    }
}