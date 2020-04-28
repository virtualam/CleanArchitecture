using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorrelationFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogWarning("hello");
            host.Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
             .ConfigureServices((hostContext, services) =>
              {
                  services.AddTargetClient(options =>
                  {
                      options.TargetClient = hostContext.Configuration.GetValue<string>("baseUrls:targetClient");
                  });

                  services.AddSingleton<CorrelationIdProvider>(_ => () => Guid.NewGuid().ToString());

                  services.AddHostedService<App>();
              });
            return hostBuilder;
        }
    }
}
