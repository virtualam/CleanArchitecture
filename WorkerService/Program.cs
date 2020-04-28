using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args);
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                string url = hostContext.Configuration.GetValue<string>("baseUrls:targetClient");
                services.AddHostedService<Worker>();
            });
            return hostBuilder;
        }
    }
}
