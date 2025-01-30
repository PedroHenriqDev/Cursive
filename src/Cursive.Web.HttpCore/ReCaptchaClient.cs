using Cursive.Communication.Dtos;
using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.User.Responses;
using Cursive.Web.HttpCore.Helpers;
using Cursive.Web.HttpCore.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Cursive.Web.HttpCore;

public class ReCaptchaClient : IReCaptchaClient
{
    private readonly IConfiguration _configuration;
    private readonly Uri _apiUrl;

    public ReCaptchaClient(IConfiguration configuration)
    {
        _configuration = configuration;
        _apiUrl = new Uri(_configuration["ReCaptcha:ApiUrl"] ?? throw new ArgumentNullException());
    }

    public async Task<IResponseDto<ReCaptchaResponse?>?> ValidateReCaptchaAsync(string recaptchaToken)
    {
        string secretKey = _configuration["ReCaptcha:SecretKey"] ?? throw new ArgumentNullException();

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage httpResponse = await client.GetAsync($"{_apiUrl}?secret={secretKey}&response={recaptchaToken}");
            string contentAsJson = await httpResponse.Content.ReadAsStringAsync();
            ReCaptchaResponse? recaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(contentAsJson);
         
            return new ResponseDto<ReCaptchaResponse?>(httpResponse.StatusCode, [], recaptchaResponse);
        }
    }
}
