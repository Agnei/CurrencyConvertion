using Infrastructure.Core.MediatR.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Core.Http
{
    public interface ICommandHttpClient
    {
        ValueTask<CommandResult<TResult>> GetStringAsync<TResult>(
              string uri,
              CancellationToken cancellationToken = default(CancellationToken)
          ) where TResult : class;
    }
}
