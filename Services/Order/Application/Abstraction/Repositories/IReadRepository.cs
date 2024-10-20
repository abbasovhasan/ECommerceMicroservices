using System.Linq.Expressions;

public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate);
}
