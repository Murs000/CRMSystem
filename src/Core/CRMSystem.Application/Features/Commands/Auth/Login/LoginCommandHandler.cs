using System.Security.Cryptography;
using CRMSystem.Application.Common.Exceptions;
using CRMSystem.Application.Features.Commands.Auth.ViewModels;
using CRMSystem.Application.Interfaces;
using CRMSystem.External.Helpers;
using MediatR;

namespace CRMSystem.Application.Features.Commands.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, JwtTokenDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserManager _userManager;
    public LoginCommandHandler(IUserRepository userRepository, IUserManager userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public async Task<JwtTokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(x => x.Username.ToLower() == request.Username.ToLower());

        if (user == null
            || !PasswordHelper.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt)
            || !user.IsActivated)
        {
            throw new UnAuthorizedException("Invalid credentials");
        }

        (var refreshToken, var refreshExpireAt) = RefreshTokenHelper.GenerateRefreshToken(user.Id);

        user.SetRefreshToken(refreshToken, refreshExpireAt);

        (string token, DateTime expireAt) = _userManager.GenerateTJwtToken(user);
        
        await _userRepository.SaveAsync();

        return new JwtTokenDto
        {
            Token = token,
            TokenExpireAt = expireAt,
            RefreshToken = refreshToken,
            RefreshExpireAt = DateTime.UtcNow.AddHours(4).AddDays(20)
        };
    }
}