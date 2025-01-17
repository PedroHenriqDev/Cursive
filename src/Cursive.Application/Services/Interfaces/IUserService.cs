using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.User.Requests;
using Cursive.Communication.Dtos.User.Responses;

namespace Cursive.Application.Services.Interfaces;

public interface IUserService
{
    Task<IResponseDto<UserResponse>> CreateAsync(UserRequest request);
    Task<IResponseDto<TokenResponse>> LoginAsync(LoginRequest request);
}
