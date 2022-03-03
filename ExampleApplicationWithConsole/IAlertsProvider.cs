namespace ExampleApplicationWithConsole;

public interface IAlertsProvider
{
    public IReadOnlyCollection<Alert> GetAlerts();
}