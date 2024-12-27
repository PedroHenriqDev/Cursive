using Cursive.API.Dtos;
using Cursive.Application.Dtos.User.Requests;
using Cursive.Application.Dtos.User.Responses;

namespace Cursive.Application.Services.Interfaces;

public interface IUserService
{
    Task<ResponseDto<UserResponse>> CreateAsync(UserRequest request);
}
