using System.Security.Claims;
using CRMSystem.Domain.Entities;

namespace CRMSystem.Application.Interfaces;

public interface IClaimManager
{
    int GetCurrentUserId();
    IEnumerable<Claim> GetUserClaims(User user);
}
