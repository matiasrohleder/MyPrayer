using Quartz;
using Quartz.Simpl;
using Quartz.Spi;

namespace BusinessLayer.JobScheduler.JobConfiguration
{
    public class JobFactory : SimpleJobFactory
    {
        private readonly IServiceProvider provider;

        public JobFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public override IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                // this will inject dependencies that the job requires
                var service = provider.GetService(bundle.JobDetail.JobType);

                if (service == null)
                    throw new NotImplementedException($"An implementation of Job type {bundle.JobDetail.JobType} was not found. Make sure the class was registered on WebApp.ServiceInjection");

                return (IJob)service;
            }
            catch (NotImplementedException)
            {
                // Throw right away so the Devs know there was an issue with the Injection of the Job class.
                throw;
            }
            catch (Exception ex)
            {
                throw new SchedulerException($"Problem while instantiating job '{bundle.JobDetail.Key}' from the Aspnet Core IOC.", ex);
            }
        }
    }
}