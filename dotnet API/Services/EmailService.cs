using dotnet_API.Models;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using dotnet_API.Interfaces;

namespace dotnet_API.Services
{
    public class EmailService : IEmailService
    {
        Email Email = new();
        EnvironmentVariable EVariable = new();
        public async Task SendResetPasswordEmail(string email)
        {
            var emailConfig = new MimeMessage();
            emailConfig.From.Add(MailboxAddress.Parse(EVariable.EmailLogin));
            emailConfig.To.Add(MailboxAddress.Parse(email));
            emailConfig.Subject = Email.ResetSubject;
            emailConfig.Body = new TextPart(TextFormat.Html) { Text = Email.ResetBody };

            var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(EVariable.EmailLogin, EVariable.EmailPassword);
            smtp.Send(emailConfig);
            smtp.Disconnect(true);

        }
    }
}
