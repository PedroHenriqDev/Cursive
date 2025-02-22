using Cursive.Communication.Dtos;
using Cursive.Communication.Dtos.Interfaces;

namespace Cursive.Communication.Factories;

public static class ResponseFactory
{
    public static IResponseDto<T> BadRequest<T>(string message, T value)
    {
        return new ResponseDto<T>(System.Net.HttpStatusCode.BadRequest, new List<string> { message }, value);
    }


    public static IResponseDto<T> BadRequest<T>(IList<string> messages, T value)
    {
        return new ResponseDto<T>(System.Net.HttpStatusCode.BadRequest, messages, value);
    }

    public static IResponseDto<T> Created<T>(string message, T value)
    {
        return new ResponseDto<T>(System.Net.HttpStatusCode.Created, new List<string> { message }, value);
    }

    public static IResponseDto<T> Created<T>(IList<string> messages, T value)
    {
        return new ResponseDto<T>(System.Net.HttpStatusCode.Created, messages, value);
    }

    public static IResponseDto<T> NotFound<T>(IList<string> messages, T value)
    {
        return new ResponseDto<T>(System.Net.HttpStatusCode.NotFound, messages, value);
    }

    public static ResponseDto<T>  NotFound<T>(string message, T value)
    {
        return new ResponseDto<T>(System.Net.HttpStatusCode.NotFound, new List<string> { message }, value);
    }

    public static IResponseDto<T> Ok<T>(string message, T value)
    {
        return new ResponseDto<T>(System.Net.HttpStatusCode.OK, new List<string> { message }, value);
    }

    public static IResponseDto<T> Ok<T>(IList<string> messages, T value)
    {
        return new ResponseDto<T>(System.Net.HttpStatusCode.OK, messages, value);
    }
}
