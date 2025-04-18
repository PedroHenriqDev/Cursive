﻿using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.Requests;
using Cursive.Communication.Dtos.Responses;

namespace Cursive.Application.Services.Interfaces;

public interface IDocumentService
{
    Task<IResponseDto<DocumentResponse>> CreateAsync(DocumentRequest request);
    Task<IResponseDto<DocumentResponse>> GetByIdAsync(Guid id);
    Task<IResponseDto<IEnumerable<DocumentResponse>>> SearchAsync(FilterDocumentRequest filter);
    Task<IResponseDto<DocumentResponse>> UpdateAsync(Guid documentId, DocumentPutRequest request);
    Task<IResponseDto<DocumentResponse>> DeleteAsync(Guid documentId);
}
