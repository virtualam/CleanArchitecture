using CorrelationFactory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public static class TargetClientExtensions
    {
        public static void AddTargetClient(this IServiceCollection services, Action<BaseUrls> configure)
        {
            //var baseUrls = new BaseUrls();
            //hostContext.Configuration.GetSection("baseUrls").Bind(baseUrls);

            //services.Configure<BaseUrls>(hostContext.Configuration.GetSection("baseUrls"));

            //services.AddHttpClient<TargetClient>((client) =>
            //{
            //    client.BaseAddress = new System.Uri(baseUrls.TargetClient);
            //});

            services.AddTransient<DefaultHttpRequestMessageHandlerNonWeb>();

            services.Configure(configure);

            services.AddHttpClient("MyClient")
                .AddHttpMessageHandler<DefaultHttpRequestMessageHandlerNonWeb>(); 

            services.AddSingleton<TargetClient>();
        }
    }
}
