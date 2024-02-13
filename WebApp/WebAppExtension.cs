using BusinessLayer;

namespace WebApp;

internal static class WebAppExtension
{
    /// <summary>
    /// Configure WebApp by adding base layers and injecting WebApp's specific services.
    /// </summary>
    public static IServiceCollection ConfigureWebApp(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBusinessLayer(configuration);

        return new ServiceInjection(services, configuration).Initialize();
    }
}