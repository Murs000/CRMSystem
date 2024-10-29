using CRMSystem.Application.Common.Exceptions;
using CRMSystem.Application.Interfaces;
using CRMSystem.External.Helpers;
using MediatR;

namespace CRMSystem.Application.Features.Commands.Auth.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand,bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailManager _emailService;

    public ForgotPasswordCommandHandler(IUserRepository userRepository, IEmailManager emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(x => x.Email == request.Email);

        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        var otp = OTPHelper.GenerateOTP();
        var otpExpireDate = DateTime.UtcNow.AddHours(4).AddMinutes(4);

        user.SetOtp(otp, otpExpireDate);

        await _userRepository.SaveAsync();

        // Send reset password email
        await _emailService.SendEmailAsync(user.Email, "OTP message", otp);

        return true;
    }
}