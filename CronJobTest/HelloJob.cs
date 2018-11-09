using System;
using System.Threading.Tasks;
using Quartz;

namespace CronJobTest
{
    class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync($"{DateTime.Now} Hello!");
        }
    }
}
