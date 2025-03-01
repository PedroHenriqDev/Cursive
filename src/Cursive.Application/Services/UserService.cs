using System.Security.Claims;
using Cursive.Communication.Dtos.Interfaces;
using Cursive.Communication.Factories;
using Cursive.Application.Mappers;
using Cursive.Application.Resources;
using Cursive.Application.Services.Interfaces;
using Cursive.Domain.Entities;
using Cursive.Domain.Validations;
using Cursive.Infra.UnitOfWork.Interfaces;
using Cursive.Communication.Dtos.Requests;
using Cursive.Communication.Dtos.Responses;

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
        var errorMessages = new List<string>();

        if (!validaton.IsValid)
            errorMessages.AddRange(validaton.Messages.Select(c => c.Message).ToList());

        if (await _unitOfWork.UserRepository.ExistsAsync(u => u.Name.FirstName == user.Name.FirstName && u.Name.LastName == user.Name.LastName))
            errorMessages.Add(string.Format(Messages.EXISTS, nameof(user.Name)));

        if (await _unitOfWork.UserRepository.ExistsAsync(u => u.Email == user.Email))
            errorMessages.Add(string.Format(Messages.EXISTS, nameof(user.Email)));

        if (errorMessages.Any())
            return ResponseFactory.BadRequest(errorMessages, user.ToUserResponse());

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

    public async Task<IResponseDto<UserResponse>> UpdateAsync(Guid userId, UserRequest request)
    {
        if (await _unitOfWork.UserRepository.GetByIdAsync(userId) is User user)
        {
            request.LoadUser(user);
            Validation validation = user.Validate();

            if (!validation.IsValid)
                return ResponseFactory.BadRequest(validation.Messages.Select(v => v.Message).ToList(), user.ToUserResponse());

            _cryptoService.EncryptPassword(user);

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();
            
            return ResponseFactory.Ok(Messages.SUCCESSFUL, user.ToUserResponse());
        }

        return ResponseFactory.NotFound(string.Format(Messages.NOT_FOUND_USER, userId), new UserResponse());
    }
}
