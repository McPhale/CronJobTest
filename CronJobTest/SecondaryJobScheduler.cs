using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace CronJobTest
{
    public class SecondaryJobScheduler
    {
        public static async Task StartAsync()
        {

            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            IJobDetail SecondaryJob = JobBuilder.Create<SecondaryJob>().Build();

            ITrigger SecondaryJobTrigger = TriggerBuilder.Create()
            .WithIdentity("SecondaryJobTrigger")
            .StartNow()
            //.WithCronSchedule("0 0/5 * 1/1 * ? *")
            .WithCronSchedule("0 0 0 1 1 ? 2099")
            .Build();

            await scheduler.ScheduleJob(SecondaryJob, SecondaryJobTrigger);   

        }  
    }
}
