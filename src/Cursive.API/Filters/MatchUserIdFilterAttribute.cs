using System.Net;
using System.Security.Claims;
using System.Web.Http.Controllers;
using Cursive.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cursive.API.Filters;

public class MatchUserIdFilterAttribute : Attribute, IActionFilter
{
    private readonly ITokenService _tokenService;

    public MatchUserIdFilterAttribute(ITokenService tokenService)
    {
        _tokenService = tokenService;
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
        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        string token = authHeader.ToString().Split(" ").Last();
        IList<Claim> claims = _tokenService.GetAuthClaims(token);
        Claim? idClaim = claims.FirstOrDefault(c => c.Type == "ID");

        if (idClaim == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        string? userId = context.HttpContext.Request.RouteValues?.FirstOrDefault(r => r.Key == "id").Value?.ToString();

        if (userId?.ToLower() != idClaim.Value.ToLower())
            context.Result = new UnauthorizedResult();
    }
}
