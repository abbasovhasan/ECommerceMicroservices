public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
{
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
