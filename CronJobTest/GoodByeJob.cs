using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace CronJobTest
{
    //[DisallowConcurrentExecution]
    class GoodByeJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
             Console.Out.WriteLine($"{DateTime.Now} Goodbye!");
        }
    }
}
