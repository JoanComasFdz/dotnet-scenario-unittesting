using ExampleApplicationWithConsole.Domain;
using JoanComas.ScenarioUnitTesting;
using NSubstitute;

namespace ExampleApplicationWithConsole.Tests;

public class WebScrapperTest
{
    public void WhenScrapIsCalled_HttpClientGetsUrl(
        // WebScrapperScenario scenario,
        Scenario<WebScrapper> genericScenario,
        string url)
    {
        //scenario.WebScrapper.Scrap(url);
        genericScenario.When().Scrap(url);

        genericScenario.Dependency<IHttpClient>()
            .Received()
            .Get(url);
    }
}