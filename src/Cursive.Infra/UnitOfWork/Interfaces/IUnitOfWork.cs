using Cursive.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cursive.Infra.UnitOfWork.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IDocumentRepository DocumentRepository { get; }
    Task SaveAsync();
    Task<IDbContextTransaction> InitTransactionAsync();
}
