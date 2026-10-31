using MediatR;
using Product.Application.Common;
using Product.Domain.Entities;

namespace Product.Application.Customers.Commands;

public record CreateCustomerCommand(string Name, string Email, string Phone) : IRequest<int>;

public class CreateCustomerHandler(IApplicationDbContext db)
    : IRequestHandler<CreateCustomerCommand, int>
{
    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken ct)
    {
        var customer = new Customer { Name = request.Name, Email = request.Email, Phone = request.Phone };
        db.Customers.Add(customer);
        await db.SaveChangesAsync(ct);
        return customer.Id;
    }
}
