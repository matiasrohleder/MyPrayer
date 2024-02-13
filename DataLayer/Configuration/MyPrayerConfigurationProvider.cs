using Microsoft.Extensions.Configuration;

namespace DataLayer.Configuration;

public class MyPrayerConfigurationProvider : ConfigurationProvider
{
    private readonly string _connectionString;
    private readonly string _modelProvider;

    public MyPrayerConfigurationProvider(string connectionString, string modelProvider)
    {
        _connectionString = connectionString;
        _modelProvider = modelProvider;
    }

    public override void Load()
    {
        using MyPrayerConfigurationContext dbContext = new(_connectionString, _modelProvider);

        base.Load();
    }
}