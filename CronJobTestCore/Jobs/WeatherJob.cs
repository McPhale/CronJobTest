using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Quartz;

namespace CronJobTestCore
{
    [DisallowConcurrentExecution]
    class JsonTodoJob : IJob
    {
        private readonly JsonPlaceholderClient _jsonPlaceholderClient;
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();


        public JsonTodoJob(JsonPlaceholderClient jsonPlaceholderClient)
        {
            _jsonPlaceholderClient = jsonPlaceholderClient;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            //use this for http client factory
            //https://jsonplaceholder.typicode.com/todos/1
            var result = await _jsonPlaceholderClient.GetTodo(1);
            logger.Info(result.ToString());
        }
    }
}
