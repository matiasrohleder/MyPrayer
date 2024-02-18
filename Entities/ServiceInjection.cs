using DataLayer;
using DataLayer.Interfaces;
using Entities.Helpers;
using Entities.Models.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Entities;

internal class ServiceInjection(IServiceCollection services, IConfiguration configuration) : AbstractServiceInjection(services, configuration)
{
    public override IServiceCollection Initialize()
    {
        RegisterUnitOfWork();
        RegisterDatabaseServices();

        return Services;
    }

    #region Private methods
    /// <summary>
    /// We'll need to Inject UnitOfWork dependencies explicitly
    /// because we need to store entities and logs in different Databases
    /// meaning that'll need different DbContext implementations.
    /// </summary>
    private void RegisterUnitOfWork()
    {
        Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
        Services.AddScoped<IUnitOfWork>((provider) =>
        {
            ModelsDbContext modelDbContext = provider.GetRequiredService<ModelsDbContext>();
            IRepositoryFactory repositoryFactory = provider.GetRequiredService<IRepositoryFactory>();
            return new UnitOfWork(modelDbContext, repositoryFactory);
        });
    }

    /// <summary>
    /// Registers the Model and Log dbContexts and sets them up to use the provider specified in appsettings.
    /// </summary>
    private void RegisterDatabaseServices()
    {
        DbContextFactoryHelper.RegisterModelsDbContext(Services);
    }
    #endregion
}