using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using NLog;
using Quartz;
using Quartz.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace CronJobTestCore
{
    public class JobScheduler 
    {
        private IScheduler scheduler;
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _AppSettings;

        


        public JobScheduler(IConfiguration AppSettings)
        {
            _AppSettings = AppSettings;
        }

        public async void Start()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            scheduler = await factory.GetScheduler();

            logger.Info("Starting scheduler." + _AppSettings.GetValue<string>("TestString"));
            await scheduler.Start();
            await QuartzServicesUtilities.StartJobAsync<HelloJob>(scheduler, "0 0/5 * 1/1 * ? *");
            await QuartzServicesUtilities.StartJobAsync<GoodByeJob>(scheduler, "0 0/10 * 1/1 * ? *");
            await QuartzServicesUtilities.StartJobAsync<GoodByeJob>(scheduler, "0 0/15 * 1/1 * ? *");
        }

        public async void Stop()
        {
            logger.Info("Shutting down scheduler.");
            await scheduler.Shutdown();
        }
    }
}
