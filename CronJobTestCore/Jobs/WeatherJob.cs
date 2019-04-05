using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Microsoft.Extensions.Logging;

namespace CronJobTestCore
{
    [DisallowConcurrentExecution]
    class JsonTodoJob : IJob
    {
        private readonly JsonPlaceholderClient _jsonPlaceholderClient;
        private readonly ILogger<JsonTodoJob> _logger;

        public JsonTodoJob(JsonPlaceholderClient jsonPlaceholderClient, ILogger<JsonTodoJob> logger)
        {
            _jsonPlaceholderClient = jsonPlaceholderClient;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            //use this for http client factory
            //https://jsonplaceholder.typicode.com/todos/1
            var result = await _jsonPlaceholderClient.GetTodo(1);
            _logger.LogInformation(result.ToString());
        }
    }
}
