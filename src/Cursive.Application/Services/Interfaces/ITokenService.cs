using System.Security.Claims;
using Cursive.Communication.Dtos.Responses;
using Microsoft.AspNetCore.Http;

namespace Cursive.Application.Services.Interfaces;

public interface ITokenService
{
    TokenResponse GenerateToken(IList<Claim> authClaims);
    IList<Claim> GetAuthClaims(string token);
    string GetTokenByHttpRequest(HttpContext context);
}
