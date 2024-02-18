using DataLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer;

/// <summary>
/// Business Layer's service injection.
/// </summary>
internal class ServiceInjection(IServiceCollection services, IConfiguration configuration) : AbstractServiceInjection(services, configuration)
{
    public override IServiceCollection Initialize()
    {
        return Services;
    }
}