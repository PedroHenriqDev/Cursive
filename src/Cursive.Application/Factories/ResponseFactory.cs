using Cursive.API.Dtos;
using Cursive.Application.Resources;

namespace Cursive.Application.Factories;

public static class ResponseFactory
{
    public static ResponseDto<T> BadRequest<T>(string message, T value)
    {
        return new ResponseDto<T>(System.Net.HttpStatusCode.BadRequest, new List<string> { message }, value);
    }


    public static ResponseDto<T> BadRequest<T>(IList<string> messages, T value)
    {
        return new ResponseDto<T>(System.Net.HttpStatusCode.BadRequest, messages, value);
    }

    public static ResponseDto<T> Created<T>(string message, T value)
    {
        return new ResponseDto<T>(System.Net.HttpStatusCode.Created, new List<string> { message }, value);
    }

    public static ResponseDto<T> Created<T>(IList<string> messages, T value)
    {
        return new ResponseDto<T>(System.Net.HttpStatusCode.Created, messages, value);
    }


    public static ResponseDto<T> Ok<T>(string message, T value)
    {
        return new ResponseDto<T>(System.Net.HttpStatusCode.OK, new List<string> { message }, value);
    }

    public static ResponseDto<T> Ok<T>(IList<string> messages, T value)
    {
        return new ResponseDto<T>(System.Net.HttpStatusCode.OK, messages, value);
    }
}
