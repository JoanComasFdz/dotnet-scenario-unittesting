using AutoFixture.Xunit2;
using ExampleApplication;
using ExampleApplication.Domain;
using JoanComas.ScenarioUnitTesting;
using NSubstitute;
using Xunit;

namespace ExampleApplicationWithxUnit.Tests;

public class ExampleTests
{
    [Theory, AutoData]
    public void WhenScrapIsCalled_HttpClientGetsUrl(
        //WebScrapperScenario scenario,
        Scenario<WebScrapper> genericScenario,
        string url)
    {
        //scenario.WebScrapper.Scrap(url);
        genericScenario.When().Scrap(url);

        genericScenario.Dependency<IHttpClient>()
            .Received()
            .Get(url);
    }

    [Theory, AutoData]
    public void WhenNotifyIsCalled_AndThereIsAnAlert_AnEmailIsSentWithTheAlertDescription(
        //NotificationServiceScenario scenario,
        Scenario<NotificationService> genericScenario,
        string emailAddress,
        Alert alert)
    {
        genericScenario.Dependency<IAlertsProvider>().GetAlerts().Returns(new[] { alert });

        //scenario.NotificationService.Notify(emailAddress);
        genericScenario.When().Notify(emailAddress);

        genericScenario.Dependency<IEmailSender>()
            .Received()
            .SendTo(emailAddress, Arg.Is<string>(s => s.Contains(alert.Description)));
    }
}