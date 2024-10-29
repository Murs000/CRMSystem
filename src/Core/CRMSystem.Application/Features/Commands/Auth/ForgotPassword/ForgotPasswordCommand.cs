using MediatR;

namespace CRMSystem.Application.Features.Commands.Auth.ForgotPassword;

public class ForgotPasswordCommand : IRequest<bool>
{
    public string Email { get; set; }
}
