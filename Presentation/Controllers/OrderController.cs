using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(IOrderService orderService): ControllerBase
{
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> Test()
    {
        await orderService.TestOrderProduce();
        return Ok();
    }
}