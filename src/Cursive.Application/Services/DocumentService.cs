using System.Reflection.Metadata.Ecma335;
using Cursive.Application.Mappers;
using Cursive.Application.Resources;
using Cursive.Application.Services.Interfaces;
using Cursive.Communication.Dtos;
using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.Requests;
using Cursive.Communication.Dtos.Responses;
using Cursive.Communication.Factories;
using Cursive.Domain.Entities;
using Cursive.Domain.Repositories.Interfaces;
using Cursive.Domain.Validations;
using Cursive.Infra.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cursive.Application.Services;

public class DocumentService : IDocumentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IQueryService _queryService;

    public DocumentService(IUnitOfWork unitOfWork, IQueryService queryService)
    {
        _unitOfWork = unitOfWork;
        _queryService = queryService;
    }

    public async Task<IResponseDto<DocumentResponse>> CreateAsync(DocumentRequest request)
    {
        Document document = request.ToDocument();

        Validation validation = document.Validate();

        if (!validation.IsValid)
            return ResponseFactory.BadRequest(validation.Messages.Select(m => m.Message).ToList(), new DocumentResponse());

        if(!await _unitOfWork.UserRepository.ExistsAsync(u => u.Id == document.UserId))
            return ResponseFactory.BadRequest(string.Format(Messages.NOT_FOUND_USER, document.UserId), new DocumentResponse());

        await _unitOfWork.DocumentRepository.CreateAsync(document);
        await _unitOfWork.SaveAsync();

        return ResponseFactory.Created(Messages.CREATED_SUCESSFULLY, document.ToResponse());
    }

    public async Task<IResponseDto<IEnumerable<DocumentResponse>>> SearchAsync(FilterDocumentRequest filter)
    {
        IEnumerable<Document> _documents = await _queryService.MountDocumentSearchQuery(filter).ToListAsync();

        if (!_documents.Any())
            return ResponseFactory.NotFound(Messages.NOT_FOUND_DOCUMENTS, Enumerable.Empty<DocumentResponse>());

        return ResponseFactory.Ok(Messages.SUCCESSFUL, _documents.Select(d => d.ToResponse()));
    }
}
