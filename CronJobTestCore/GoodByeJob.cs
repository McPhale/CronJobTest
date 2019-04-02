using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Quartz;

namespace CronJobTestCore
{
    //[DisallowConcurrentExecution]
    class GoodByeJob : IJob
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync($"{DateTime.Now} Goodbye!");
            logger.Info("Goodbye!");
            //return Task.CompletedTask;
        }
    }
}
