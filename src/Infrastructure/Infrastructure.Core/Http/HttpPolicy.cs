using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;


namespace Infrastructure.Core.Http
{
    public static class HttpPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int exponentialBackoff = 2, int retryAttemptCount = 6)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(retryAttemptCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(exponentialBackoff, retryAttempt)));
        }

        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(int handledEventsAllowedBeforeBreaking = 5, int durationOfBreakSeconds = 30)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(durationOfBreakSeconds));
        }
    }

}
