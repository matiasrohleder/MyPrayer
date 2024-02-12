using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Models;

public class Repository<T>(DbContext dbContext) : IRepository<T> where T : class, IEntity
{
    private readonly DbContext dbContext = dbContext;

    ///<inheritdoc/>
    public async Task<T?> GetAsync(Guid id) => await dbContext.Set<T>().FindAsync(id);

    ///<inheritdoc/>
    public async Task<IEnumerable<T>> GetAllAsync() => await dbContext.Set<T>().ToListAsync();

    ///<inheritdoc/>
    public void Add(T entity)
    {
        dbContext.Set<T>().Add(entity);
        dbContext.SaveChanges();
    }

    ///<inheritdoc/>
    public void Update(T entity)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        dbContext.SaveChanges();
    }

    ///<inheritdoc/>
    public void Delete(T entity)
    {
        dbContext.Set<T>().Remove(entity);

        dbContext.Entry(entity).State = EntityState.Deleted;
        dbContext.SaveChanges();
    }
}
