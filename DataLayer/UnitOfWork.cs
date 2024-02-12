using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

/// <summary>
/// Unit of Work class.
/// </summary>
public class UnitOfWork(DbContext modelDbContext) : IUnitOfWork, IDisposable
{
    #region Properties and constructor

    private readonly DbContext modelDbContext = modelDbContext;
    private bool disposed = false;
    #endregion

    #region Public methods
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
    {
        return new Repository<TEntity>(
            modelDbContext
        );
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
        {
            modelDbContext.Dispose();
        }

        disposed = true;
    }
    #endregion
}