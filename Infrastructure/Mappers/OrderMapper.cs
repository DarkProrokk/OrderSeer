using Application.Models;
using Infrastructure.Interfaces;
using Scheme.OrderStatusChanged;


namespace Infrastructure.Mappers;

public class OrderMapper: IMapper
{
    public OrderStatusChange ToAvroModel(KafkaOrderStatusChangedModel model)
    {
        Scheme.OrderStatusChanged.Status status = new Scheme.OrderStatusChanged.Status();
        status.code = model.Status.code;
        status.name = model.Status.name;
        return new OrderStatusChange
        {
            orderId = model.OrderId.ToString(),
            status = status
        };
    }
}