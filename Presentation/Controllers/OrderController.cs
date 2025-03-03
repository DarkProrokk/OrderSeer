using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(IOrderService orderService): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetByUserGuid([FromQuery]Guid userGuid)
    {
        var orders = await orderService.GetByUserGuid(userGuid);
        return Ok(orders);
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> Test()
    {
        await orderService.TestOrderProduce();
        return Ok();
    }
}