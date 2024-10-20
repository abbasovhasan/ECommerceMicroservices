using Microsoft.EntityFrameworkCore;
using System;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
{
    private readonly CatalogDbContext _context;
    public DbSet<T> Table => _context.Set<T>();

    public WriteRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        Table.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        Table.Remove(entity);
        _context.SaveChanges();
    }
}
