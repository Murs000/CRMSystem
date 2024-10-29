using FluentValidation;

namespace CRMSystem.Application.Features.Commands.Auth.Login;

public class LoginCommandValidator:AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(command => command.Username).NotNull();
        RuleFor(command => command.Password).MinimumLength(10).NotNull();
    }
}