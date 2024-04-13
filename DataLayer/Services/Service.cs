using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Services;

public class Service<TEntity> : IService<TEntity> where TEntity : class, IEntity
{
    #region Properties and constructor
    protected readonly IRepository<TEntity> repository;
    protected readonly IUnitOfWork unitOfWork;

    public Service(
        IUnitOfWork unitOfWork
    )
    {
        repository = unitOfWork.GetRepository<TEntity>();
        this.unitOfWork = unitOfWork;
    }
    #endregion

    #region Public methods
    ///<inheritdoc/>
    public virtual async Task<TEntity?> GetAsync(Guid id)
    {
        TEntity? entity;
        try
        {
            entity = await repository.GetAsync(id);

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    ///<inheritdoc/>
    public virtual IQueryable<TEntity> GetAll(bool asNoTracking = false)
    {
        try
        {
            IQueryable<TEntity> entities = repository.GetAll();

            if (asNoTracking)
                entities = entities.AsNoTracking();

            return entities;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    ///<inheritdoc/>
    public virtual async Task AddAsync(TEntity entity)
    {
        try
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.LastEditedDate = entity.CreatedDate;
            // entity.CreatorId = operationContext.GetUserId(); TODO
            entity.CreatorId = Guid.NewGuid();
            entity.LastEditorId = entity.CreatorId;

            repository.Add(entity);

            await unitOfWork.SaveChangesAsync<TEntity>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    ///<inheritdoc/>
    public virtual async Task UpdateAsync(TEntity entity)
    {
        try
        {
            entity.LastEditedDate = DateTime.UtcNow;
            // entity.LastEditorId = this.operationContext.GetUserId(); TODO

            repository.Update(entity);

            await unitOfWork.SaveChangesAsync<TEntity>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    ///<inheritdoc/>
    public virtual async Task DeleteAsync(Guid id)
    {
        try
        {
            TEntity entity = await GetAsync(id) ?? throw new Exception("Entity not found");

            await DeleteAsync(entity);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(TEntity entity)
    {
        try
        {
            entity.LastEditedDate = DateTime.Now;
            entity.Deleted = true;
            // entity.LastEditorId = this.operationContext.GetUserId();TODO

            repository.Update(entity);

            await unitOfWork.SaveChangesAsync<TEntity>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion
}