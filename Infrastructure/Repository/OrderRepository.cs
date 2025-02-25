using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository;

public class OrderRepository(OrderseerContext context, ILogger<OrderRepository> logger): IOrderRepository
{
    public async Task<Order?> GetAsync(int orderId)
    {
        logger.LogInformation($"GetAsync called with ID {orderId}");
        return await context.Orders.FindAsync(orderId);
    }

    public async Task<Order?> GetAsync(Guid guid)
    {
        logger.LogInformation($"GetAsync called with GUID {guid}");
        return await context.Orders.FindAsync(guid);
    }

    public async Task<IEnumerable<Order>> GetByUserGuid(Guid guid)
    {
        return await context.Orders.Where(o => o.UserReference == guid).ToListAsync();
    }


    public async Task<IEnumerable<Order?>> GetOrdersByUserGuidAsync(Guid userReferenceGuid)
    {
        logger.LogInformation($"Get orders by userRefenceGuid {userReferenceGuid}");
        return await context.Orders.Where(o => o.UserReference == userReferenceGuid).ToListAsync();
    }
    
    public async Task AddAsync(Guid userReference, Guid orderId)
    {
        logger.LogInformation($"AddAsync called with userReference {userReference}, orderId {orderId}");
        try
        {
            Order order = Order.CreateOrder(userReference, orderId);
            await context.Orders.AddAsync(order);
        }
        catch (ArgumentException e)
        {
            logger.LogError(e.Message, "ArgumentException occured");
        }
    }
}