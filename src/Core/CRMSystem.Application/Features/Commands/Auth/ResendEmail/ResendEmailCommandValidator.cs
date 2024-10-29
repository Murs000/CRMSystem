using FluentValidation;

namespace CRMSystem.Application.Features.Commands.Auth.ResendEmail;

public class ResendEmailCommandValidator : AbstractValidator<ResendEmailCommand>
{
    public ResendEmailCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}