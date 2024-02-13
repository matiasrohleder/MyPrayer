using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer;

public static class DataLayerExtension
{
    /// <summary>
    /// Adds DAL's capabilities.
    /// </summary>
    public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return new ServiceInjection(services, configuration).Initialize();
    }
}