using MediatR;
using Product.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;
using ProductEntity = Product.Domain.Entities.Product;

namespace Product.Application.Products.Commands;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.product.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.product.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}

public record CreateProductCommand(CreateProductRequest product) : IRequest<int>;

public class CreateProductHandler(IRepository<ProductEntity> products, ILogger<CreateProductHandler> logger): IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken ct)
    {
        var product = new ProductEntity { Name = request.product.Name, Price = request.product.Price };
        await products.AddAsync(product, ct);
        logger.LogInformation("Product created with Id {ProductId} and Name {ProductName}", product.Id, product.Name);

        return product.Id;
    }
}
