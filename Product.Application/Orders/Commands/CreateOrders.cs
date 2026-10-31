using MediatR;
using Product.Application.Common;
using Product.Domain.Entities;

namespace Product.Application.Orders.Commands;

public record CreateOrdersCommand(OrderDto Order) : IRequest<int>;

public class CreateOrdersHandler(IApplicationDbContext db)
    : IRequestHandler<CreateOrdersCommand, int>
{
    public async Task<int> Handle(
        CreateOrdersCommand request,
        CancellationToken ct)
    {
        var order = new OrderEntity
        {
            OrderRef = request.Order.OrderRef,
            CustomerId = request.Order.CustomerId
        };

        db.Orders.Add(order);

        await db.SaveChangesAsync(ct);

        return order.Id;
    }
}
