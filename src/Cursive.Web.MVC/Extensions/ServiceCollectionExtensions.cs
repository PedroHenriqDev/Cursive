using Cursive.Application.Services;
using Cursive.Application.Services.Interfaces;
using Cursive.Web.MVC.Services;
using Cursive.Web.MVC.Services.Interfaces;

namespace Cursive.Web.MVC.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebServices(this IServiceCollection app)
    {
         app.AddScoped<IAuthenticationService, AuthenticationService>();
         app.AddScoped<ITokenService, TokenService>();
         return app;
    }

    public static IServiceCollection AddCookieAuth(this IServiceCollection app, IConfiguration configuration)
    {
        string? cookieName = configuration["Keys:TokenCookieKey"];

        app.AddAuthentication("CookieAuth").AddCookie("CookieAuth", opt =>
        {
            opt.Cookie.Name = cookieName;
            opt.LoginPath = "/User/Login";
            opt.AccessDeniedPath = "/User/Login";
            opt.Cookie.HttpOnly = true;
            opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            opt.Cookie.SameSite = SameSiteMode.Lax;
        });

        return app;
    }
}
