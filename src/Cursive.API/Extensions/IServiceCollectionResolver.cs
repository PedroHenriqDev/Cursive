using System.Text;
using Cursive.API.Filters;
using Cursive.Application.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Cursive.API.Extensions;

public static class IServiceCollectionResolver
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        (string secretKey, DateTime expireTime) = configuration.GetJwtConfig();

        serviceCollection.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.RequireHttpsMetadata = false;
            opt.SaveToken = true;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                RequireExpirationTime = true,
            };
        });

        return serviceCollection;
    }

    public static IServiceCollection AddActionFilters(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<MatchUserIdFilterAttribute>();
        serviceCollection.AddScoped<MatchDocumentUserFilterAttribute>();
        return serviceCollection;
    }
}
