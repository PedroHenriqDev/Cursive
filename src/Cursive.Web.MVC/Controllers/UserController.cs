using System.Net;
using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.User.Requests;
using Cursive.Communication.Dtos.User.Responses;
using Cursive.Web.HttpCore.Interfaces;
using Cursive.Web.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cursive.Web.MVC.Controllers;

public class UserController : Controller
{
    private readonly IUserClient _userClient;
    private readonly IAuthenticationService _authService;

    public UserController(IUserClient userClient, IAuthenticationService authService)
    {
        _userClient = userClient;
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        IResponseDto<TokenResponse>? response = await _userClient.LoginAsync(loginRequest);

        if (response == null)
            return BadRequest();

        if(response?.Data != null && response?.StatusCode == HttpStatusCode.OK)
            _authService.SetAuthSession(HttpContext, response.Data);

        return StatusCode((int)response!.StatusCode, response.Data);
    }
}
