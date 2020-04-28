using Domain.Interfaces;
using Infrastructure.Redis;
using Infrastructure.RepoServices;
using Infrastructure.SQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;
using Serilog.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class Extensions
    {
        //public static void SetupLoggingFramework()
        //{
        //    Log.Logger = new LoggerConfiguration().CreateLogger();
        //}

        public static ILoggerFactory GetLoggerFactory()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            LoggerFactory f = new LoggerFactory();
            f.AddSerilog();
            return f;
        }

        public static void Cleanup()
        {
            Log.CloseAndFlush();
        }

        public static IHostBuilder AddInfrastructureHostBuilderConfig(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((hostBuilderContext, loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
            });
            return hostBuilder;
        }

        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheSettings = configuration.GetSection(nameof(RedisCacheSettings)).Get<RedisCacheSettings>();
            //configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);

            if (!redisCacheSettings.Enabled)
            {
                //Registers Distributed Memory Cache service
                services.AddDistributedMemoryCache();
            }
            else
            {

                services.AddSingleton(redisCacheSettings);
                services.AddSingleton<IConnectionMultiplexer>(_ =>
                    ConnectionMultiplexer.Connect(redisCacheSettings.ConnectionString));

                //Registers Redis Distributed Cache service
                //services.AddStackExchangeRedisCache(options => options.Configuration = redisCacheSettings.ConnectionString);
            }

            //services.AddSingleton<IResponseCacheService, ResponseCacheService>();

            services.AddTransient<IDbConnectionFactory, Dapper.SqlServer.ConnectionFactory>();
            services.AddTransient<IPaginationFilter, PaginationFilter>();
        }
    }
}
