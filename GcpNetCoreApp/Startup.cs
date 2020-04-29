using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Diagnostics.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GcpNetCoreApp
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
            services.AddRazorPages();

            // Add exception logging.
            services.AddGoogleExceptionLogging(options =>
            {
                options.ProjectId = Configuration["Google:ProjectId"];
                options.ServiceName = Configuration["Google:AppEngine:ServiceName"] ?? "WebApp";
                options.Version = Configuration["Google:AppEngine:Version"] ?? "0.0";
            });
            // Add trace service.
            services.AddGoogleTrace(options =>
            {
                options.ProjectId = Configuration["Google:ProjectId"];
                options.Options = Google.Cloud.Diagnostics.Common.TraceOptions.Create(
                    bufferOptions: Google.Cloud.Diagnostics.Common.BufferOptions.NoBuffer());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            ILoggerFactory loggerFactory)
        {
            // Configure logging service.
            loggerFactory.AddGoogle(app.ApplicationServices, Configuration["Google:ProjectId"]);

            // Configure trace service.
            app.UseGoogleTrace();

            app.UseGoogleExceptionLogging();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //    // Configure error reporting service.
            //    app.UseGoogleExceptionLogging();
            //}

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
