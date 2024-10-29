using CRMSystem.Application.Features.Commands.Auth.ViewModels;
using MediatR;

namespace CRMSystem.Application.Features.Commands.Auth.Login;

public class LoginCommand:IRequest<JwtTokenDto>
{
    public string Username {  get; set; }
    public string Password { get; set; }    
}
