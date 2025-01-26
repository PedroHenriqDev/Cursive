using Cursive.Web.MVC.Services;
using Cursive.Web.MVC.Services.Interfaces;

namespace Cursive.Web.MVC.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebServices(this IServiceCollection app)
    {
        return app.AddScoped<IAuthenticationService, AuthenticationService>();
    }
}
