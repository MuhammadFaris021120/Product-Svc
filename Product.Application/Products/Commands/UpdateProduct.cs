using MediatR;
using Product.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;
using ProductEntity = Product.Domain.Entities.Product;

namespace Product.Application.Products.Commands;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.product.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.product.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}
public record UpdateProductCommand(ProductDto product) : IRequest<bool>;

public class UpdateProductHandler(IRepository<ProductEntity> products)
    : IRequestHandler<UpdateProductCommand, bool>
{
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken ct)
    {
        var product = await products.GetByIdAsync(request.product.Id, ct);
        if (product is null) return false;

        product.Name = request.product.Name;
        product.Price = request.product.Price;
        await products.UpdateAsync(product, ct);
        return true;
    }
}
