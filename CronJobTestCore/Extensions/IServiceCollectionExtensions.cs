using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CronJobTestCore.Extensions
{
    static class IServiceCollectionExtensions
    {
        public static void UseQuartz(this IServiceCollection services, params Type[] jobs)
        {
            services.AddSingleton<IJobFactory, QuartzJonFactory>();
            services.Add(jobs.Select(jobType => new ServiceDescriptor(jobType, jobType, ServiceLifetime.Singleton)));

            services.AddSingleton(async provider =>
            {
                var schedulerFactory = new StdSchedulerFactory();
                var scheduler = await schedulerFactory.GetScheduler();
                scheduler.JobFactory = provider.GetService<IJobFactory>();
                await scheduler.Start();
                return scheduler;
            });
        }
    }
}
