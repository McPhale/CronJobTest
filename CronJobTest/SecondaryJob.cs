using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace CronJobTest
{
    //[DisallowConcurrentExecution]
    class SecondaryJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync($"{DateTime.Now} Hello from Secondary job!");
        }
    }
}
