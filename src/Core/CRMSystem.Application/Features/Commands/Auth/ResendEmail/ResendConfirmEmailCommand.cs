using MediatR;

namespace CRMSystem.Application.Features.Commands.Auth.ResendEmail;

public class ResendEmailCommand : IRequest<bool>
{
    public string Email { get; set; }
}