using System.Security.Claims;
using Cursive.Application.Services.Interfaces;

namespace Cursive.Web.MVC.Extensions;

public static class ClaimPrincipalExtension
{
    public static string GetUserId(this ClaimsPrincipal claimsPrincipal, ITokenService tokenService)
    {
        string token = claimsPrincipal.FindFirst("ApiToken")?.Value ?? string.Empty;
        IList<Claim> authClaims = tokenService.GetAuthClaims(token);
        return authClaims.FirstOrDefault(c => c.Type == "ID")?.Value ?? string.Empty;
    }
}
