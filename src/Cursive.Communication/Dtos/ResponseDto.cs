using System.Net;
using System.Text.Json.Serialization;
using Cursive.Communication.Dtos.Interfaces;

namespace Cursive.Communication.Dtos;

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
    public HttpStatusCode StatusCode { get; set; }
    public IList<string> Messages { get; private set; } = [];
    public T? Data { get; set; }
}
