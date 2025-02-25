using Domain.Primitives;

namespace Domain.Entities;

public class Status: Entity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<OrderStatusHistory> OrderStatusHistoryOldStatuses { get; set; } = new List<OrderStatusHistory>();

    public virtual ICollection<OrderStatusHistory> OrderStatusHistoryStatuses { get; set; } = new List<OrderStatusHistory>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
