using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Xunit;

namespace ScenarioUnitTesting.Tests
{
    public class ExampleTests
    {
        [Theory, AutoNSubstituteData]
        public void WhenNotifyIsCalled_AndNoAlerts_AnEmailIsSentWithEmptyBody(
            Scenario<NotificationService> scenario,
            string emailAddress)
        {
            scenario.When().Notify(emailAddress);

            scenario.Dependency<IEmailSender>()
                .Received()
                .SendTo(emailAddress, Arg.Is<string>(s => string.IsNullOrWhiteSpace(s)));
        }

        [Theory, AutoNSubstituteData]
        public void WhenNotifyIsCalled_AndThereIsAnAlert_AnEmailIsSentWithTheAlertDescription(
            Scenario<NotificationService> scenario,
            string emailAddress,
            Alert alert)
        {
            scenario.Dependency<IAlertsProvider>().GetAlerts().Returns(new [] { alert });

            scenario.When().Notify(emailAddress);

            scenario.Dependency<IEmailSender>()
                .Received()
                .SendTo(emailAddress, Arg.Is<string>(s => s.Contains(alert.Description)));
        }
    }

    #region Domain for this example

    public interface IEmailSender
    {
        public void SendTo(string emailAddress, string content);
    }

    public interface IAlertsProvider
    {
        public IReadOnlyCollection<Alert> GetAlerts();
    }

    public record Alert(string Description);

    public class NotificationService
    {
        private readonly IEmailSender _emailSender;
        private readonly IAlertsProvider _alertsProvider;

        public NotificationService(IEmailSender emailSender, IAlertsProvider alertsProvider)
        {
            _emailSender = emailSender;
            _alertsProvider = alertsProvider;
        }

        public void Notify(string emailAddress)
        {
            var alerts = this._alertsProvider.GetAlerts();
            var alertingText = string.Join(Environment.NewLine, alerts.Select(a => a.ToString()));
            this._emailSender.SendTo(emailAddress, alertingText);
        }
    } 
    #endregion
}