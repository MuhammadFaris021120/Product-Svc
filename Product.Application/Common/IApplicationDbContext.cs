using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using ProductEntity = global::Product.Domain.Entities.Product;

namespace Product.Application.Common;

public interface IApplicationDbContext
{
    DbSet<ProductEntity> Products { get; }
    DbSet<Customer> Customers { get; }
    DbSet<OrderEntity> Orders { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}