using Application.Command;
using KafkaMessages;
using Orderseer.Common.Models;

namespace Infrastructure.Mappers;

public static class Mapper
{
    public static OrderStatusChangeCommand Map(OrderStatusChangedEvent changedEvent)
    {
        return new OrderStatusChangeCommand(changedEvent.OrderId, changedEvent.Status.Code);
    }
    
    public static OrderStatusChangedEvent Map(OrderStatusChangeCommand command)
    {
        return new OrderStatusChangedEvent
        {
            OrderId = command.OrderGuid,
            Status = new Status
            {
                Code = command.NewStatus,
                Name = command.NewStatus.ToString()
            }
        };
    }
}