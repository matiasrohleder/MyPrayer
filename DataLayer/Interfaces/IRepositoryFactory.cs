using Microsoft.EntityFrameworkCore;

namespace DataLayer.Interfaces;

/// <summary>
/// Factory to create repositories.
/// </summary>
public interface IRepositoryFactory
{
    /// <summary>
    /// Creates a repository for a given Entity Type and DbContext.
    /// </summary>
    IRepository<TEntity> CreateRepository<TEntity>(DbContext dbContext) where TEntity : class, IEntity;
}