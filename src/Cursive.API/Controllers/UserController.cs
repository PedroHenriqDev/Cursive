using Cursive.Communication.Dtos.Interfaces;
using Cursive.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Cursive.Communication.Dtos.Requests;
using Cursive.Communication.Dtos.Responses;
using Cursive.API.Filters;
using Cursive.Communication.Dtos;

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

    [HttpPut]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(IResponseDto<UserResponse>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponseDto<UserResponse>), statusCode: StatusCodes.Status400BadRequest)]
    [TypeFilter(typeof(MatchUserIdFilterAttribute))]
    public async Task<ActionResult<IResponseDto<UserResponse>>> UpdateAsync([FromRoute] Guid id, [FromBody] UserRequest userRequest)
    {
        IResponseDto<UserResponse> response = await _userService.UpdateAsync(id, userRequest);
        return StatusCode((int)response.StatusCode, response);
    }
}
