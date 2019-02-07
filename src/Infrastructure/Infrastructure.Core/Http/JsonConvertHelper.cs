using Infrastructure.Core.MediatR.Commands;
using Newtonsoft.Json;

namespace Infrastructure.Core.Http
{
    public static class JsonConvertHelper
    {
        public static CommandResult<T> Deserialize<T>(string content) where T : class
        {
            try
            {
                return CommandResult<T>.Success(JsonConvert.DeserializeObject<T>(content));
            }
            catch (JsonException ex)
            {
                return CommandResult<T>.Failed(string.Empty, ex.Message);
            }
        }
    }
}
