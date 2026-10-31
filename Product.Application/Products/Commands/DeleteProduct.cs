using MediatR;
using Product.Application.Common.Interfaces;
using ProductEntity = Product.Domain.Entities.Product;

namespace Product.Application.Products.Commands;

public record DeleteProductCommand(int Id) : IRequest<bool>;

public class DeleteProductHandler(IRepository<ProductEntity> products)
    : IRequestHandler<DeleteProductCommand, bool>
{
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken ct)
    {
        var product = await products.GetByIdAsync(request.Id, ct);
        if (product is null) return false;

        await products.DeleteAsync(product, ct);
        return true;
    }
}
