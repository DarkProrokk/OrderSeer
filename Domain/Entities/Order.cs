using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Primitives;
using Entities;

namespace Domain.Entities;

public class Order: Entity
{
    public Guid Guid { get; private set; }

    public int StatusId { get; private set; }

    public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; private set; } = new List<OrderStatusHistory>();

    public virtual Status Status { get; private set; } = null!;
    
    [NotMapped]
    public OrderStatus OrderStatus
    {
        get => (OrderStatus)StatusId;
        private set => StatusId = (int)value;
    }

    public void ChangeStatus(OrderStatus newStatus)
    {
        if (newStatus == OrderStatus) throw new WrongStatusException($"Status already is {newStatus}");
        if(!CanChangeStatus(newStatus))
            throw new WrongStatusException($"Cannot change order status from {OrderStatus} to {newStatus}");
        OrderStatus = newStatus;
    }

    private bool CanChangeStatus(OrderStatus newStatus)
    {
        return OrderStatus switch
        {
            OrderStatus.Pending => newStatus is OrderStatus.Processing or OrderStatus.Cancelled,
            OrderStatus.Processing => newStatus is OrderStatus.Shipped or OrderStatus.Cancelled,
            OrderStatus.Shipped => newStatus is OrderStatus.Delivered or OrderStatus.Cancelled,
            OrderStatus.Delivered => newStatus is OrderStatus.Returned,
            _ => false
        };
    }

    public static Order CreateOrder(Guid orderId)
    {
        if (orderId == Guid.Empty) throw new ArgumentException($"Order guid {orderId} cannot be empty");
        return new Order()
        {
            Guid = orderId,
            OrderStatus = OrderStatus.Pending
        };
    }
}
