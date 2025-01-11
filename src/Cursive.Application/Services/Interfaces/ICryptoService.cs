using Cursive.Domain.Entities;

namespace Cursive.Application.Services.Interfaces;

public interface ICryptoService
{
    void EncryptPassword(User user);
    bool ComparePassword(string password, string passwordHash);
    string GenerateSalt();
}
