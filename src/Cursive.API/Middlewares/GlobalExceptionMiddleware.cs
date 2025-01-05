using Cursive.API.Resources;
using Cursive.Logging.Loggers.Interfaces;

namespace Cursive.API.Middlewares;

public class GlobalExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex) 
        {
            IFileLogger<GlobalExceptionMiddleware> fileLogger = GetFileLogger(context);

            await fileLogger.LogErrorAsync(ex.Message);

            context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;

            object response = new 
            {
                Message = ApiMessages.INTERNAL_SERVER_ERROR,
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }

    public IFileLogger<GlobalExceptionMiddleware> GetFileLogger(HttpContext context)
    {
        IFileLogger<GlobalExceptionMiddleware> fileLogger = context.RequestServices.GetRequiredService<IFileLogger<GlobalExceptionMiddleware>>();
        fileLogger.LoggerContext.UseContext = false;
        return fileLogger;
    }
}
