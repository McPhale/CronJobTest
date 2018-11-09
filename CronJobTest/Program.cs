using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace CronJobTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();
            var helloJob = JobBuilder
                .Create<HelloJob>()
                .Build();
            var trigger = TriggerBuilder
                .Create()
                .WithSimpleSchedule(s => s.WithIntervalInSeconds(1).RepeatForever())
                .Build();
            await scheduler.ScheduleJob(helloJob, trigger);
            await Console.Out.WriteLineAsync("Press enter to exit.");
            await scheduler.Start();
            await Console.In.ReadLineAsync();
        }
    }
}
