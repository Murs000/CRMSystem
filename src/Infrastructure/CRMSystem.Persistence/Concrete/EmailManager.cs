using System.Net;
using System.Net.Mail;
using CRMSystem.Application.Interfaces;
using CRMSystem.External.Settings;
using Microsoft.Extensions.Options;

namespace CRMSystem.Persistence.Concrete;

public class EmailManager : IEmailManager
{
    private readonly SmtpClient _smtpClient;
    private readonly string _fromEmail;

    public EmailManager(IOptions<EmailSettings> emailSettings)
    {
        var settings = emailSettings.Value;
        _fromEmail = settings.FromEmail;

        _smtpClient = new SmtpClient(settings.SmtpHost, settings.SmtpPort)
        {
            Credentials = new NetworkCredential(settings.SmtpUsername, settings.SmtpPassword),
            EnableSsl = true
        };
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var mailMessage = new MailMessage(_fromEmail, toEmail, subject, body)
        {
            IsBodyHtml = true
        };
        await _smtpClient.SendMailAsync(mailMessage);
    }
}