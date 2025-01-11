namespace Cursive.API.Middlewares;

public static class MiddlewareResolver
{
    public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app) 
    {
        return app.UseMiddleware<GlobalExceptionMiddleware>();
    }
}
