using Cursive.API.Filters;
using Cursive.Application.Services.Interfaces;
using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.Requests;
using Cursive.Communication.Dtos.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cursive.API.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class DocumentController : Controller
{
    private readonly IDocumentService _documentService;
    private readonly ITokenService _tokenService;
    private readonly IClaimService _claimService;

    public DocumentController(IDocumentService service, ITokenService tokenService, IClaimService claimService)
    {
        _documentService = service;
        _tokenService = tokenService;
        _claimService = claimService;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IResponseDto<DocumentResponse>>> CreateAsync([FromBody] DocumentRequest request)
    {
        string token = _tokenService.GetTokenByHttpRequest(HttpContext);
        string userId = _claimService.GetUserId(_tokenService.GetAuthClaims(token));

        if(userId.ToLower() != request.UserId.ToString().ToLower())
            return BadRequest();

        IResponseDto<DocumentResponse> response = await _documentService.CreateAsync(request);

        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize]
    [TypeFilter(typeof(MatchDocumentUserFilterAttribute))]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IResponseDto<DocumentResponse>>> UpdateAsync([FromRoute] Guid id, [FromBody] DocumentPutRequest request)
    {
        IResponseDto<DocumentResponse> response = await _documentService.UpdateAsync(id, request);
        
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize]
    [TypeFilter(typeof(MatchDocumentUserFilterAttribute))]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IResponseDto<DocumentResponse>>> DeleteAsync([FromRoute] Guid id)
    {
        IResponseDto<DocumentResponse> response = await _documentService.DeleteAsync(id);
        
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize]
    [TypeFilter(typeof(MatchDocumentUserFilterAttribute))]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IResponseDto<DocumentResponse>>> GetById([FromRoute] Guid id)
    {
        IResponseDto<DocumentResponse> response = await _documentService.GetByIdAsync(id);

        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [Route("search")]
    [Authorize]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponseDto<DocumentResponse>), statusCode: StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IResponseDto<DocumentResponse>>> SearchAsync([FromQuery] FilterDocumentRequest filter)
    {
        IResponseDto<IEnumerable<DocumentResponse>> response = await _documentService.SearchAsync(filter);

        return StatusCode((int)response.StatusCode, response.Data);
    }
}
