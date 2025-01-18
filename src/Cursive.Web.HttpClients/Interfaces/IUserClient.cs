using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.User.Requests;
using Cursive.Communication.Dtos.User.Responses;

namespace Cursive.Web.HttpClients.Interfaces;

public interface IUserClient
{
    Task<IResponseDto<UserResponse>?> CreateAsync(UserRequest userRequest);
    Task<IResponseDto<TokenResponse>?> LoginAsync(LoginRequest loginRequest);
}
