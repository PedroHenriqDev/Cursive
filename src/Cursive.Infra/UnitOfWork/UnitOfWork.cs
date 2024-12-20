using Cursive.Domain.Repositories.Interfaces;
using Cursive.Infra.Data;
using Cursive.Infra.Repositories;
using Cursive.Infra.UnitOfWork.Interfaces;

namespace Cursive.Infra.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(CursiveDbContext dbContext)
    {
        _dbContext = dbContext;
        _userRepository = new UserRepository(_dbContext);
    }

    private readonly CursiveDbContext _dbContext;
    private readonly IUserRepository _userRepository;

    public IUserRepository UserRepository => _userRepository ?? new UserRepository(_dbContext);
}
