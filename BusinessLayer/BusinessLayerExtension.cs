using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using BusinessLayer.Services.FileService;

namespace BusinessLayer;

public static class BusinessLayerExtension
{
    /// <summary>
    /// Adds Business Layer's capabilities.
    /// </summary>
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEntities(configuration);

        // Load business-specific configuration
        var businessSettingsBuilder = new ConfigurationBuilder()
            .AddJsonFile("businesssettings.json", optional: false, reloadOnChange: true);

        using (var serviceProvider = services.BuildServiceProvider())
        {
            var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            if (env.IsDevelopment())
            {
                businessSettingsBuilder.AddJsonFile("businesssettings.Development.json", optional: true, reloadOnChange: true);
            }
        }

        var businessSettings = businessSettingsBuilder.Build();

        services.AddSingleton<IFileServiceConfiguration, FileServiceConfiguration>(provider => new FileServiceConfiguration(businessSettings));
        services.AddSingleton<IFileServiceHelper, FileServiceHelper>();

        return new ServiceInjection(services, configuration).Initialize();
    }
}