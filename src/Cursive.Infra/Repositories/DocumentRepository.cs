using Cursive.Domain.Entities;
using Cursive.Domain.Repositories.Interfaces;
using Cursive.Infra.Data;

namespace Cursive.Infra.Repositories;

public class DocumentRepository : Repository<Document>, IDocumentRepository
{
    public DocumentRepository(CursiveDbContext dbContext) : base(dbContext)
    {
    }
}
