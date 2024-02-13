using DataLayer.Constants;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Configuration;

internal class MyPrayerConfigurationContext(string connectionString, string modelProvider) : DbContext
{
    private readonly string _connectionString = connectionString;
    private readonly string _modelProvider = modelProvider;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        switch (_modelProvider)
        {
            case DatabaseProviders.SqlServer:
                optionsBuilder.UseSqlServer(_connectionString);
                break;
            default:
                throw new NotImplementedException($"The database provider '{_modelProvider}' specified for the Model is not supported. ");
        }
    }
}