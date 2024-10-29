using CRMSystem.Application.Common.Exceptions;
using CRMSystem.Application.Interfaces;
using CRMSystem.External.Helpers;
using MediatR;

namespace CRMSystem.Application.Features.Commands.Auth.ResendEmail;

public class ResendEmailCommandHandler : IRequestHandler<ResendEmailCommand,bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailManager _emailService;

    public ResendEmailCommandHandler(IUserRepository userRepository, IEmailManager emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<bool> Handle(ResendEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(x => x.Email == request.Email && !x.IsActivated);

        if (user == null)
        {
            throw new NotFoundException("User not found or already activated.");
        }

        var newOtp = OTPHelper.GenerateOTP();
        var newOtpExpireDate = DateTime.UtcNow.AddHours(4).AddMinutes(10); 

        user.SetOtp(newOtp, newOtpExpireDate);

        await _userRepository.SaveAsync();

        // Send confirmation email
        await _emailService.SendEmailAsync(user.Email,"OTP message", newOtp);

        return true;
    }
}