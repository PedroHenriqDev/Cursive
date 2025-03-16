using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.Responses;

namespace Cursive.Web.HttpCore.Interfaces;

public interface IDocumentClient
{
    Task<IResponseDto<IEnumerable<DocumentResponse>>?> GetByUserIdAsync(Guid userId);
}
