using System.Net;
using System.Text.Json.Serialization;
using Cursive.Application.Dtos.Interfaces;

namespace Cursive.API.Dtos;

public class ResponseDto<T> : IResponseDto<T> 
{
    public ResponseDto(HttpStatusCode statusCode, IList<string> messages, T data)
    {
        StatusCode = statusCode;
        Messages = messages;
        Data = data;
    }

    public ResponseDto()
    {
    }

    [JsonIgnore]
    public HttpStatusCode StatusCode { get; private set; }
    public IList<string> Messages { get; private set; } = [];
    public T? Data { get; private set; }
}
