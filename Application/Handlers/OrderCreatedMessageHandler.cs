using System.Text.Json;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Handlers;

public class OrderCreatedMessageHandler(IUnitOfWork unitOfWork, IKafkaProducerService kafkaProducerService, ILogger<OrderCreatedMessageHandler> logger): IMessageHandler
{
    public string Topic { get; } = "order_created";
    public async Task HandleMessage(string message, CancellationToken cancellationToken)
    {
        try
        {
            var orderData = JsonSerializer.Deserialize<KafkaOrderCreatedModel>(message);
            if (!orderData.Validate())
            {
                logger.LogError($"Invalid message received: {message}");
                await kafkaProducerService.ProduceInDlqAsync("error", "message was empty", cancellationToken);
                return;
            }
            await unitOfWork.OrderRepository.AddAsync(orderData.UserReference, orderData.OrderReference);
            await unitOfWork.SaveChangesAsync();
            logger.LogInformation($"OrderCreatedMessageHandler handled message");
        }
        catch (JsonException e)
        {
            logger.LogError($"Invalid message format received: {message}");
            await kafkaProducerService.ProduceInDlqAsync("error", "message was empty", cancellationToken);
        }
        
        


        #region test
        //
        // Random rand  = new Random();
        // for (int i = 0; i < 1000; i++)
        // {
        //     var userGuid = Guid.NewGuid();
        //     int randomNumber = rand.Next(2, 15);
        //     for (int j = 0; j < randomNumber; j++)
        //     {
        //         var orderGuid = Guid.NewGuid();
        //         await unitOfWork.OrderRepository.AddAsync(userGuid, orderGuid);
        //     }
        // }
        //
        #endregion
    }
}