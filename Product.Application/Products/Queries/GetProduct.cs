using MediatR;
using Product.Application.Common.Interfaces;
using ProductEntity = Product.Domain.Entities.Product;

namespace Product.Application.Products.Queries;

public record GetProductsQuery : IRequest<List<ProductDto>>;

public class GetProductsHandler(IRepository<ProductEntity> products) : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken ct)
    {
        var list =  await products.GetAllAsync(ct);
             
             return list.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            CreatedAt = p.CreatedAt
        }).ToList();
    }
}
