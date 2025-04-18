using System.Security.Claims;
using Cursive.Application.Services.Interfaces;
using Cursive.Communication.Dtos.Responses;
using Cursive.Domain.Entities;
using Cursive.Web.HttpCore.Helpers;
using Cursive.Web.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace Cursive.Web.MVC.Services;

public class AuthenticationService : Interfaces.IAuthenticationService
{
    public readonly ITokenService _tokenService;
    private readonly string _key;

    public AuthenticationService(IConfiguration configuration, ITokenService tokenService)
    {
        _key = configuration["Keys:TokenCookieKey"] ?? throw new ArgumentNullException();
        _tokenService = tokenService;
    }

    public async Task SignInAsync(HttpContext httpContext, TokenResponse tokenResponse)
    {
        IList<Claim> claims = _tokenService.GetAuthClaims(tokenResponse.Token);
        claims.Add(new Claim("ApiToken", tokenResponse.Token));

        var identity = new ClaimsIdentity(claims, "CookieAuth");
        var principal = new ClaimsPrincipal(identity);

        await httpContext.SignInAsync("CookieAuth", principal, new AuthenticationProperties { ExpiresUtc = tokenResponse.ExpireTime, IsPersistent = true});
    }

    public void RemoveAuthSession(HttpContext httpContext)
    {
        httpContext.RemoveCookie(_key);
    }
}
