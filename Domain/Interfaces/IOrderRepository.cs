using Domain.Entities;
using Domain.Enum;

namespace Domain.Interfaces;

public interface IOrderRepository
{
    public Task<Order?> GetAsync(int orderId);
    
    public Task<Order?> GetAsync(Guid guid);

    public Task AddAsync(Guid orderId);
    
    public void Update(Order order);
}