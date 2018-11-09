using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronJobTest
{
    class Program
    {
        static void Main(string[] args)
        {
            JobScheduler.StartAsync().GetAwaiter().GetResult();
        }
    }
}
