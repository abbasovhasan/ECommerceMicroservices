using Microsoft.EntityFrameworkCore;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
{
    private readonly OrderDbContext _context;

    public WriteRepository(OrderDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        Table.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        Table.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
