using System.Security.Cryptography;

namespace CRMSystem.External.Helpers;

public static class OTPHelper
{
    public static string GenerateOTP()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}