using JoanComas.ScenarioUnitTesting;
using NSubstitute;

namespace ExampleApplicationWithConsole.Tests;

public class NotificationServiceTest
{
    public void WhenNotifyIsCalled_AndThereIsAnAlert_AnEmailIsSentWithTheAlertDescription(
        // NotificationServiceScenario scenario,
        Scenario<NotificationService> genericScenario,

        string emailAddress,
        Alert alert)
    {
        //scenario.Dependency<IAlertsProvider>()
        genericScenario.Dependency<IAlertsProvider>()
            .GetAlerts()
            .Returns(new[] { alert });

        //scenario.NotificationService.Notify(emailAddress);
        genericScenario.When().Notify(emailAddress);

        genericScenario.Dependency<IEmailSender>()
            .Received()
            .SendTo(emailAddress, Arg.Is<string>(s => s.Contains(alert.Description)));
    }
}