using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Common;

namespace Product.Application.Customers.Queries;

public record GetCustomersQuery : IRequest<List<CustomerDto>>;

public class GetCustomersHandler(IApplicationDbContext db)
    : IRequestHandler<GetCustomersQuery, List<CustomerDto>>
{
    public async Task<List<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken ct)
    {
        return await db.Customers
            .AsNoTracking()
            .Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone
            })
            .ToListAsync(ct);
    }
}
