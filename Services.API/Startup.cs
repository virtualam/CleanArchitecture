using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorrelationFactory;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services.API.Extensions;

namespace Services.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructureServices(Configuration);
            services.AddAppServices();

            services.AddControllers(options =>
            {
                //options.Filters.Add<RequestValidationFilter>();
            })
            .AddNewtonsoftJson()
            .AddControllerServices();

            services.AddSingleton<CorrelationIdProvider>(_ => () => Guid.NewGuid().ToString());

            services.AddTransient<ILoggerScope, Infrastructure.Serilog.LoggerScope>();

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<CorrelationFactory.CorrelationMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            ///app.UseGenericExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
