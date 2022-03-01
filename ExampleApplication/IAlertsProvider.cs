namespace ExampleApplication;

public interface IAlertsProvider
{
    public IReadOnlyCollection<Alert> GetAlerts();
}