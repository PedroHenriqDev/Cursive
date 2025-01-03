﻿using Cursive.Application.Dtos.Interfaces;
using Cursive.Application.Dtos.User.Requests;
using Cursive.Application.Dtos.User.Responses;
using Cursive.Application.Services;
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
    [ProducesResponseType(typeof(IResponseDto<UserResponse>), statusCode: (int)StatusCodes.Status201Created)]
    public async Task<ActionResult<IResponseDto<UserResponse>>> CreateAsync([FromBody] UserRequest request)
    {
        IResponseDto<UserResponse> response = await _userService.CreateAsync(request);
        return StatusCode((int)response.StatusCode, response);
    }
}
