using dotnet_API.Models;
using dotnet_API.Interfaces;
using SendGrid.Helpers.Mail;
using SendGrid;
using dotnet_API.Extensions;

namespace dotnet_API.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendMailAsync(string userEmail)
        {
            EnvironmentVariable environment = new();
            Email emailClass = new();

            var apiKey = environment.SendgridApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(environment.EmailLogin, "A New Level Music");
            var subject = emailClass.ResetSubject;
            var to = new EmailAddress(userEmail, userEmail.FriendlyEmailName());
            var plainTextContent = "Ola mundo";
            var htmlContent = $"<strong>{emailClass.ResetBody}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
