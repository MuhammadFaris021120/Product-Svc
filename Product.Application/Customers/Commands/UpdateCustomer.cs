using MediatR;
using Product.Application.Common;

namespace Product.Application.Customers.Commands;

public record UpdateCustomerCommand(int Id, string Name, string Email, string Phone) : IRequest<bool>;

public class UpdateCustomerHandler(IApplicationDbContext db)
    : IRequestHandler<UpdateCustomerCommand, bool>
{
    public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken ct)
    {
        var customer = await db.Customers.FindAsync([request.Id], ct);
        if (customer is null) return false;

        customer.Name = request.Name;
        customer.Email = request.Email;
        customer.Phone = request.Phone;
        await db.SaveChangesAsync(ct);
        return true;
    }
}
