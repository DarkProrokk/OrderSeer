using Domain.Entities;
using Domain.Enum;

namespace Domain.Interfaces;

public interface IOrderRepository
{
    public Task<Order?> GetAsync(int orderId);
    
    public Task<Order?> GetAsync(Guid guid);

    public Task<IEnumerable<Order?>> GetByUserGuid(Guid userReferenceGuid);

    public Task AddAsync(Guid userReference, Guid orderId);
}