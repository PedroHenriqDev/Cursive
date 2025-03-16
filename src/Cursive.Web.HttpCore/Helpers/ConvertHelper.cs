using System.Net.Http.Headers;
using Cursive.Communication.Dtos;
using Cursive.Communication.Dtos.Interfaces;
using Newtonsoft.Json;

namespace Cursive.Web.HttpCore.Helpers;

public static class ConvertHelper
{
    public static StringContent ConvertToStringContent(object objToConvert)
    {
        return new StringContent(JsonConvert.SerializeObject(objToConvert), System.Text.Encoding.UTF8, "application/json");
    }

    public static async Task<IResponseDto<T>?> ConvertToResponseDtoAsync<T>(HttpResponseMessage httpResponse) where T : class
    {
        try
        {
            string contentAsJson = await httpResponse.Content.ReadAsStringAsync();
            ResponseDto<T>? responseDto = JsonConvert.DeserializeObject<ResponseDto<T>>(contentAsJson);

            if (responseDto != null)
                responseDto.StatusCode = httpResponse.StatusCode;

            return responseDto;
        }
        catch (JsonException ex)
        {
            Console.WriteLine("Erro na deserialização: " + ex.Message);
            return null;
        }
    }

    public static string ConvertToQueryString<T>(T obj)
    {
         return $"?{string.Format("&", typeof(T)
             .GetProperties()
             .Where(p => p.GetValue(obj) != null)
             .Select(p => $"{p.Name}={p.GetValue(obj)}"))}";
    } 
}
