﻿using Cursive.API.Resources;

namespace Cursive.API.Middlewares;

public class GlobalExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception) 
        {
            context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;

            object response = new 
            {
                Message = ApiMessages.INTERNAL_SERVER_ERROR,
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
