using Microsoft.EntityFrameworkCore;
using Product.Application.Common;
using ProductEntity = Product.Domain.Entities.Product;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<ProductEntity> Products => Set<ProductEntity>();
    public DbSet<Customer> Customers => Set<Customer>();  
    public DbSet<OrderEntity> Orders => Set<OrderEntity>(); 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductEntity>(b =>
        {
            b.HasKey(p => p.Id);
            b.Property(p => p.Name).IsRequired().HasMaxLength(200);
            b.Property(p => p.Price).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Customer>(b =>
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.Name).IsRequired().HasMaxLength(200);
            b.Property(c => c.Email).IsRequired().HasMaxLength(250);
            b.Property(c => c.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<OrderEntity>(b =>
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.OrderRef).IsRequired().HasMaxLength(200);
        });
    }
}
