using DataLayer.Interfaces;

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
        this.unitOfWork = unitOfWork;
        repository = unitOfWork.GetRepository<TEntity>();
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

            CanAccess(entity, DALOperations.Get);

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion
}