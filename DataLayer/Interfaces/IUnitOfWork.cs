namespace DataLayer.Interfaces;

/// <summary>
/// Unit of Work interface.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Instantiates a new Repository for a given IEntity type.
    /// </summary>
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
}