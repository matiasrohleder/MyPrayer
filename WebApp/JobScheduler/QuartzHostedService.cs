using BusinessLayer.JobScheduler.JobConfiguration;
using Quartz;
using WebApp.JobScheduler;

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