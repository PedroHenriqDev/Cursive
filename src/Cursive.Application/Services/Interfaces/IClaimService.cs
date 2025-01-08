using System.Security.Claims;

namespace Cursive.Application.Services.Interfaces;

public interface IClaimService
{
    IList<Claim> CreateAuthClaims(string name, string id);
}
