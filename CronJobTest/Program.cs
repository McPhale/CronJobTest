using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronJobTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Console.Out.WriteLineAsync("Press enter to exit.");
            await JobScheduler.StartAsync();
            await SecondaryJobScheduler.StartAsync();
            await Console.In.ReadLineAsync();
        }
    }
}
