using Cursive.Application.Services.Interfaces;
using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.Requests;
using Cursive.Communication.Dtos.Responses;
using Cursive.Web.HttpCore.Interfaces;
using Cursive.Web.MVC.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cursive.Web.MVC.Controllers;

[Authorize]
public class DocumentController : Controller
{
    private readonly IDocumentClient _documentClient;
    private readonly ITokenService _tokenService;

    public DocumentController(IDocumentClient documentClient, ITokenService tokenService)
    {
        _documentClient = documentClient;
        _tokenService = tokenService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] DocumentRequest request)
    {
        request.UserId = new Guid(User.GetUserId(_tokenService));

        IResponseDto<DocumentResponse>? response = await _documentClient.CreateAsync(request, User.GetApiToken());

        return StatusCode((int)response!.StatusCode, response.Data);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] DocumentRequest request)
    {
        request.UserId = new Guid(User.GetUserId(_tokenService));

        IResponseDto<DocumentResponse>? response = await _documentClient.UpdateAsync(request, User.GetApiToken());

        return StatusCode((int)response!.StatusCode, response.Data);
    }

    [HttpPatch]

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery]Guid documentId)
    {
        string apiToken = User.GetApiToken();
        IResponseDto<DocumentResponse>? response = await _documentClient.GetByIdAsync(documentId, apiToken);

        return StatusCode((int)response!.StatusCode, response.Data);
    }
}
