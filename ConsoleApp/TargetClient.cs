using CorrelationFactory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class TargetClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private BaseUrls _options;

        public TargetClient(IHttpClientFactory httpClientFactory, CorrelationIdProvider correlationIdProvider, IOptions<BaseUrls> options )
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            var correlationId = correlationIdProvider.Invoke();
            _httpClient = httpClientFactory.CreateClient("MyClient");
            _httpClient.BaseAddress = new System.Uri(_options.TargetClient);
        }

        public void Dispose()
        {
            if(_httpClient!= null)
            {
                _httpClient.Dispose();
            }
        }

        public async Task<string> Get(CancellationToken stoppingToken)
        {
            var response = await _httpClient.GetAsync("target", stoppingToken);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
