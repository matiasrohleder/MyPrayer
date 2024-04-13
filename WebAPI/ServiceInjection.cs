using BusinessLayer.Configurations;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataLayer;

namespace WebAPI;

/// <summary>
/// WebApp's service injection.
/// </summary>
internal class ServiceInjection : AbstractServiceInjection
{

    public ServiceInjection(IServiceCollection services, IConfiguration configuration)
        : base(services, configuration)
    {
    }

    public override IServiceCollection Initialize()
    {
        Services.AddHttpClient();

        AddServices();
        AddBusinessLogics();
        AddConfigurations();

        return Services;
    }
    private void AddServices()
    {
        Services.AddScoped<IFileService, FileService>();
    }

    private void AddBusinessLogics()
    {
    }

    private void AddConfigurations()
    {
        Services.AddSingleton<IConfiguration>(x => new MyPrayerConfiguration(x.GetRequiredService<IWebHostEnvironment>(), x.GetRequiredService<IServiceProvider>()));

        Services.AddScoped<IBibleConfiguration, BibleConfiguration>();
    }
}