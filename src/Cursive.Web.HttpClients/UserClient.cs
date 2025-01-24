using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.User.Requests;
using Cursive.Communication.Dtos.User.Responses;
using Cursive.Web.HttpClients.Helpers;
using Cursive.Web.HttpClients.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cursive.Web.HttpClients;

public class UserClient : IUserClient
{
    private readonly IConfiguration _configuration;
    private readonly Uri _baseUrl;
    private readonly string _baseApiController;

    public UserClient(IConfiguration configuration)
    {
        _configuration = configuration;
        _baseUrl = new Uri(configuration["httpClient:baseUrl"] ?? throw new ArgumentNullException("The url base cannot be null."));
        _baseApiController = configuration["httpClient:baseApiUserController"] ?? throw new ArgumentNullException("The url of api cannot be null.");
    }

    public async Task<IResponseDto<UserResponse>?> CreateAsync(UserRequest userRequest)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.BaseAddress = _baseUrl;

            HttpResponseMessage httpResponse = await httpClient.PostAsync(_baseApiController, ConvertHelper.ConvertToStringContent(userRequest));

            return await ConvertHelper.ConvertToResponseDtoAsync<UserResponse>(httpResponse);
        }
    }

    public async Task<IResponseDto<TokenResponse>?> LoginAsync(LoginRequest loginRequest)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.BaseAddress = _baseUrl;

            HttpResponseMessage httpResponse = await httpClient.PostAsync(_baseApiController + "login", ConvertHelper.ConvertToStringContent(loginRequest));

            return await ConvertHelper.ConvertToResponseDtoAsync<TokenResponse>(httpResponse);
        }
    }
}
