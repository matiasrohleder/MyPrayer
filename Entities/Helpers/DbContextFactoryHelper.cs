using DataLayer.Configuration;
using DataLayer.Constants;
using Entities.Models.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Entities.Helpers;

public static class DbContextFactoryHelper
{
    public static IConfigurationRoot GetConfiguration()
    {
        string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        return new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environment}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();
    }

    public static void RegisterModelsDbContext(IServiceCollection services)
    {
        var configuration = GetConfiguration();
        var dbConfig = new DatabaseConfiguration().Bind(configuration);

        switch (dbConfig.ModelProvider)
        {
            case DatabaseProviders.SqlServer:
                RegisterDataBaseContext<ModelsDbContext, ModelsDbContextSQL>(services, options => options.UseSqlServer(dbConfig.ModelConnection));
                break;
            case DatabaseProviders.PostgreSQL:
                RegisterDataBaseContext<ModelsDbContext, ModelsDbContextPostgreSQL>(services, options => options.UseNpgsql(dbConfig.ModelConnection));
                break;
            default:
                throw new NotImplementedException($"The database provider '{dbConfig.ModelProvider}' specified for the Model is not supported. ");
        }
    }

    /// <summary>
    /// Sets the TDbContext to use Database provider
    /// </summary>
    private static void RegisterDataBaseContext<TBaseDbContext, TDbContext>(IServiceCollection services, Action<DbContextOptionsBuilder> options)
        where TDbContext : TBaseDbContext
        where TBaseDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(options, ServiceLifetime.Scoped);
        services.AddScoped<TBaseDbContext, TDbContext>();
    }
}