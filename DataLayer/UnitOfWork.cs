using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

/// <summary>
/// Unit of Work class.
/// </summary>
public class UnitOfWork(DbContext modelsDbContext, IRepositoryFactory repositoryFactory) : IUnitOfWork, IDisposable
{
    #region Properties and constructor
    private readonly DbContext modelsDbContext = modelsDbContext;
    private readonly IRepositoryFactory repositoryFactory = repositoryFactory;
    private bool disposed = false;
    #endregion

    #region Public methods
    ///<inheritdoc/>
    public DbContext GetDbContext<TEntity>() where TEntity : class, IEntity => GetModelsDbContext();

    ///<inheritdoc/>
    public DbContext GetModelsDbContext() => modelsDbContext;

    ///<inheritdoc/>
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity => repositoryFactory.CreateRepository<TEntity>(GetDbContext<TEntity>());

    ///<inheritdoc/>
    public async Task<int> SaveChangesAsync<TEntity>() where TEntity : class, IEntity
    {
        return await modelsDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Disposes the UnitOfWork checking if it's already disposed first.
    /// </summary>
    public void Dispose()
    {
        DisposeDbContexts();
        GC.SuppressFinalize(this);
    }
    #endregion

    #region Private methods
    private void DisposeDbContexts()
    {
        if (!disposed)
            modelsDbContext.Dispose();

        disposed = true;
    }
    #endregion
}