using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Core.MediatR.Commands;

namespace Infrastructure.Core.Http
{
    public class CommandHttpClient : ICommandHttpClient
    {
        private readonly IHttpClient _httpClient;

        public CommandHttpClient(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async ValueTask<CommandResult<TResult>> GetStringAsync<TResult>(string uri, CancellationToken cancellationToken = default(CancellationToken)) where TResult : class
        {
            var result = await _httpClient.GetAsync(uri, cancellationToken);
            return await result.ToCommandResult<TResult>();
        }
        
    }
}
