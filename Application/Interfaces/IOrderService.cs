using Application.Models;

namespace Application.Interfaces;

public interface IOrderService
{
    public Task<IEnumerable<OrderViewModel>> GetByUserGuid(Guid userGuid);
}