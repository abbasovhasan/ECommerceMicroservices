using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public interface IRepository<T> where T : BaseEntity
{
    DbSet<T> Table { get; }
}
