namespace CRMSystem.Application.Features.Commands.Auth.ViewModels;

public class JwtTokenDto
{
    public string Token { get; set; }
    public DateTime TokenExpireAt { get; set; }
    public string RefreshToken{ get; set; }
    public DateTime RefreshExpireAt { get; set; }
}
