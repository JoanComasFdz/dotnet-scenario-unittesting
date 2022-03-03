// See https://aka.ms/new-console-template for more information

using ExampleApplicationWithConsole;
using ExampleApplicationWithConsole.Domain;
using ExampleApplicationWithConsole.Tests;
using NSubstitute;
using ScenarioUnitTesting;

Console.WriteLine("Hello, THIS IS A UNIT TEST EMULATOR because can't debug a Source Generator with an xUnit project!");

public class WebScrapperTest
{
    public void WhenScrapIsCalled_HttpClientGetsUrl(
        WebScrapperScenario scenario,
        string url)
    {
        scenario.WebScrapper.Scrap(url);

        scenario.Dependency<IHttpClient>()
            .Received()
            .Get(url);
    }
}

public class NotificationServiceTest
{
    public void WhenNotifyIsCalled_AndThereIsAnAlert_AnEmailIsSentWithTheAlertDescription(
        NotificationServiceScenario scenario,
        string emailAddress,
        Alert alert)
    {
        scenario.Dependency<IAlertsProvider>()
            .GetAlerts()
            .Returns(new[] { alert });

        scenario.NotificationService.Notify(emailAddress);

        scenario.Dependency<IEmailSender>()
            .Received()
            .SendTo(emailAddress, Arg.Is<string>(s => s.Contains(alert.Description)));
    }
}