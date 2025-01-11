using Cursive.Application.Dtos.Interfaces;
using Cursive.Application.Dtos.User.Requests;
using Cursive.Application.Dtos.User.Responses;
using Cursive.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cursive.API.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(IResponseDto<UserResponse>), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(IResponseDto<UserResponse>), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IResponseDto<UserResponse>>> CreateAsync([FromBody] UserRequest request)
    {
        IResponseDto<UserResponse> response = await _userService.CreateAsync(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(IResponseDto<string>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponseDto<string>), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IResponseDto<TokenResponse>>> LoginAsync([FromBody] LoginRequest request)
    {
        IResponseDto<TokenResponse> response = await _userService.LoginAsync(request);
        return StatusCode((int)response.StatusCode, response);
    }
}
