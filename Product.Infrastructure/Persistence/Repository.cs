using Microsoft.EntityFrameworkCore;
using Product.Application.Common.Interfaces;

namespace Product.Infrastructure.Persistence;

public class Repository<T>(AppDbContext context) : IRepository<T> where T : class
{
    protected readonly DbSet<T> Set = context.Set<T>();

    public async Task<T?> GetByIdAsync(int id, CancellationToken ct)
        => await Set.FindAsync([id], ct);

    public async Task<List<T>> GetAllAsync(CancellationToken ct)
        => await Set.ToListAsync(ct);

    public async Task AddAsync(T entity, CancellationToken ct)
    {
        Set.Add(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(T entity, CancellationToken ct)
    {
        Set.Update(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(T entity, CancellationToken ct)
    {
        Set.Remove(entity);
        await context.SaveChangesAsync(ct);
    }
}
