using Microsoft.EntityFrameworkCore;

namespace DataLayer.Interfaces;

/// <summary>
/// Unit of Work interface.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Exposes the DbContext correspondent to a given TEntity as readonly.
    /// </summary>
    DbContext GetDbContext<TEntity>() where TEntity : class, IEntity;

    /// <summary>
    /// Exposes the ModelsDbContext
    /// </summary>
    DbContext GetModelsDbContext();


    /// <summary>
    /// Instantiates a new Repository for a given IEntity type.
    /// </summary>
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;

    /// <summary>
    /// Asynchronously saves all the changes registered to the DbContext
    /// </summary>
    Task<int> SaveChangesAsync<TEntity>() where TEntity : class, IEntity;
}