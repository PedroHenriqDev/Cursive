using Cursive.Communication.Dtos.Responses;
using Cursive.Domain.Entities;

namespace Cursive.Web.MVC.Services.Interfaces;

public interface IAuthenticationService
{
    Task SignInAsync(HttpContext httpContext, TokenResponse tokenResponse);
    void RemoveAuthSession(HttpContext httpContext);
}
