using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cursive.Application.Services.Interfaces;
using Cursive.Application.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Cursive.Communication.Dtos.Responses;
using Cursive.Application.Resources;
using Microsoft.AspNetCore.Http;

namespace Cursive.Application.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenResponse GenerateToken(IList<Claim> authClaims)
    {
        (string secretKey, DateTime expireTime) = _configuration.GetJwtConfig();

        SigningCredentials signingCredentials = GenerateSigningCredentials(secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = expireTime,
            Subject = new ClaimsIdentity(authClaims),
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
        
        return new TokenResponse(tokenHandler.WriteToken(token), expireTime);
    }

    public IList<Claim> GetAuthClaims(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);

        if (jwtToken == null)
            throw new ArgumentNullException(Messages.INVALID_TOKEN);

        return jwtToken.Claims.ToList();
    }

    public SigningCredentials GenerateSigningCredentials(string secretKey)
    {
        byte[] secretKeyAsBase64 = Encoding.ASCII.GetBytes(secretKey);
        var symetricKey = new SigningCredentials(new SymmetricSecurityKey(secretKeyAsBase64), SecurityAlgorithms.HmacSha256Signature);
        return symetricKey;
    }

    public string GetTokenByHttpRequest(HttpContext context)
    {
        string token = string.Empty;

        if (context.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            token = authHeader.ToString().Split(" ").Last();
        }

        return token;
    }
}
