using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Services.API
{
//#pragma warning disable CS1591
    public class Program
    {
        public async static Task Main(string[] args)
        {
            //AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //Infrastructure.DependencyInjection.SetupLoggingFramework();
            using (var loggerFactory = Infrastructure.Extensions.GetLoggerFactory())
            {
                var logger = loggerFactory.CreateLogger<Program>();
                try
                {
                    logger.LogInformation("Application starting up...");
                    var host = CreateHostBuilder(args).Build();
                    await host.RunAsync();
                }
                catch (Exception ex)
                {
                    logger.LogCritical(ex, "The application failed to start correctly.");
                    throw;
                }
                finally
                {
                    logger.LogInformation("Application is stopping...");
                    Infrastructure.Extensions.Cleanup();
                    System.Threading.Thread.Sleep(5000);
                }
            }
        }

        //private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        //{
        //    Infrastructure.DependencyInjection.Cleanup();
        //}

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .AddInfrastructureHostBuilderConfig()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseContentRoot(Path.GetDirectoryName(typeof(Program).Assembly.Location));
                    webBuilder.UseStartup<Startup>();
                });
            return hostBuilder;
        }
    }
//#pragma warning restore CS1591
}
