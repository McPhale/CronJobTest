using CronJobTestCore;
using System;
using System.Threading.Tasks;
using Topshelf;
using Microsoft.Extensions.DependencyInjection;

namespace CronJobTestCoreCore
{
    static class Program
    {
        static void Main(string[] args)
        {
            //Set up services container
            IServiceCollection services = new ServiceCollection();
            //Instantiate singletons 
            Startup startup = new Startup();
            //Add services 
            startup.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();



            var rc = HostFactory.Run(x =>
            {
                x.Service<JobScheduler>(s =>
                {
                    s.ConstructUsing(name => serviceProvider.GetService<JobScheduler>());
                    s.WhenStarted(js => js.Start());
                    s.WhenStopped(js => js.Stop());
                    s.WhenShutdown(js => js.Stop());
                });
                x.StartAutomatically();
                x.EnableShutdown();
                x.RunAsLocalSystem();

                x.SetDescription("Prints hello/goodbye to console/log file at 5 min intervals.");
                x.SetDisplayName("CronJobCore");
                x.SetServiceName("CronJob");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
