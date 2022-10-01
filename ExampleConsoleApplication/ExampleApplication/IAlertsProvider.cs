namespace ExampleConsoleApplication;

public interface IAlertsProvider
{
    public IReadOnlyCollection<Alert> GetAlerts();
}