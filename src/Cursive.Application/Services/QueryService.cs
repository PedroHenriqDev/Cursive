using Cursive.Application.Query;
using Cursive.Application.Services.Interfaces;
using Cursive.Communication.Dtos.Abstractions;
using Cursive.Communication.Dtos.Requests;
using Cursive.Domain.Entities;
using Cursive.Domain.Entities.Abstractions;
using Cursive.Infra.UnitOfWork.Interfaces;

namespace Cursive.Application.Services;

public class QueryService : IQueryService
{
    private readonly IUnitOfWork _unitOfWork;

    public QueryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public QueryContext MountSearchQuery<TEntity>(FilterBase filter, ref IQueryable<TEntity> query) where TEntity : Entity
    { 
        var queryContext = new QueryContext();

        queryContext.HaveConditionById = filter.Id != Guid.Empty;

        if (queryContext.HaveConditionById)
            query = query.Where(e => e.Id == filter.Id);

        if (filter.StartDateToCreatedAt != DateTime.MinValue)
            query = query.Where(e => e.CreatedAt > filter.StartDateToCreatedAt);
        
        if(filter.EndDateToCreatedAt != DateTime.MinValue)
            query = query.Where(e => e.CreatedAt < filter.EndDateToCreatedAt);

        return queryContext;
    }

    public IQueryable<Document> MountDocumentSearchQuery(FilterDocumentRequest filter)
    {
        IQueryable<Document> query = _unitOfWork.DocumentRepository.AllNotTracking;

        QueryContext queryContext = MountSearchQuery(filter, ref query);

        if(!string.IsNullOrEmpty(filter.Title) && !queryContext.HaveConditionById)
            query = query.Where(d => d.Title == filter.Title);

        if (!string.IsNullOrEmpty(filter.Text) && !queryContext.HaveConditionById)
            query = query.Where(d => d.Text == filter.Text);

        if (!string.IsNullOrEmpty(filter.Type) && !queryContext.HaveConditionById)
            query = query.Where(d => (int)d.Type == Convert.ToInt32(filter.Type));

        if(filter.UserId != Guid.Empty && !queryContext.HaveConditionById)
            query = query.Where(d => d.UserId == filter.UserId);

        return query;
    }
}
