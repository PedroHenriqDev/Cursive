using System.Security.Claims;
using Cursive.Application.Dtos.User.Responses;

namespace Cursive.Application.Services.Interfaces;

public interface ITokenService
{
    TokenResponse GenerateToken(IList<Claim> authClaims);
}
