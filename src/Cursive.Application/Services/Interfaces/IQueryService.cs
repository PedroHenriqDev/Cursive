using Cursive.Application.Query;
using Cursive.Communication.Dtos.Abstractions;
using Cursive.Communication.Dtos.Requests;
using Cursive.Domain.Entities;
using Cursive.Domain.Entities.Abstractions;

namespace Cursive.Application.Services.Interfaces;

public interface IQueryService
{
    IQueryable<Document> MountDocumentSearchQuery(FilterDocumentRequest filter);
    QueryContext MountSearchQuery<TEntity>(FilterBase filterBase, ref IQueryable<TEntity> query) where TEntity : Entity;
}
