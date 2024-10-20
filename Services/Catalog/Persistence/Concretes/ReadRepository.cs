using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly CatalogDbContext _context;
    public DbSet<T> Table => _context.Set<T>();

    public ReadRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await Table.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Table.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await Table.Where(predicate).ToListAsync();
    }
}
