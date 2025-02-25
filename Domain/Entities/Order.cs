using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enum;
using Domain.Primitives;

namespace Domain.Entities;

public class Order: Entity
{
    public Guid Guid { get; private set; }

    public Guid UserReference { get; private set; }

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
        if(!CanChangeStatus(newStatus))
            throw new ArgumentException($"Cannot change order status from {OrderStatus} to {newStatus}");
        OrderStatus = newStatus;
    }

    private bool CanChangeStatus(OrderStatus newStatus)
    {
        return newStatus is OrderStatus.Cancelled || OrderStatus switch
        {
            OrderStatus.Pending => newStatus is OrderStatus.Processing or OrderStatus.Cancelled,
            OrderStatus.Processing => newStatus is OrderStatus.Pending,
            OrderStatus.Shipped => newStatus is OrderStatus.Delivered,
            _ => false
        };
    }

    public static Order CreateOrder(Guid userId, OrderStatus orderStatus)
    {
        if (userId == Guid.Empty) throw new ArgumentException($"User {userId} cannot be empty");
        if (!CanCreateOrder(orderStatus)) 
            throw new ArgumentException($"Cannot create order with status {orderStatus}." +
                                        $" Initial order status must be is {OrderStatus.Pending}.");
        return new Order()
        {
            Guid = Guid.NewGuid(),
            UserReference = userId,
            OrderStatus = orderStatus
        };
    }

    private static bool CanCreateOrder(OrderStatus newStatus)
    {
        return newStatus is OrderStatus.Pending;
    }
}
