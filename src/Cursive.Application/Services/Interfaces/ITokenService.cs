using System.Security.Claims;

namespace Cursive.Application.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(IList<Claim> authClaims);
}
