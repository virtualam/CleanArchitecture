using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorrelationFactory
{
    /// <summary>
    /// A Middleware that injects session from a Request header or creates a new one if unavailable
    /// </summary>
    public class CorrelationMiddleware
    {
        private readonly ILoggerScope _loggerScope;
        private readonly CorrelationIdProvider _correlationIdProvider;
        private readonly RequestDelegate _next;

        public CorrelationMiddleware(ILoggerScope loggerScope, CorrelationIdProvider correlationIdProvider, RequestDelegate next)
        {
            this._loggerScope = loggerScope;
            _correlationIdProvider = correlationIdProvider;
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var header = context.Request.Headers[Constants.CorrelationIDHeader];
            string correlationId;

            if (header.Count > 0)
            {
                correlationId = header[0];
            }
            else
            {
                correlationId = _correlationIdProvider.Invoke();
            }

            context.Items[Constants.CorrelationIDHeader] = correlationId;

            var logger = context.RequestServices.GetRequiredService<ILogger<CorrelationMiddleware>>();

            //using (_loggerScope.BeginScope("{@CorrelationId}", correlationId))
            using (logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId
            }))
            {
                await this._next(context);
            }
        }
    }
}
