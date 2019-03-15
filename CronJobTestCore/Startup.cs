using CronJobTest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CronJobTestCore
{
    public class Startup
    {
        IConfigurationRoot AppSettings { get; }

        public Startup()
        {
            AppSettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationRoot>(AppSettings);
            services.AddSingleton<JobScheduler>();
            //services.AddDbContext<Directoryv2Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DirectoryV2"));
        }
    }
}
