using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure;

public class UnitOfWork(OrderseerContext context, IOrderRepository orderRepository): IUnitOfWork, IAsyncDisposable
{
    public IOrderRepository OrderRepository { get; }= orderRepository;
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        context.Dispose();
    }
    

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
    }
}