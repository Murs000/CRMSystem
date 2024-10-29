using CRMSystem.Domain.Entities;

namespace CRMSystem.Application.Interfaces;

public interface IUserManager
{
    public int GetCurrentUserId();
    (string token,DateTime expireAt) GenerateTJwtToken(User user);
}
