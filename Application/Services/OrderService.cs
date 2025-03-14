using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Domain.Interfaces;
using KafkaMessages;

namespace Application.Services;

public class OrderService(IUnitOfWork unitOfWork, IKafkaProducerService kafkaProducerService): IOrderService
{
    public async Task<IEnumerable<OrderViewModel>> GetByUserGuid(Guid userGuid)
    {
        var order = await unitOfWork.OrderRepository.GetByUserGuid(userGuid);
        return order.ToOrderViewModel();
    }

    public async Task TestOrderProduce()
    {
        var cancellationToken = new CancellationTokenSource();
        var msg = new OrderStatusChangedEvent
        {
            OrderId = Guid.NewGuid(),
            Status = new Status
            {
                code = 1,
                name = "Pending"
            }
        };
        await kafkaProducerService.PlaceOrderAsync("order_status_changed", "key", msg, cancellationToken.Token);
    }
}