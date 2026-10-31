using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Common.Interfaces;
using ProductEntity = Product.Domain.Entities.Product;

namespace Product.Application.Products.Queries;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;

public class GetProductByIdHandler(IRepository<ProductEntity> products)
    : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken ct)
    {
         var product = await products.GetByIdAsync(request.Id, ct); 

         if (product is null)
        return null;
        
           return new ProductDto 
            { 
                Id = product.Id, 
                Name = product.Name, 
                Price = product.Price, 
                CreatedAt = product.CreatedAt 
            };
            
    }
}
