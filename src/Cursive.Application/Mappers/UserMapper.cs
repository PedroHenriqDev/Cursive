using Cursive.Communication.Dtos.Requests;
using Cursive.Communication.Dtos.Responses;
using Cursive.Domain.Entities;
using Cursive.Domain.ValueObjects;

namespace Cursive.Application.Mappers;

public static class UserMapper
{
    public static UserResponse ToUserResponse(this User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            BirthDate = user.BirthDate,
            CreatedAt = user.CreatedAt,
            Email = user.Email,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
        };
    }

    public static User ToUser(this UserRequest request) 
    {
        return new User(new Name(request.FirstName, request.LastName), request.Email, request.Password, request.BirthDate);
    }
}
