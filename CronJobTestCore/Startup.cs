using CronJobTestCore;
using CronJobTestCore.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using NLog.Extensions.Logging;

namespace CronJobTestCoreCore
{
    public class Startup
    {
        IConfiguration AppSettings { get; }

        public Startup()
        {
            AppSettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(AppSettings);
            services.AddHttpClient<JsonPlaceholderClient>();
            services.UseQuartz(typeof(HelloJob), typeof(GoodByeJob), typeof(JsonTodoJob));
            services.AddLogging(options => options.AddNLog(new NLogProviderOptions { CaptureMessageProperties = true, CaptureMessageTemplates = true }));
            //services.AddLogging(options => options.AddNLog());
            services.AddSingleton<JobScheduler>();
        }
    }
}
