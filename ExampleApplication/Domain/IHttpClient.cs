namespace ExampleApplication.Domain;

public interface IHttpClient
{
    public string Get(string endpoint);
}