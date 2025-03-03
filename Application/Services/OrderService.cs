using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Domain.Interfaces;

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
        var msg = new KafkaOrderStatusChangedModel();
        msg.OrderId = Guid.NewGuid();
        var status = new Status();
        status.code = 1;
        status.name = "Pending";
        msg.Status = status;
        await kafkaProducerService.PlaceOrderAsync("order_status_changed", "key", msg, cancellationToken.Token);
    }
}