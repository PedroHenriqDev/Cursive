using System.Security.Claims;
using Cursive.Application.Services.Interfaces;

namespace Cursive.Web.MVC.Extensions;

public static class ClaimPrincipalExtension
{
    public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal, ITokenService tokenService)
    {
        string token = claimsPrincipal.FindFirst("ApiToken")?.Value ?? string.Empty;
        IList<Claim> authClaims = tokenService.GetAuthClaims(token);
        return new Guid(authClaims.FirstOrDefault(c => c.Type == "ID")?.Value ?? Guid.Empty.ToString());
    }

    public static string GetApiToken(this ClaimsPrincipal user)
    {
        return user.FindFirst("ApiToken")?.Value ?? string.Empty;
    }
}
