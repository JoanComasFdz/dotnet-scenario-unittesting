// See https://aka.ms/new-console-template for more information

using ExampleApplicationWithConsole;
using ExampleApplicationWithConsole.Domain;
using JoanComas.ScenarioUnitTesting;
using NSubstitute;

Console.WriteLine("Hello, THIS IS A UNIT TEST EMULATOR because can't debug a Source Generator with an xUnit project!");

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