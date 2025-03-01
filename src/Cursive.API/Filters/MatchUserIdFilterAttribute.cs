using System.Security.Claims;
using System.Web.Http.Controllers;
using Cursive.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cursive.API.Filters;

public class MatchUserIdFilterAttribute : Attribute, IActionFilter
{
    private readonly ITokenService _tokenService;
    private readonly IClaimService _claimService;

    public MatchUserIdFilterAttribute(ITokenService tokenService, IClaimService claimService)
    {
        _tokenService = tokenService;
        _claimService = claimService;
    }

    public bool AllowMultiple => true;

    public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
    {

        return await continuation();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        string token = _tokenService.GetTokenByHttpRequest(context.HttpContext);

        string tokenUserId = _claimService.GetUserId(_tokenService.GetAuthClaims(token));

        string? userId = context.HttpContext.Request.RouteValues?.FirstOrDefault(r => r.Key == "id").Value?.ToString();

        if (userId?.ToLower() != tokenUserId.ToLower())
            context.Result = new UnauthorizedResult();
    }
}
