using System.Security.Claims;
using Cursive.Communication.Dtos.Responses;

namespace Cursive.Application.Services.Interfaces;

public interface ITokenService
{
    TokenResponse GenerateToken(IList<Claim> authClaims);
}
