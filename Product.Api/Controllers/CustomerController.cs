using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Customers.Commands;
using Product.Application.Customers.Queries;

namespace Product.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await sender.Send(new GetCustomersQuery(), ct));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var customer = await sender.Send(new GetCustomerByIdQuery(id), ct);
        return customer is null ? NotFound() : Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerCommand command, CancellationToken ct)
    {
        var id = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCustomerCommand command, CancellationToken ct)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        return await sender.Send(command, ct) ? NoContent() : NotFound();
    }

}
