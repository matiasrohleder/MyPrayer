using BusinessLayer.Interfaces;
using BusinessLayer.JobScheduler.JobConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace BusinessLayer.JobScheduler.Jobs
{
    public class ReadingsJob : JobBase
    {
        public ReadingsJob(IQuartzJobServiceInjection serviceInjection) : base(serviceInjection)
        {
        }

        protected override async Task ExecuteAsync(IServiceScope scope, IJobExecutionContext context)
        {
            // Inject services from scope.
            var readingBusinessLogic = scope.ServiceProvider.GetRequiredService<IReadingBusinessLogic>();

            await readingBusinessLogic.GetReadings();

        }
    }
}