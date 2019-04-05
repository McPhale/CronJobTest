using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace CronJobTestCore
{
    public class JobScheduler 
    {
        private readonly IScheduler _scheduler;
        private readonly ILogger<JobScheduler> _logger;
        private readonly IConfiguration _AppSettings;

        public JobScheduler(IConfiguration AppSettings, IScheduler scheduler, ILogger<JobScheduler> logger)
        {

            _AppSettings = AppSettings;
            _scheduler = scheduler;
            _logger = logger;
        }

        public async void Start()
        {
            //_logger.Info("Starting scheduler." + _AppSettings.GetValue<string>("TestString"));
            _logger.LogInformation("Starting scheduler." + _AppSettings.GetValue<string>("TestString"));
            await _scheduler.Start();
            await QuartzServicesUtilities.StartJobAsync<HelloJob>(_scheduler, "0 0/5 * 1/1 * ? *");
            await QuartzServicesUtilities.StartJobAsync<GoodByeJob>(_scheduler, "0 0/10 * 1/1 * ? *");
            await QuartzServicesUtilities.StartJobAsync<GoodByeJob>(_scheduler, "0 0/15 * 1/1 * ? *");

            ITrigger runOnceAtStartupTrigger = TriggerBuilder.Create()
           .WithIdentity("runOnceAtStartupTrigger")
           .StartNow()
           .WithSimpleSchedule()
           .Build();
            await QuartzServicesUtilities.StartJobAsync<JsonTodoJob>(_scheduler, new TimeSpan(0,0,15));
        }

        public async void Stop()
        {
            //_logger.Info("Shutting down scheduler.");
            _logger.LogInformation("Shutting down scheduler.");
            await _scheduler.Shutdown();
        }
    }
}
