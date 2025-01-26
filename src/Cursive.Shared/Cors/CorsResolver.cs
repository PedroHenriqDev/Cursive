using Microsoft.Extensions.DependencyInjection;

namespace Cursive.Shared.Cors;

public static class CorsResolver
{
    public static IServiceCollection AddDefaultCorsAllowAll(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
            });
        });

        return serviceCollection;
    }
}
