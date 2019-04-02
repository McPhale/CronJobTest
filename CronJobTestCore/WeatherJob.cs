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
    class WeatherJob : IJob
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();


        public WeatherJob(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            //use this for http client factory
            //https://jsonplaceholder.typicode.com/todos/1
            var client = _httpClientFactory.CreateClient();
            var result = await client.GetAsync("https://jsonplaceholder.typicode.com/todos/1");
            //await Console.Out.WriteLineAsync(result.ToString());
            logger.Info(result.ToString());
        }
    }
}
