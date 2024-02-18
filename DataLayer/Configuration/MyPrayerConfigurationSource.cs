using Microsoft.Extensions.Configuration;

namespace DataLayer.Configuration;

public class MyPrayerConfigurationSource(string connectionString, string modelProvider) : IConfigurationSource
{
    private readonly string _connectionString = connectionString;
    private readonly string _modelProvider = modelProvider;

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new MyPrayerConfigurationProvider(_connectionString, _modelProvider);
    }
}