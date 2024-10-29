using FluentValidation;

namespace CRMSystem.Application.Features.Commands.Auth.ConfirmEmail;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.OTP).NotEmpty();
    }
}