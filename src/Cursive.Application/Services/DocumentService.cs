using Azure;
using Cursive.Application.Mappers;
using Cursive.Application.Resources;
using Cursive.Application.Services.Interfaces;
using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.Requests;
using Cursive.Communication.Dtos.Responses;
using Cursive.Communication.Factories;
using Cursive.Domain.Entities;
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

    public async Task<IResponseDto<DocumentResponse>> DeleteAsync(Guid documentId)
    {
        if(await _unitOfWork.DocumentRepository.GetByIdAsync(documentId) is Document document) 
        {
            _unitOfWork.DocumentRepository.Delete(document);
            await _unitOfWork.SaveAsync();

            return ResponseFactory.Ok(Messages.SUCCESSFUL, document.ToResponse());
        }

        return ResponseFactory.NotFound(string.Format(Messages.NOT_FOUND_DOCUMENT, documentId), new DocumentResponse());
    }

    public async Task<IResponseDto<DocumentResponse>> GetByIdAsync(Guid documentId)
    {
        if(await _unitOfWork.DocumentRepository.GetByIdAsync(documentId) is Document document)
        {
            return ResponseFactory.Ok(Messages.SUCCESSFUL, document.ToResponse());
        }

        return ResponseFactory.NotFound(string.Format(Messages.NOT_FOUND_DOCUMENT, documentId), new DocumentResponse());
    }

    public async Task<IResponseDto<IEnumerable<DocumentResponse>>> SearchAsync(FilterDocumentRequest filter)
    {
        IEnumerable<Document> documents = await _queryService.MountDocumentSearchQuery(filter).ToListAsync();

        if (!documents.Any())
            return ResponseFactory.NotFound(Messages.NOT_FOUND_DOCUMENTS, Enumerable.Empty<DocumentResponse>());

        return ResponseFactory.Ok(Messages.SUCCESSFUL, documents.Select(d => d.ToResponse()));
    }

    public async Task<IResponseDto<DocumentResponse>> UpdateAsync(Guid documentId, DocumentPutRequest request)
    {
        if(await _unitOfWork.DocumentRepository.GetByIdAsync(documentId) is Document document)
        {
            request.MapToDocument(document);

            Validation validation = document.Validate();
            if (!validation.IsValid)
            {
                return ResponseFactory.BadRequest(validation.Messages.Select(m => m.Message).ToList(), document.ToResponse());
            }

            _unitOfWork.DocumentRepository.Update(document);
            await _unitOfWork.SaveAsync();

            return ResponseFactory.Ok(Messages.SUCCESSFUL, document.ToResponse());
        }

        return ResponseFactory.NotFound(string.Format(Messages.NOT_FOUND_DOCUMENT, documentId), new DocumentResponse());
    }
}
