﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Quartz;

namespace CronJobTestCore
{
    [DisallowConcurrentExecution]
    class HelloJob : IJob
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        public Task Execute(IJobExecutionContext context)
        {
            logger.Info("Hello!");
            return Task.CompletedTask;
        }
    }
}