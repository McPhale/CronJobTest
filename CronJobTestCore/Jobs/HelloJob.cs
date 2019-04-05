using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CronJobTestCore
{
    [DisallowConcurrentExecution]
    class HelloJob : IJob
    {
        private readonly ILogger<GoodByeJob> _logger;
        public HelloJob( ILogger<GoodByeJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello!");
            return Task.CompletedTask;
        }
    }
}
