using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NLog;
using Quartz;
using Quartz.Impl;

namespace CronJobTest
{
    public class JobScheduler 
    {
        private readonly IScheduler scheduler;
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _AppSettings;

        


        public JobScheduler(IConfiguration AppSettings)
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            scheduler = factory.GetScheduler().ConfigureAwait(true).GetAwaiter().GetResult();
            _AppSettings = AppSettings;
        }

        public async void Start()
        {

            logger.Info("Starting scheduler." + _AppSettings.GetValue<string>("TestString"));
            await scheduler.Start();

            IJobDetail HelloJob = JobBuilder.Create<HelloJob>().Build();
            IJobDetail GoodbyeJob = JobBuilder.Create<GoodByeJob>().StoreDurably().Build();
            await scheduler.AddJob(GoodbyeJob, false);

            ITrigger fiveMinuteTrigger = TriggerBuilder.Create()
            .WithIdentity("fiveMinuteTrigger")
            .StartNow()
            .WithCronSchedule("0 0/5 * 1/1 * ? *")
            .Build();


            ITrigger tenMinuteTrigger = TriggerBuilder.Create()
            .WithIdentity("tenMinuteTrigger")
            .WithCronSchedule("0 0/10 * 1/1 * ? *")
            .StartNow()
            .ForJob(GoodbyeJob)
            .Build();

            ITrigger fifteenMinuteTrigger = TriggerBuilder.Create()
            .WithIdentity("fifteenMinuteTrigger")
            .WithCronSchedule("0 0/15 * 1/1 * ? *")
            .StartNow()
            .ForJob(GoodbyeJob)
            .Build();


            await scheduler.ScheduleJob(HelloJob, fiveMinuteTrigger);
            await scheduler.ScheduleJob(tenMinuteTrigger);
            await scheduler.ScheduleJob(fifteenMinuteTrigger);

        }

        public async void Stop()
        {
            logger.Info("Shutting down scheduler.");
            await scheduler.Shutdown();
        }
    }
}
