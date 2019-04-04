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
        private readonly IScheduler _scheduler;
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _AppSettings;

        


        public JobScheduler(IConfiguration AppSettings, IScheduler scheduler)
        {

            _AppSettings = AppSettings;
            _scheduler = scheduler;
        }

        public async void Start()
        {
            logger.Info("Starting scheduler." + _AppSettings.GetValue<string>("TestString"));
            await _scheduler.Start();
            await QuartzServicesUtilities.StartJobAsync<HelloJob>(_scheduler, "0 0/5 * 1/1 * ? *");
            await QuartzServicesUtilities.StartJobAsync<GoodByeJob>(_scheduler, "0 0/10 * 1/1 * ? *");
            await QuartzServicesUtilities.StartJobAsync<GoodByeJob>(_scheduler, "0 0/15 * 1/1 * ? *");

            ITrigger runOnceAtStartupTrigger = TriggerBuilder.Create()
           .WithIdentity("runOnceAtStartupTrigger")
           .StartNow()
           .WithSimpleSchedule()
           .Build();
            await QuartzServicesUtilities.StartJobAsync<WeatherJob>(_scheduler, runOnceAtStartupTrigger);
        }

        public async void Stop()
        {
            logger.Info("Shutting down scheduler.");
            await _scheduler.Shutdown();
        }
    }
}
