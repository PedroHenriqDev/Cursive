using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.Requests;
using Cursive.Communication.Dtos.Responses;

namespace Cursive.Web.HttpCore.Interfaces;

public interface IUserClient
{
    Task<IResponseDto<UserResponse>?> RequestToCreateAsync(UserRequest userRequest);
    Task<IResponseDto<TokenResponse>?> RequestToLoginAsync(LoginRequest loginRequest);
}
