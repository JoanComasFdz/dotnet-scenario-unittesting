namespace ExampleApplicationWithConsole.Domain;

public class WebScrapper
{
    private readonly IHttpClient _client;

    public WebScrapper(IHttpClient client)
    {
        _client = client;
    }

    public string Scrap(string endpoint)
    {
        return _client.Get(endpoint);
    }

}

