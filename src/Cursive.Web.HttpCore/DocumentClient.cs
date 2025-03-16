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
