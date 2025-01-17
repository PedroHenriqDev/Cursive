using System.Security.Claims;
using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Dtos.User.Requests;
using Cursive.Communication.Dtos.User.Responses;
using Cursive.Communication.Factories;
using Cursive.Application.Mappers;
using Cursive.Application.Resources;
using Cursive.Application.Services.Interfaces;
using Cursive.Domain.Entities;
using Cursive.Domain.Validations;
using Cursive.Infra.UnitOfWork.Interfaces;

namespace Cursive.Application.Services;

public class UserService : IUserService
{
    public UserService(IUnitOfWork unitOfWork, ICryptoService cryptoService, ITokenService tokenService, IClaimService claimService)
    {
        _unitOfWork = unitOfWork;
        _cryptoService = cryptoService;
        _tokenService = tokenService;
        _claimService = claimService;
    }

    private readonly IUnitOfWork _unitOfWork;
    private readonly ICryptoService _cryptoService;
    private readonly ITokenService _tokenService;
    private readonly IClaimService _claimService;

    public async Task<IResponseDto<UserResponse>> CreateAsync(UserRequest request)
    {
        User user = request.ToUser();
        Validation validaton = user.Validate();

        if (!validaton.IsValid)
            return ResponseFactory.BadRequest(validaton.Messages.Select(c => c.Message).ToList(), user.ToUserResponse());

        if (await _unitOfWork.UserRepository.ExistsAsync(u => u.Name.FirstName == user.Name.FirstName && u.Name.LastName == user.Name.LastName))
            return ResponseFactory.BadRequest(string.Format(Messages.EXISTS, nameof(user.Name)), user.ToUserResponse());

        if (await _unitOfWork.UserRepository.ExistsAsync(u => u.Email == user.Email))
            return ResponseFactory.BadRequest(string.Format(Messages.EXISTS, nameof(user.Email)), user.ToUserResponse());

        _cryptoService.EncryptPassword(user);

        await _unitOfWork.UserRepository.CreateAsync(user);
        await _unitOfWork.SaveAsync();

        return ResponseFactory.Created(Messages.CREATED_SUCESSFULLY, user.ToUserResponse());
    }


    public async Task<IResponseDto<TokenResponse>> LoginAsync(LoginRequest request)
    {
        if (request == null)
            return ResponseFactory.BadRequest(Messages.INVALID_LOGIN, new TokenResponse());

        User? user = await _unitOfWork.UserRepository.GetAsync(u => u.Email == request.Email);

        if (user == null)
            return ResponseFactory.BadRequest(Messages.INVALID_LOGIN, new TokenResponse());

        if (!_cryptoService.ComparePassword(request.Password, user!.Password))
            return ResponseFactory.BadRequest(Messages.INVALID_LOGIN, new TokenResponse());

        IList<Claim> authClaims = _claimService.CreateAuthClaims(user.Email, user.Id.ToString());
        return ResponseFactory.Ok(Messages.LOGIN_SUCESSFULLY, _tokenService.GenerateToken(authClaims));
    }
}
