using Cursive.API.Dtos;
using Cursive.Application.Dtos.Interfaces;
using Cursive.Application.Dtos.User.Requests;
using Cursive.Application.Dtos.User.Responses;

namespace Cursive.Application.Services.Interfaces;

public interface IUserService
{
    Task<IResponseDto<UserResponse>> CreateAsync(UserRequest request);
    Task<IResponseDto<TokenResponse>> LoginAsync(LoginRequest request);
}
