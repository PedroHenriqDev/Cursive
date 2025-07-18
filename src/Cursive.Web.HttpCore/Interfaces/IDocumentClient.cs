﻿using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.Requests;
using Cursive.Communication.Dtos.Responses;

namespace Cursive.Web.HttpCore.Interfaces;

public interface IDocumentClient
{
    Task<IResponseDto<IEnumerable<DocumentResponse>>?> GetByUserIdAsync(Guid userId, string apiToken);
    Task<IResponseDto<DocumentResponse>?> UpdateAsync(DocumentRequest documentRequest, string apiToken);
    Task<IResponseDto<DocumentResponse>?> CreateAsync(DocumentRequest request, string apiToken);
    Task<IResponseDto<DocumentResponse>?> GetByIdAsync(Guid documentId, string apiToken);
}
