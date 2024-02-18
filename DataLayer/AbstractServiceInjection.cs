using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer;

/// <summary>
/// Service injection abstract class.
/// Defines the required methods and properties to register services.
/// </summary>
public abstract class AbstractServiceInjection(IServiceCollection services, IConfiguration configuration) : IServiceInjection
{
    /// <inheritdoc/>
    public IConfiguration Configuration { get; set; } = configuration;

    /// <inheritdoc/>
    public IServiceCollection Services { get; set; } = services;

    /// <inheritdoc/>
    public abstract IServiceCollection Initialize();
}