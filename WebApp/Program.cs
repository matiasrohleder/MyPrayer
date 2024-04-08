using BusinessLayer.JobScheduler.JobConfiguration;
using Entities.Models;
using Entities.Models.DbContexts;
using Microsoft.AspNetCore.Identity;
using Quartz;
using Quartz.Impl;
using WebApp;
using WebApp.JobScheduler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.ConfigureWebApp(builder.Configuration);

#region Identity configuration
builder.Services.AddRazorPages();

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(config =>
    {
        config.User.RequireUniqueEmail = true;
        config.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<ModelsDbContext>()
    .AddDefaultTokenProviders();
#endregion

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(int.Parse(port));
});

// Add Quartz scheduler
builder.Services.AddSingleton<IScheduler>(provider =>
{
    var schedulerFactory = new StdSchedulerFactory();
    return schedulerFactory.GetScheduler().Result;
});

builder.Services.AddHostedService<QuartzHostedService>();
builder.Services.AddSingleton<WebAppStartupJobsTrigger>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

#region Endpoint routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
#endregion

app.Run();

public class QuartzHostedService : IHostedService
{
    private readonly IScheduler _scheduler;
    private readonly IServiceProvider _serviceProvider;

    public QuartzHostedService(IScheduler scheduler, IServiceProvider serviceProvider)
    {
        _scheduler = scheduler;
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Start the scheduler
        await _scheduler.Start(cancellationToken);

        _scheduler.JobFactory = new JobFactory(_serviceProvider);

        // Retrieve the WebAppStartupJobsTrigger from the service provider
        using (var scope = _serviceProvider.CreateScope())
        {
            var jobTrigger = scope.ServiceProvider.GetRequiredService<WebAppStartupJobsTrigger>();
            await jobTrigger.StartAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Shutdown the scheduler
        return _scheduler.Shutdown(cancellationToken);
    }
}