using DataLayer.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace DataLayer.Extensions;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddMyPrayerConfiguration(this IConfigurationBuilder builder, IHostEnvironment env)
    {
        IConfigurationRoot configuration = builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .Build();

        DatabaseConfiguration db = new DatabaseConfiguration().Bind(configuration);

        return builder.Add(new MyPrayerConfigurationSource(db.ModelConnection ?? "defaultConnection", db.ModelProvider ?? "defaultProvider"));
    }

    public static IConfigurationBuilder AddMyPrayerConfiguration(this IConfigurationBuilder builder, IWebHostEnvironment env)
    {
        IConfigurationRoot configuration = builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .Build();

        DatabaseConfiguration db = new DatabaseConfiguration().Bind(configuration);

        return builder.Add(new MyPrayerConfigurationSource(db.ModelConnection ?? "defaultConnection", db.ModelProvider ?? "defaultProvider"));
    }
}