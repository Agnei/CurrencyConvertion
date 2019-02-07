using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Core.Http
{
    public class StandardHttpClient : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public StandardHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        public async ValueTask<HttpResponseMessage> GetAsync(string uri, CancellationToken cancellationToken)
        {
            var message =
                new HttpRequestMessage(HttpMethod.Get, uri);

            return await _httpClient.SendAsync(message, cancellationToken);
        }
    }
}
