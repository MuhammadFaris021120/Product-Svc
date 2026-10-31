using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Common;

namespace Product.Application.Customers.Queries;

public record GetCustomerByIdQuery(int Id) : IRequest<CustomerDto?>;

public class GetCustomerByIdHandler(IApplicationDbContext db)
    : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken ct)
    {
        return await db.Customers
            .AsNoTracking()
            .Where(c => c.Id == request.Id)
            .Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone
            })
            .FirstOrDefaultAsync(ct);
    }
}
