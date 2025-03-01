using System.Security.Claims;
using Cursive.Application.Services.Interfaces;
using Cursive.Domain.Entities;
using Cursive.Infra.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cursive.API.Filters;

public class MatchDocumentUserFilterAttribute : Attribute, IAsyncActionFilter
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IClaimService _claimService;

    public MatchDocumentUserFilterAttribute(IUnitOfWork unitOfWork, ITokenService tokenService, IClaimService claimService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _claimService = claimService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string token = _tokenService.GetTokenByHttpRequest(context.HttpContext);

        IList<Claim> authClaims = _tokenService.GetAuthClaims(token);

        string tokenUserId = _claimService.GetUserId(authClaims);

        string? documentId = context.HttpContext.Request.RouteValues?.FirstOrDefault(r => r.Key == "id").Value?.ToString();

        if (string.IsNullOrEmpty(documentId))
        {
            context.Result = new BadRequestResult();
            return;
        }

        Document? document = await _unitOfWork.DocumentRepository.GetByIdAsync(new Guid(documentId));

        if(document?.UserId.ToString().ToLower() != tokenUserId.ToLower())
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        await next();
    }
}
