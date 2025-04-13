using Cursive.Communication.Dtos.Responses;

namespace Cursive.Web.MVC.Services.Interfaces;

public interface IAuthenticationService
{
    void SetAuthSession(HttpContext httpContext, TokenResponse tokenResponse);
    void RemoveAuthSession(HttpContext httpContext);
}
