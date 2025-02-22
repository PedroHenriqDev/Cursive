using System.Reflection.Metadata.Ecma335;
using Cursive.Application.Mappers;
using Cursive.Application.Resources;
using Cursive.Application.Services.Interfaces;
using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.Requests;
using Cursive.Communication.Dtos.Responses;
using Cursive.Communication.Factories;
using Cursive.Domain.Entities;
using Cursive.Domain.Repositories.Interfaces;
using Cursive.Domain.Validations;

namespace Cursive.Application.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentService(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<IResponseDto<DocumentResponse>> CreateAsync(DocumentRequest request)
    {
        Document document = request.ToDocument();

        Validation validation = document.Validate();

        if (!validation.IsValid)
            return ResponseFactory.BadRequest(validation.Messages.Select(m => m.Message).ToList(), new DocumentResponse());

        await _documentRepository.CreateAsync(document);

        return ResponseFactory.Created(Messages.CREATED_SUCESSFULLY, document.ToResponse());
    }
}
