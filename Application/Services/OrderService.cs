using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Domain.Interfaces;
using Entities;
using KafkaMessages;

namespace Application.Services;

public class OrderService(IUnitOfWork unitOfWork, IKafkaProducerService kafkaProducerService): IOrderService
{

    public async Task TestOrderProduce()
    {
        var cancellationToken = new CancellationTokenSource();
        var msg = new OrderStatusChangedEvent
        {
            OrderId = Guid.NewGuid(),
            Status = new Status
            {
                Code = OrderStatus.Pending,
                Name = "Pending"
            }
        };
        await kafkaProducerService.PlaceOrderAsync("order_status_changed", "key", msg, cancellationToken.Token);
    }
}