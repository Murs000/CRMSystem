using CRMSystem.Domain.Enums;

namespace CRMSystem.Domain.Entities;

public class User : BaseEntity<User>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpireDate { get; set; }
    public string? OTP { get; set; }
    public DateTime? OTPExpireDate { get; set; }
    public bool IsAllowedPasswordChange {get; set;}
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public bool IsActivated { get; set; }
    public UserRole Role { get; set; }

    public User SetRefreshToken(string refreshToken, DateTime? refreshTokenExpireDate)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpireDate = refreshTokenExpireDate;

        return this;
    } 

    public User ActivateUser()
    {
        IsActivated = true;

        return this;
    }

    public User ResetOtp()
    {
        // Clear OTP after successful confirmation
        OTP = null;
        OTPExpireDate = null;

        return this;
    } 

    public User SetPassword(string passwordHash, string paswordSalt)
    {
        PasswordHash = passwordHash;
        PasswordSalt = paswordSalt;

        return this;
    }

    public User SetOtp(string otp, DateTime expireDate)
    {
        OTP = otp;
        OTPExpireDate = expireDate;

        return this;
    }

    public User SetDetails(string username, string email,string name, string surname)
    {
        Email = email;
        Username = username;
        Name = name;
        Surname = surname;

        return this;
    }
    public User ChangeRole(UserRole role)
    {
        Role = role;

        return this;
    }
}