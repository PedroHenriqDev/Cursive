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
using Cursive.Infra.UnitOfWork.Interfaces;

namespace Cursive.Application.Services;

public class DocumentService : IDocumentService
{
    private readonly IUnitOfWork _unitOfWork;

    public DocumentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
}
