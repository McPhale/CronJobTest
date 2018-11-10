using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace CronJobTest
{
    public class JobScheduler
    {
        public static void  Start()
        {

            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler =  factory.GetScheduler();
             scheduler.Start();

            IJobDetail HelloJob = JobBuilder.Create<HelloJob>().Build();
            IJobDetail GoodbyeJob = JobBuilder.Create<GoodByeJob>().StoreDurably().Build();
             scheduler.AddJob(GoodbyeJob, false);

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


             scheduler.ScheduleJob(HelloJob, fiveMinuteTrigger);   
             scheduler.ScheduleJob(tenMinuteTrigger);                              
             scheduler.ScheduleJob(fifteenMinuteTrigger);

        }  
    }
}
