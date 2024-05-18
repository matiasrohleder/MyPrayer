using BusinessLayer.Interfaces;
using BusinessLayer.JobScheduler.JobConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace BusinessLayer.JobScheduler.Jobs
{
    public class ReadingsJob(IQuartzJobServiceInjection serviceInjection) : JobBase(serviceInjection)
    {
        protected override async Task ExecuteAsync(IServiceScope scope, IJobExecutionContext context)
        {
            // Inject services from scope.
            IReadingBusinessLogic readingBusinessLogic = scope.ServiceProvider.GetRequiredService<IReadingBusinessLogic>();

            await readingBusinessLogic.GetReadings();
        }
    }
}