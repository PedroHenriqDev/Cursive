using Cursive.Application.Services.Interfaces;
using Cursive.Application.Utils;
using Cursive.Communication.Dtos.Requests;
using Cursive.Communication.Dtos.Responses;
using Cursive.Domain.Entities;
using Cursive.Domain.Enums;

namespace Cursive.Application.Mappers;

public static class DocumentMapper
{
    public static Document ToDocument(this DocumentRequest request)
    {
        EDocumentType documentType = EDocumentType.Text;
        if (int.TryParse(request.Type, out int documentTypeAsInt))
            documentType = documentTypeAsInt.CastEnumByNumber<EDocumentType>();
        else
            documentType = EnumUtils.GetEnumByName<EDocumentType>(request.Type);

        return new Document(request.Title, request.Text, documentType, request.UserId);
    }

    public static void MapToDocument(this DocumentRequest request, Document document)
    {
        document.Title = request.Title;
        document.Text = request.Text;
        document.Type = Enum.Parse<EDocumentType>(request.Type);
    }

    public static DocumentResponse ToResponse(this Document response)
    {
        return new DocumentResponse()
        {
            Id = response.Id,
            UserId = response.UserId,
            CreatedAt = response.CreatedAt,
            Text = response.Text,
            Title = response.Title,
            Type = response.Type.ToString(),
        };
    }
}
