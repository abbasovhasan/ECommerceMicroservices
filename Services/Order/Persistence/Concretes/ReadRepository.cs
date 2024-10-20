using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly OrderDbContext _context;

    public ReadRepository(OrderDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Table.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await Table.FirstOrDefaultAsync(e => e.Id == id);
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
    {
        return Table.Where(predicate);
    }
}
