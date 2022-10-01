namespace ExampleConsoleApplication;

public interface IEmailSender
{
    public void SendTo(string emailAddress, string content);
}