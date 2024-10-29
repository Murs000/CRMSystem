using FluentValidation;

namespace CRMSystem.Application.Features.Commands.Auth.ValidateOTP;

public class ValidateOTPCommandValidator : AbstractValidator<ValidateOTPCommand>
{
    public ValidateOTPCommandValidator()
    {
        RuleFor(x => x.OTP).NotEmpty();
    }
}