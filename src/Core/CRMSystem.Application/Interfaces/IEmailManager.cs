namespace CRMSystem.Application.Interfaces;

public interface IEmailManager
{
    public Task SendEmailAsync(string toEmail, string subject, string body);
}