using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace BusinessLayer.JobScheduler.JobConfiguration
{
    public abstract class JobBase : IJob
    {
        private readonly IServiceProvider _serviceProvider;

        public JobBase(IQuartzJobServiceInjection serviceInjection)
        {
            _serviceProvider = serviceInjection.Initialize().BuildServiceProvider();
        }

        /// <summary>
        /// This method is called when a job is triggered. Generates an isolated scope then executes job logic and finally disposes the scope. 
        /// </summary>
        /// <param name="context">The job context</param>
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                await ExecuteAsync(scope, context);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Executes the job logic.
        /// </summary>
        /// <param name="scope">Used to inject services</param>
        /// <param name="context">The job context</param>
        protected abstract Task ExecuteAsync(IServiceScope scope, IJobExecutionContext context);
    }
}
