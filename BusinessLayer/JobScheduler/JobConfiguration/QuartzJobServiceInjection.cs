using BusinessLayer.BusinessLogic;
using BusinessLayer.Configurations;
using BusinessLayer.Interfaces;
using DataLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tools.Helpers.Configuration;
using Tools.Interfaces.Configuration;

namespace BusinessLayer.JobScheduler.JobConfiguration;

/// <summary>
/// Quartz Job's service injection.
/// </summary>
internal class QuartzJobServiceInjection : ServiceInjection, IQuartzJobServiceInjection
{
    public QuartzJobServiceInjection(IConfiguration configuration) : base(new ServiceCollection(), configuration)
    {

    }

    public override IServiceCollection Initialize()
    {
        Services.AddBusinessLayer(Configuration);
        Services.AddHttpClient();

        AddServices();
        AddBusinessLogics();
        AddConfigurations();

        return Services;
    }
    private void AddServices()
    {
    }

    private void AddBusinessLogics()
    {
        Services.AddScoped<IReadingBusinessLogic, ReadingBusinessLogic>();

    }

    private void AddConfigurations()
    {
        Services.AddScoped(x => Configuration);

        Services.AddScoped<IBibleConfiguration, BibleConfiguration>();
        Services.AddScoped<IGeneralConfiguration, GeneralConfiguration>();
        Services.AddScoped<IRealmConfiguration, RealmConfiguration>();
    }
}