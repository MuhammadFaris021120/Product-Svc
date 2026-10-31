using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Orders.Commands;

namespace Product.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrdersCommand command, CancellationToken ct)
    {
       var orderId = await sender.Send(command, ct);
       return Ok(orderId);
    }
}
