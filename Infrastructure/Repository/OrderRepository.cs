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
    
    public async Task AddAsync(Guid orderId)
    {
        logger.LogInformation($"AddAsync called with orderId {orderId}");
        try
        {
            Order order = Order.CreateOrder(orderId);
            await context.Orders.AddAsync(order);
        }
        catch (ArgumentException e)
        {
            logger.LogError(e.Message, "ArgumentException occured");
        }
    }
}