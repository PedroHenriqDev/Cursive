using Cursive.Communication.Dtos;
using Cursive.Communication.Dtos.Interfaces;
using Newtonsoft.Json;

namespace Cursive.Web.HttpClients.Helpers;

public static class ConvertHelper
{
    public static StringContent ConvertToStringContent(object objToConvert)
    {
        return new StringContent(JsonConvert.SerializeObject(objToConvert), System.Text.Encoding.UTF8, "application/json");
    }

    public static async Task<IResponseDto<T>?> ConvertToResponseDtoAsync<T>(HttpResponseMessage httpResponse) where T : class
    {
        return JsonConvert.DeserializeObject<IResponseDto<T>>(await httpResponse.Content.ReadAsStringAsync());
       
    }
}
