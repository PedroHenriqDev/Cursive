using Cursive.Application.Exceptions;
using Cursive.Application.Resources;
using Microsoft.Extensions.Configuration;

namespace Cursive.Application.Utils;

public static class ConfigurationUtils
{
    public static (string secretKey, DateTime expireTime) GetJwtConfig(this IConfiguration configuration)
    {
        string secretKey = configuration["tokenJwt:secretKey"] ?? throw new TokenException(Messages.NOT_FOUND_SECRET_KEY);
        int expireTimeInMinutes = Convert.ToInt32(configuration["TokenJwt:expireTime"] ?? throw new TokenException(Messages.NOT_FOUND_EXPIRE_TIME));
        return (secretKey, DateTime.UtcNow.AddMinutes(expireTimeInMinutes));
    }
}
