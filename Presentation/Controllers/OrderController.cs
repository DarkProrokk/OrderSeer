using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(IOrderRepository orderRepository): ControllerBase
{
    [HttpGet("byGuid/{userGuid}")]
    public async Task<IActionResult> GetOrderByUserGuid(Guid userGuid)
    {
        var orders = await orderRepository.GetOrdersByUserGuidAsync(userGuid);
        return Ok(orders);
    }
}