using Cursive.Application.Services.Interfaces;
using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.Requests;
using Cursive.Communication.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Cursive.API.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class DocumentController : Controller
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService service)
    {
        _documentService = service;
    }

    [HttpPost]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IResponseDto<DocumentResponse>>> CreateAsync([FromBody] DocumentRequest request)
    {
        IResponseDto<DocumentResponse> response = await _documentService.CreateAsync(request);

        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [Route("search")]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IResponseDto<DocumentResponse>>> SearchAsync([FromQuery] FilterDocumentRequest filter)
    {
        IResponseDto<IEnumerable<DocumentResponse>> response = await _documentService.SearchAsync(filter);

        return StatusCode((int)response.StatusCode, response.Data);
    }
}
