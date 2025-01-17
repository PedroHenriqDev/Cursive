using System.Net;

namespace Cursive.Communication.Dtos.Interfaces;

public interface IResponseDto<T> 
{
    HttpStatusCode StatusCode { get; }
    IList<string> Messages { get; }
    T? Data { get; }
}
