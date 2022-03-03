namespace ExampleApplicationWithConsole.Domain;

public interface IHttpClient
{
    public string Get(string endpoint);
}