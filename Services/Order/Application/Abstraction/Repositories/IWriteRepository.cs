public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
