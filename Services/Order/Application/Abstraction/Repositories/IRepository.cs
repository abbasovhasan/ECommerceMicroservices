using Microsoft.EntityFrameworkCore;

public interface IRepository<T> where T : BaseEntity
{
    DbSet<T> Table { get; } // Entity'nin DbSet'i
}
