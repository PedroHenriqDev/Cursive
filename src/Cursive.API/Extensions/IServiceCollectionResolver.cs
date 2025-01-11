using System.Text;
using Cursive.Application.Utils;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.IdentityModel.Tokens;

namespace Cursive.API.Extensions;

public static class IServiceCollectionResolver
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        (string secretKey, DateTime expireTime) = configuration.GetJwtConfig();

        serviceCollection.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = BearerTokenDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = BearerTokenDefaults.AuthenticationScheme;
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
}
