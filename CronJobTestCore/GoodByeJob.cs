using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Quartz;

namespace CronJobTest
{
    //[DisallowConcurrentExecution]
    class GoodByeJob : IJob
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        public Task Execute(IJobExecutionContext context)
        {
            //Console.Out.WriteLine($"{DateTime.Now} Goodbye!");
            logger.Info("Goodbye!");
            return Task.CompletedTask;
        }
    }
}
