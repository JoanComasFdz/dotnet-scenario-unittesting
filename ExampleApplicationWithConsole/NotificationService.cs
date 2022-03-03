namespace ExampleApplicationWithConsole;

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

