using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Domain.Interfaces;

namespace Application.Services;

public class OrderService(IUnitOfWork unitOfWork): IOrderService
{
    public async Task<IEnumerable<OrderViewModel>> GetByUserGuid(Guid userGuid)
    {
        var order = await unitOfWork.OrderRepository.GetByUserGuid(userGuid);
        return order.ToOrderViewModel();
    }
}