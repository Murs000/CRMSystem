using CRMSystem.Application.Common.Exceptions;
using CRMSystem.Application.Interfaces;
using MediatR;

namespace CRMSystem.Application.Features.Commands.Auth.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand,bool>
{
    private readonly IUserRepository _userRepository;

    public ConfirmEmailCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(x => x.Email == request.Email && x.OTP == request.OTP && x.OTPExpireDate > DateTime.UtcNow.AddHours(4));

        if (user == null || user.IsActivated)
        {
            throw new NotFoundException("Invalid OTP or user already activated.");
        }

        user.ActivateUser()
            .ResetOtp();

        await _userRepository.SaveAsync();

        return true;
    }
}
