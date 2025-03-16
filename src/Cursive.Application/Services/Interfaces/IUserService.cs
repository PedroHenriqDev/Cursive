using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.Requests;
using Cursive.Communication.Dtos.Responses;

namespace Cursive.Application.Services.Interfaces;

public interface IUserService
{
    Task<IResponseDto<UserResponse>> CreateAsync(UserRequest request);
    Task<IResponseDto<TokenResponse>> LoginAsync(LoginRequest request);
    Task<IResponseDto<UserResponse>> UpdateAsync(Guid userId, UserRequest request);
    Task<IResponseDto<UserResponse>> GetByIdAsync(Guid userId);
}
