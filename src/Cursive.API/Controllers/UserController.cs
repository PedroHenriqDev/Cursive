using Cursive.Application.Dtos.Interfaces;
using Cursive.Application.Dtos.User.Requests;
using Cursive.Application.Dtos.User.Responses;
using Cursive.Application.Services.Interfaces;
using Cursive.Logging.Loggers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cursive.API.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IFileLogger<UserController> _fileLogger;

    public UserController(IUserService userService, IFileLogger<UserController> fileLogger)
    {
        _fileLogger = fileLogger;
        _fileLogger.LogInfoAsync("User controller initializing.", method: "Contrutor").GetAwaiter().GetResult();
        _userService = userService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(IResponseDto<UserResponse>), statusCode: (int)StatusCodes.Status201Created)]
    public async Task<ActionResult<IResponseDto<UserResponse>>> CreateAsync([FromBody] UserRequest request)
    {
        await _fileLogger.LogInfoAsync("Create Async endpoint test.", nameof(CreateAsync));
        IResponseDto<UserResponse> response = await _userService.CreateAsync(request);
        return StatusCode((int)response.StatusCode, response);
    }
}
