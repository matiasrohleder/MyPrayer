using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using DataLayer.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace DataLayer;

public class MyPrayerConfiguration : IConfiguration
{
    private readonly IConfigurationRoot configurationRoot;
    private readonly IServiceProvider serviceProvider;

    public MyPrayerConfiguration(IHostEnvironment env, IServiceProvider serviceProvider)
    {
        configurationRoot = new ConfigurationBuilder()
                .AddMyPrayerConfiguration(env)
                .AddEnvironmentVariables()
                .Build();

        this.serviceProvider = serviceProvider;
    }

    public MyPrayerConfiguration(IWebHostEnvironment env, IServiceProvider serviceProvider)
    {
        configurationRoot = new ConfigurationBuilder()
                .AddMyPrayerConfiguration(env)
                .AddEnvironmentVariables()
                .Build();

        this.serviceProvider = serviceProvider;
    }

    public string? this[string key]
    {
        get
        {

            return configurationRoot[key];
        }

        set
        {
            configurationRoot[key] = value;
            if (value == null)
                configurationRoot.Reload();
        }
    }

    public IEnumerable<IConfigurationSection> GetChildren()
    {
        return configurationRoot.GetChildren();
    }

    public IChangeToken GetReloadToken()
    {
        return configurationRoot.GetReloadToken();
    }

    public IConfigurationSection GetSection(string key)
    {
        return new MyPrayerConfigurationSection(configurationRoot as ConfigurationRoot, key);
    }
}