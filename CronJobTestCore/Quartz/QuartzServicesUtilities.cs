using Quartz;
using System;
using System.Threading.Tasks;

public static class QuartzServicesUtilities
{
    public static void StartJobAsync<TJob>(IScheduler scheduler, TimeSpan runInterval)
        where TJob : IJob
    {
        var jobName = typeof(TJob).FullName;

        var job = JobBuilder.Create<TJob>()
            .WithIdentity(jobName)
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity(Guid.NewGuid().ToString())
            .StartNow()
            .WithSimpleSchedule(scheduleBuilder =>
                scheduleBuilder
                    .WithInterval(runInterval)
                    .RepeatForever())
            .Build();

        scheduler.ScheduleJob(job, trigger);
    }

    public static async Task StartJobAsync<TJob>(IScheduler scheduler, String cronExpression)
       where TJob : IJob
    {
        var jobName = typeof(TJob).FullName;

        var job = JobBuilder.Create<TJob>()
            .WithIdentity(jobName)
            .StoreDurably()
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity($"{jobName}.trigger")
            .WithCronSchedule(cronExpression)
            .StartNow()
            .ForJob(jobName)
            .Build();


        await scheduler.ScheduleJob(trigger);
    }

    public static void StartJobAsync<TJob>(IScheduler scheduler, ITrigger trigger)
       where TJob : IJob
    {
        var jobName = typeof(TJob).FullName;

        var job = JobBuilder.Create<TJob>()
            .WithIdentity(jobName)
            .Build();

        scheduler.ScheduleJob(job, trigger);
    }
}
