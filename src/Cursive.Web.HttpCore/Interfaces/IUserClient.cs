using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.User.Requests;
using Cursive.Communication.Dtos.User.Responses;

namespace Cursive.Web.HttpCore.Interfaces;

public interface IUserClient
{
    Task<IResponseDto<UserResponse>?> RequestToCreateAsync(UserRequest userRequest);
    Task<IResponseDto<TokenResponse>?> RequestToLoginAsync(LoginRequest loginRequest);
}
