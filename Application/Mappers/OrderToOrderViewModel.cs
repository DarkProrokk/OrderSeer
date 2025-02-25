using Application.Models;
using Domain.Entities;

namespace Application.Mappers;

public static class OrderToOrderViewModel
{
    public static OrderViewModel ToOrderViewModel(this Order order)
    {
        return new OrderViewModel
        {
            OrderId = order.Guid,
            Status = order.OrderStatus.ToString(),
        };
    }

    public static IEnumerable<OrderViewModel> ToOrderViewModel(this IEnumerable<Order> orders)
    {
        return orders.Select(ToOrderViewModel);
    }
}