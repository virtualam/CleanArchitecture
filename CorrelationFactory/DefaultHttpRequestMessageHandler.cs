using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorrelationFactory
{
    /// <summary>
    /// This is a handler that delegates processing of HTTP response messages to another handler, 
    /// but not before adding the required request headers
    /// </summary>
    public class DefaultHttpRequestMessageHandler: DelegatingHandler
    {
        private readonly ICorrelationIdAccessor _correlationIdAccessor;

        public DefaultHttpRequestMessageHandler(ICorrelationIdAccessor correlationIdAccessor)
        {
            this._correlationIdAccessor = correlationIdAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add(Constants.RequestIDHeader, _correlationIdAccessor.CorrelationId);
            request.Headers.Add(Constants.CorrelationIDHeader, _correlationIdAccessor.CorrelationId);

            return base.SendAsync(request, cancellationToken);
        }
    }

    public class DefaultHttpRequestMessageHandlerNonWeb : DelegatingHandler
    {
        private readonly CorrelationIdProvider _correlationIdProvider;

        public DefaultHttpRequestMessageHandlerNonWeb(CorrelationIdProvider correlationIdProvider)
        {
            _correlationIdProvider = correlationIdProvider;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var correlationId = _correlationIdProvider.Invoke();

            request.Headers.Add(Constants.RequestIDHeader, correlationId);
            request.Headers.Add(Constants.CorrelationIDHeader, correlationId);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
