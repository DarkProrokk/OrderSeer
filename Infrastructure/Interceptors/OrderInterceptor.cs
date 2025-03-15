using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Status = KafkaMessages.Status;

namespace Infrastructure.Interceptors;

public class OrderCreatedInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var context = eventData.Context;
        if (context == null) return result;

        var newOrders = context.ChangeTracker.Entries<Order>()
            .Where(e => e.State == EntityState.Added)
            .Select(e => e.Entity)
            .ToList();

        if (newOrders.Any())
        {
            foreach (var order in newOrders)
            {
                var historyEntries = new OrderStatusHistory
                {
                    OrderId = order.Id, // ID ещё нет, но EF сам проставит
                    OldStatusId = null,
                    StatusId = order.StatusId,
                };
                order.OrderStatusHistories.Add(historyEntries);
            }
        }

        return result;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        return new ValueTask<InterceptionResult<int>>(SavingChanges(eventData, result));
    }

}
