using BusinessLayer.BusinessLogic;
using BusinessLayer.Interfaces;
using BusinessLayer.JobScheduler.Jobs;
using DataLayer;
using Tools.Helpers.Configuration;
using Tools.Helpers.Email;
using Tools.Interfaces.Configuration;
using Tools.Interfaces.Email;

namespace WebApp;

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
        AddJobs();
        AddEmailService();
        AddAuthentication();

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
        Services.AddSingleton<IConfiguration>(x => new MyPrayerConfiguration(x.GetRequiredService<IWebHostEnvironment>(), x.GetRequiredService<IServiceProvider>()));

        Services.AddScoped<IGeneralConfiguration, GeneralConfiguration>();
        Services.AddScoped<IRealmConfiguration, RealmConfiguration>();
    }

    private void AddJobs()
    {
        Services.AddTransient<ReadingsJob>();
    }

    private void AddEmailService()
    {
        Services.AddTransient<IEmailSender, EmailSender>();

        var smtpConfig = new SmtpConfiguration().Bind(Configuration);

        Services.AddFluentEmail(smtpConfig.Address)
            .AddRazorRenderer()
            .AddSmtpSender(() =>
            {
                var smtpConfig = new SmtpConfiguration().Bind(Configuration);
                return smtpConfig.GenerateSmtpClient();
            });
    }

    private void AddAuthentication()
    {
        Services.ConfigureApplicationCookie(config =>
        {
            config.LoginPath = "/Identity/Account/Login";
            config.AccessDeniedPath = "/Identity/Account/AccessDenied";
        });
    }
}