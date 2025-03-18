using System.Text.Json;
using Application.Interfaces;
using Domain.Interfaces;
using KafkaMessages;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Handlers;

public class OrderCreatedMessageHandler(IUnitOfWork unitOfWork, IKafkaProducerService kafkaProducerService, ILogger<OrderCreatedMessageHandler> logger): IMessageHandler
{
    public string Topic { get; } = "order_created";
    public async Task HandleMessage(string message, CancellationToken cancellationToken)
    {
        try
        {
            var orderData = JsonSerializer.Deserialize<OrderCreatedForProcessingEvent>(message);
            if (!orderData.Validate())
            {
                logger.LogError($"Invalid message received: {message}");
                await kafkaProducerService.ProduceInDlqAsync("error", "message was empty", cancellationToken);
                return;
            }
            await unitOfWork.OrderRepository.AddAsync(orderData.OrderReference);
            await unitOfWork.SaveChangesAsync();
            logger.LogInformation($"OrderCreatedMessageHandler handled message");
        }
        catch (JsonException e)
        {
            logger.LogError($"Invalid message format received: {message}");
            await kafkaProducerService.ProduceInDlqAsync("error", "message was empty", cancellationToken);
        }
    }
}