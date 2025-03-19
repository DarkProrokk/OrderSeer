using Application.Command;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(IOrderService orderService, IMediator mediator): ControllerBase
{
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> Test()
    {
        await orderService.TestOrderProduce();
        return Ok();
    }


    [HttpPut]
    public async Task<IActionResult> CancelOrder(OrderStatusChangeCommand command)
    {
        var result = await mediator.Send(command);
        if (result.IsSuccess) Ok(result);
        return Conflict(result);
    }
}