using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Microsoft.Extensions.Logging;

namespace CronJobTestCore
{
    //[DisallowConcurrentExecution]
    class GoodByeJob : IJob
    {
        private readonly ILogger<GoodByeJob> _logger;
        public GoodByeJob( ILogger<GoodByeJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Goodbye!");
            return Task.CompletedTask;
        }
    }
}
