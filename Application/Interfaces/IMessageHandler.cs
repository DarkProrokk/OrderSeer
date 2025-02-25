namespace Application.Interfaces;

public interface IMessageHandler
{
    public string Topic { get; }
    Task HandleMessage(string message, CancellationToken cancellationToken);
}