using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cursive.Application.Exceptions;
using Cursive.Application.Resources;
using Cursive.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cursive.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(IList<Claim> authClaims)
        {
            (string secretKey, DateTime expireTime) = GetConfigurations();

            SigningCredentials signingCredentials = GenerateSigningCredentials(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = expireTime,
                Subject = new ClaimsIdentity(authClaims),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }

        public SigningCredentials GenerateSigningCredentials(string secretKey)
        {
            byte[] secretKeyAsBase64 = Encoding.ASCII.GetBytes(secretKey);
            var symetricKey = new SigningCredentials(new SymmetricSecurityKey(secretKeyAsBase64), SecurityAlgorithms.HmacSha256Signature);
            return symetricKey;
        }

        public (string secretKey, DateTime expireTime) GetConfigurations()
        {
            string secretKey = _configuration["tokenJwt:secretKey"] ?? throw new TokenException(Messages.NOT_FOUND_SECRET_KEY);
            int expireTimeInMinutes = Convert.ToInt32(_configuration["tokenJwt:secretKey"] ?? throw new TokenException(Messages.NOT_FOUND_EXPIRE_TIME));
            return (secretKey, DateTime.Now.AddMinutes(expireTimeInMinutes));
        }
    }
}
