using Cursive.API.Dtos;

namespace Cursive.Application.Factories;

public static class ResponseFactory
{
    public static ResponseDto<T> BadRequest<T>(IList<string> messages, T value)
    {
        return new ResponseDto<T>(System.Net.HttpStatusCode.BadRequest, messages, value);
    }

    public static ResponseDto<T> Created<T>(IList<string> messages, T value)
    {
        return new ResponseDto<T>(System.Net.HttpStatusCode.Created, messages, value);
    }
}
