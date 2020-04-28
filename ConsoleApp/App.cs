using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class App : BackgroundService
    {
        private readonly ILogger<App> _logger;
        private readonly TargetClient _targetClient;

        public App(ILogger<App> logger, TargetClient targetClient)
        {
            _logger = logger;
            _targetClient = targetClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var headers = await _targetClient.Get(stoppingToken);

            Console.WriteLine("Hello World");

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}

            await Task.CompletedTask;
        }
    }
}
