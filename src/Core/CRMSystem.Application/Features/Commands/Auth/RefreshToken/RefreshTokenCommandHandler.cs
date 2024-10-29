using CRMSystem.Application.Common.Exceptions;
using CRMSystem.Application.Features.Commands.Auth.ViewModels;
using CRMSystem.Application.Interfaces;
using CRMSystem.External.Helpers;
using MediatR;

namespace CRMSystem.Application.Features.Commands.Auth.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, JwtTokenDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserManager _userManager;

    public RefreshTokenCommandHandler(IUserRepository userRepository, IUserManager userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public async Task<JwtTokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(x => x.RefreshToken == request.RefreshToken);

        if (user == null || user.RefreshTokenExpireDate <= DateTime.UtcNow)
        {
            throw new UnAuthorizedException("Invalid or expired refresh token.");
        }

        (var newRefreshToken, var newRefreshExpireAt) = RefreshTokenHelper.GenerateRefreshToken(user.Id);

        user.SetRefreshToken(newRefreshToken, newRefreshExpireAt);

        await _userRepository.SaveAsync();

        var (token, expireAt) = _userManager.GenerateTJwtToken(user);

        return new JwtTokenDto
        {
            Token = token,
            TokenExpireAt = expireAt,
            RefreshToken = newRefreshToken,
            RefreshExpireAt = newRefreshExpireAt
        };
    }
}