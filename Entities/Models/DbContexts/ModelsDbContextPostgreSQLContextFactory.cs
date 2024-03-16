using DataLayer.Configuration;
using Entities.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Entities.Models.DbContexts;

public class ModelsDbContextPostgreSQLContextFactory : IDesignTimeDbContextFactory<ModelsDbContextPostgreSQL>
{
    public ModelsDbContextPostgreSQL CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = DbContextFactoryHelper.GetConfiguration();

        DbContextOptionsBuilder<ModelsDbContextPostgreSQL> optionsBuilder = new();

        DatabaseConfiguration dbConfig = new DatabaseConfiguration().Bind(configuration);

        optionsBuilder.UseNpgsql(dbConfig.ModelConnection);

        return new ModelsDbContextPostgreSQL(optionsBuilder.Options);
    }
}