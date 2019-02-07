using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Core.Http
{
    public interface IHttpClient
    {
        ValueTask<HttpResponseMessage> GetAsync(
                string uri,
                CancellationToken cancellationToken = default(CancellationToken)
            );
    }
}
