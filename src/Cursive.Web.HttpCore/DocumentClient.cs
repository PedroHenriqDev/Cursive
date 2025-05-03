using System.Net.Http.Headers;
using System.Xml.Schema;
using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.Requests;
using Cursive.Communication.Dtos.Responses;
using Cursive.Web.HttpCore.Helpers;
using Cursive.Web.HttpCore.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cursive.Web.HttpCore;

public class DocumentClient : IDocumentClient
{
    private readonly Uri _baseUrl;
    private readonly string _baseApiController;

    public DocumentClient(IConfiguration configuration)
    {
        _baseUrl = new Uri(configuration["httpClient:baseUrl"] ?? throw new ArgumentNullException("The url base cannot be null."));
        _baseApiController = configuration["httpClient:baseApiDocumentController"] ?? throw new ArgumentNullException("The url of api cannot be null.");
    }

    public async Task<IResponseDto<DocumentResponse>?> CreateAsync(DocumentRequest request, string apiToken)
    {
        using(HttpClient httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            httpClient.BaseAddress = _baseUrl;

            HttpResponseMessage httpResponse = await httpClient.PostAsync($"{_baseApiController}", ConvertHelper.ConvertToStringContent(request));

            return await ConvertHelper.ConvertToResponseDtoAsync<DocumentResponse>(httpResponse);
        }
    }

    public async Task<IResponseDto<DocumentResponse>?> GetByIdAsync(Guid documentId, string apiToken)
    {
        using(var  httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            httpClient.BaseAddress = _baseUrl;
            HttpResponseMessage httpResponse = await httpClient.GetAsync($"{_baseApiController}{documentId}");

            return await ConvertHelper.ConvertToResponseDtoAsync<DocumentResponse>(httpResponse);
        }
    }

    public async Task<IResponseDto<IEnumerable<DocumentResponse>>?> GetByUserIdAsync(Guid userId)
    {
        FilterDocumentRequest filterRequest = new FilterDocumentRequest { Id = userId };

        using(HttpClient httpClient = new HttpClient())
        {
            httpClient.BaseAddress = _baseUrl;

            HttpResponseMessage httpResponse = await httpClient.GetAsync($"{_baseApiController}search{ConvertHelper.ConvertToQueryString(filterRequest)}");

            return await ConvertHelper.ConvertToResponseDtoAsync<IEnumerable<DocumentResponse>>(httpResponse);
        }
    }
}
