using Cursive.Communication.Dtos.Responses;
using Cursive.Application.Services.Interfaces;
using Cursive.Domain.Entities;

namespace Cursive.Application.Services;

public class CryptoService : ICryptoService
{
    public bool ComparePassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    public void EncryptPassword(User user)
    {
        user.Salt = GenerateSalt();
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, user.Salt);
    }

    public string GenerateSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt();
    }
}
