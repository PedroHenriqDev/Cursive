using Cursive.API.Dtos;
using Cursive.Application.Dtos.User.Requests;
using Cursive.Application.Dtos.User.Responses;
using Cursive.Application.Factories;
using Cursive.Application.Mappers;
using Cursive.Application.Resources;
using Cursive.Application.Services.Interfaces;
using Cursive.Domain.Entities;
using Cursive.Domain.Validations;
using Cursive.Infra.UnitOfWork.Interfaces;

namespace Cursive.Application.Services;

public class UserService : IUserService
{
    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private readonly IUnitOfWork _unitOfWork;

    public async Task<ResponseDto<UserResponse>> CreateAsync(UserRequest request)
    {
        User user = request.ToUser();
        Validation validaton = user.Validate();

        if (!validaton.IsValid)
            return ResponseFactory.BadRequest(validaton.Messages.Select(c => c.Message).ToList(), user.ToUserResponse());

        if (await _unitOfWork.UserRepository.ExistsAsync(u => u.Name.FirstName == user.Name.FirstName && u.Name.LastName == user.Name.LastName))
            return ResponseFactory.BadRequest([string.Format(Messages.EXISTS, nameof(user.Name))], user.ToUserResponse());

        if (await _unitOfWork.UserRepository.ExistsAsync(u => u.Email == user.Email))
            return ResponseFactory.BadRequest([string.Format(Messages.EXISTS, nameof(user.Email))], user.ToUserResponse());

        await _unitOfWork.UserRepository.CreateAsync(user);
        await _unitOfWork.SaveAsync();

        return ResponseFactory.Created(new List<string>{ Messages.CREATED_SUCESSFULLY }, user.ToUserResponse());
    }
}
