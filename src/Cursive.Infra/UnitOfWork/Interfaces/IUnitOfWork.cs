using Cursive.Domain.Repositories.Interfaces;

namespace Cursive.Infra.UnitOfWork.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
}
