using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer;

public interface IServiceInjection
{
    /// <summary>
    /// Use the configuration property to access values from the current project's config file.
    /// </summary>
    IConfiguration Configuration { get; set; }

    /// <summary>
    /// Use the services property to register new services (E.g: using the AddScopped method).
    /// </summary>
    IServiceCollection Services { get; set; }

    /// <summary>
    /// Registers all required services.
    /// </summary>
    IServiceCollection Initialize();
}