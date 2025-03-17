using System.Text.Json;
using Application.Interfaces;
using Entities;
using Domain.Interfaces;
using KafkaMessages;
using Microsoft.Extensions.Logging;

namespace Application.Handlers;

public class OrderStatusChangedMessageHandler(IUnitOfWork unitOfWork, IKafkaProducerService kafkaProducerService, ILogger<OrderStatusChangedMessageHandler> logger): IMessageHandler
{
    public string Topic { get; } = "order_status_changed";
    public async Task HandleMessage(string message, CancellationToken cancellationToken)
    {
        try
        {
            var orderData = JsonSerializer.Deserialize<OrderStatusChangedEvent>(message);

            // logger.LogError($"Invalid message received: {message}");
            // await kafkaProducerService.ProduceInDlqAsync("error", "message was empty", cancellationToken);
            // return;
            var order = await unitOfWork.OrderRepository.GetAsync(orderData.OrderId);
            OrderStatus newStatus = (OrderStatus)orderData.Status.code;
            order.ChangeStatus(newStatus);
            unitOfWork.OrderRepository.Update(order);
            await unitOfWork.SaveChangesAsync();
            logger.LogInformation($"OrderStatusChangedMessageHandler handled message");
        }
        catch (JsonException e)
        {
            logger.LogError($"Invalid message format received: {message}");
            await kafkaProducerService.ProduceInDlqAsync("error", "message was empty", cancellationToken);
        }
    }
}