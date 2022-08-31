using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HR.LeaveManagement.Infrastructure.Mail;

public class EmailSender : IEmailSender
{
    private EmailSettings emailSettings { get; }

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        this.emailSettings = emailSettings.Value;
    }

    public async Task<bool> SendEmail(Email email)
    {
        var client = new SendGridClient(emailSettings.ApiKey);
        var to = new EmailAddress(email.To);
        var from = new EmailAddress
        {
            Email = emailSettings.FromAddress,
            Name = emailSettings.FromName
        };

        var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
        var response = await client.SendEmailAsync(message);

        return response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted;
    }
}