using System.Security.Claims;
using Cursive.Application.Services.Interfaces;

namespace Cursive.Application.Services;

public class ClaimService : IClaimService
{
    public IList<Claim> CreateAuthClaims(string name, string id)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, name),
            new Claim(nameof(id).ToUpper(), id)
        };

        return claims;
    }
}
