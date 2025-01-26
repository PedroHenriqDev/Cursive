using Cursive.Communication.Dtos.User.Responses;

namespace Cursive.Web.MVC.Services.Interfaces;

public interface IAuthenticationService
{
    void SetAuthSession(HttpContext httpContext, TokenResponse tokenResponse);
}
