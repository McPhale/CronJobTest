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
            //JobScheduler.StartAsync().GetAwaiter().GetResult();
            Console.WriteLine("hello from main");
            await JobScheduler.StartAsync();
        }
    }
}
