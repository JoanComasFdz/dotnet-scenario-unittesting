using AutoFixture.Xunit2;
using ExampleApplication.Domain;
using NSubstitute;
using Xunit;

namespace ExampleApplication.Tests;

public class ExampleTests
{
    [Theory, AutoData]
    public void WhenScrapIsCalled_HttpClientGetsUrl(
        WebScrapperScenario scenario,
        string url)
    {
        scenario.WebScrapper.Scrap(url);
        
        scenario.Dependency<IHttpClient>()
            .Received()
            .Get(url);
    }

    [Theory, AutoData]
    public void WhenNotifyIsCalled_AndThereIsAnAlert_AnEmailIsSentWithTheAlertDescription(
        NotificationServiceScenario scenario,
        string emailAddress,
        Alert alert)
    {
        scenario.Dependency<IAlertsProvider>().GetAlerts().Returns(new [] { alert });

        scenario.NotificationService.Notify(emailAddress);

        scenario.Dependency<IEmailSender>()
            .Received()
            .SendTo(emailAddress, Arg.Is<string>(s => s.Contains(alert.Description)));
    }
}