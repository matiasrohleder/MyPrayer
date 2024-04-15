using BusinessLayer.JobScheduler.Jobs;
using Quartz;

namespace WebApp.JobScheduler
{
    public class WebAppStartupJobsTrigger
    {
        private readonly IScheduler scheduler;

        public WebAppStartupJobsTrigger(IScheduler scheduler)
        {
            this.scheduler = scheduler;
        }
        public async Task StartAsync()
        {
            // Start the scheduler
            await scheduler.Start();

            // Define job and trigger
            IJobDetail jobDetail = JobBuilder.Create<ReadingsJob>()
                .WithIdentity("readingsJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("readingsTrigger", "group1")
                .WithCronSchedule("0 0 1 * * ?") // Everyday at 1 am
                .Build();

            // Schedule the job with the trigger
            await scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}