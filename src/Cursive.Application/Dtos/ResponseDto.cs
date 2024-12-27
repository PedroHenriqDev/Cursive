using System.Net;
using Cursive.Application.Dtos.Interfaces;

namespace Cursive.API.Dtos;

public class ResponseDto<T> : IResponseDto<T> 
{
    public ResponseDto(HttpStatusCode statusCode, IList<string> messages, T data)
    {
        StatusCode = statusCode;
        Messages = messages;
        Date = DateTime.Now;
        Data = data;
    }

    public ResponseDto()
    {
    }

    public HttpStatusCode StatusCode { get; private set; }
    public IList<string> Messages { get; private set; } = [];
    public DateTime Date { get; private set; }
    public T? Data { get; private set; }
}
