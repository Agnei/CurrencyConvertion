using Infrastructure.Core.MediatR.Commands;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Infrastructure.Core.Http
{
    public static class HttpRequestMessageExtension
    {
        public static async ValueTask<CommandResult<T>> ToCommandResult<T>(this HttpResponseMessage message) where T : class
        {
            if (message == null) return CommandResult<T>.Failed("HttpRequest", "message is null");

            var content = await message.Content.ReadAsStringAsync();

            return message.IsSuccessStatusCode ? JsonConvertHelper.Deserialize<T>(content) : CommandResult<T>.Failed(string.Empty, content);
        }
    }
}
