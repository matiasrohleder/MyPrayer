using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

/// <summary>
/// Factory to create repositories.
/// </summary>
public class RepositoryFactory : IRepositoryFactory
{
    public RepositoryFactory()
    {
    }

    /// <inheritdoc />
    public IRepository<TEntity> CreateRepository<TEntity>(DbContext dbContext) where TEntity : class, IEntity
    {
        return new Repository<TEntity>(
            dbContext
        );
    }
}