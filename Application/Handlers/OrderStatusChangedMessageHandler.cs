using Application.Interfaces;

namespace Application.Handlers;

public class OrderStatusChangedMessageHandler: IMessageHandler
{
    public string Topic { get; } = "order_status_changed";
    public async Task HandleMessage(string message, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}