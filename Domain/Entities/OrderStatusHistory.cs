using Domain.Primitives;

namespace Domain.Entities;

public class OrderStatusHistory: Entity
{

    public int? OrderId { get; set; }

    public int? StatusId { get; set; }

    public DateTime? ChangeDate { get; set; }

    public int? OldStatusId { get; set; }

    public virtual Status? OldStatus { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Status? Status { get; set; }
}
