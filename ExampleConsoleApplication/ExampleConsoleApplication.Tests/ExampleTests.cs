using AutoFixture.Xunit2;
using NSubstitute;
using JoanComas.ScenarioUnitTesting;
using Xunit;

namespace ExampleConsoleApplication.Tests;

public class ExampleTests
{
    [Theory, AutoData]
    public void WhenNotifyIsCalled_AndNoAlerts_AnEmailIsSentWithEmptyBody(
        Scenario<NotificationService> scenario,
        string emailAddress)
    {
        scenario.When().Notify(emailAddress);

        scenario.Dependency<IEmailSender>()
            .Received()
            .SendTo(emailAddress, Arg.Is<string>(s => string.IsNullOrWhiteSpace(s)));
    }

    [Theory, AutoData]
    public void WhenNotifyIsCalled_AndThereIsAnAlert_AnEmailIsSentWithTheAlertDescription(
        Scenario<NotificationService> scenario,
        string emailAddress,
        Alert alert)
    {
        scenario.Dependency<IAlertsProvider>().GetAlerts().Returns([alert]);

        scenario.When().Notify(emailAddress);

        scenario.Dependency<IEmailSender>()
            .Received()
            .SendTo(emailAddress, Arg.Is<string>(s => s.Contains(alert.Description)));
    }
}