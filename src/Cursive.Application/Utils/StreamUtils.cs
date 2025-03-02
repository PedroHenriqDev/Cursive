using System.Text.Json;

namespace Cursive.Application.Utils;

public class StreamUtils
{
    public static async Task<T?> ConvertAsync<T>(Stream stream) where T : class
    {
        using(StreamReader stremReader =  new StreamReader(stream, leaveOpen: true))
        {
            object body = await stremReader.ReadToEndAsync();

            if (body == null)
                return null;

            string bodyAsString = body.ToString();

            return JsonSerializer.Deserialize<T>(body.ToString()!, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
