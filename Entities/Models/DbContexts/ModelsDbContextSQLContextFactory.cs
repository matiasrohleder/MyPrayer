using DataLayer.Configuration;
using Entities.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Entities.Models.DbContexts;

public class ModelsDbContextSQLContextFactory : IDesignTimeDbContextFactory<ModelsDbContextSQL>
{
    public ModelsDbContextSQL CreateDbContext(string[] args)
    {
        var configuration = DbContextFactoryHelper.GetConfiguration();

        var optionsBuilder = new DbContextOptionsBuilder<ModelsDbContextSQL>();

        var dbConfig = new DatabaseConfiguration().Bind(configuration);

        optionsBuilder.UseSqlServer(dbConfig.ModelConnection);

        return new ModelsDbContextSQL(optionsBuilder.Options);
    }
}