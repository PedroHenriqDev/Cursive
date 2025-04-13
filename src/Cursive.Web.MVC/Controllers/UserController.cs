using System.Net;
using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.Requests;
using Cursive.Communication.Dtos.Responses;
using Cursive.Web.HttpCore.Interfaces;
using Cursive.Web.MVC.Extensions;
using Cursive.Web.MVC.Models;
using Cursive.Web.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cursive.Web.MVC.Controllers;

public class UserController : Controller
{
    private readonly IUserClient _userClient;
    private readonly IAuthenticationService _authService;
    private readonly IReCaptchaClient _reCaptchaClient;

    public UserController(IUserClient userClient, IAuthenticationService authService, IReCaptchaClient reCaptchaClient)
    {
        _userClient = userClient;
        _authService = authService;
        _reCaptchaClient = reCaptchaClient;
    }

    [HttpGet]
    public IActionResult Login() => View(); 

    [HttpGet] 
    public IActionResult Register() => View();

    [HttpGet]
    public IActionResult Logout()
    {
        _authService.RemoveAuthSession(HttpContext);

        return View(nameof(Login));
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserRequest userRequest)
    {
        IResponseDto<UserResponse>? userResponse = await _userClient.RequestToCreateAsync(userRequest);

        if(userResponse == null)
            return StatusCode(StatusCodes.Status500InternalServerError);

        return StatusCode((int)userResponse.StatusCode, ((int)userResponse.StatusCode).IsSuccessStatusCode() ? userResponse.Data : userResponse.Messages);
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        IResponseDto<ReCaptchaResponse?>? reCaptchaResponse = await _reCaptchaClient.ValidateReCaptchaAsync(loginRequest.ReCaptchaToken);

        if (reCaptchaResponse == null || reCaptchaResponse.Data == null) 
            return StatusCode(StatusCodes.Status500InternalServerError);

        if (!reCaptchaResponse!.Data!.Success)
            return StatusCode(StatusCodes.Status400BadRequest, new LoginViewModel { IsReCaptchaError = true });

        IResponseDto<TokenResponse>? response = await _userClient.RequestToLoginAsync(loginRequest);

        if (response == null)
            return BadRequest();

        if(response?.Data != null && response?.StatusCode == HttpStatusCode.OK)
            _authService.SetAuthSession(HttpContext, response.Data);

        return StatusCode((int)response!.StatusCode, new LoginViewModel { IsReCaptchaError = false});
    }
}
