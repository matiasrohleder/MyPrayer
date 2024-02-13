using DataLayer.Interfaces;
using DataLayer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer;

/// <summary>
/// DataLayer's service injection.
/// </summary>
internal class ServiceInjection(IServiceCollection services, IConfiguration configuration) : AbstractServiceInjection(services, configuration)
{
    public override IServiceCollection Initialize()
    {
        Services.AddScoped(typeof(IService<>), typeof(Service<>));

        return Services;
    }
}