using Cursive.Domain.Entities;
using Cursive.Domain.Repositories.Interfaces;
using Cursive.Infra.Data;

namespace Cursive.Infra.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(CursiveDbContext context) : base(context)
    {
    }
}
