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

        return new ServiceInjection(services, configuration).Initialize();
    }
}