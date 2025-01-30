using Cursive.Communication.Dtos.User.Responses;
using Cursive.Web.HttpCore.Helpers;
using Cursive.Web.MVC.Services.Interfaces;

namespace Cursive.Web.MVC.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly string _key;

    public AuthenticationService(IConfiguration configuration)
    {
        _key = configuration["keys:TokenCookieKey"] ?? throw new ArgumentNullException();
    }

    public void SetAuthSession(HttpContext httpContext, TokenResponse tokenResponse)
    {
        var cookiesOptions = new CookieOptions()
        {
            Expires = tokenResponse?.ExpireTime,
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax
        };

        httpContext.SetCookie(_key, tokenResponse!.Token, cookiesOptions);
    }
}
