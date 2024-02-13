namespace DataLayer.Interfaces;

/// <summary>
/// Generic service interface.
/// </summary>
public interface IService<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Asynchronously retrieves a single IEntity object from the Database by id.
    /// </summary>
    Task<TEntity?> GetAsync(Guid id);

    /// <summary>
    /// Returns an IQueryable of type TEntity representing all rows of an IEntity table.
    /// </summary>
    /// <param name="asNoTracking">If set to true, the entities returned by the query will not be tracked by the dbContext</param>
    IQueryable<TEntity> GetAll(bool asNoTracking = false);

    /// <summary>
    /// Asynchronously inserts a new IEntity object into the database.
    /// </summary>
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Asynchronously updates an existent IEntity object from the database.
    /// </summary>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Asynchronously deletes (logical) an existent IEntity object from the database by id.
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Asynchronously deletes (logical) an existent IEntity object from the database.
    /// </summary>
    Task DeleteAsync(TEntity entity);
}