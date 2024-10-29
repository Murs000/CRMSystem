using System.Security.Cryptography;
using System.Text;

namespace CRMSystem.External.Helpers;

public static class RefreshTokenHelper
{
    public static (string token, DateTime expireAt) GenerateRefreshToken(int userId)
    {
        var random = GenerateRandomNumber();
        var refreshToken = $"{random}_{userId}";
        var expireAt = DateTime.UtcNow.AddDays(20);
        return (refreshToken, expireAt);
    }

    private static object GenerateRandomNumber()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}