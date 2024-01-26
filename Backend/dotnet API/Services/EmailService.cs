using dotnet_API.Extensions;
using dotnet_API.Interfaces;
using dotnet_API.Models;
using Microsoft.IdentityModel.Tokens;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dotnet_API.Services
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {
        }
        public async Task SendMailAsync(string userEmail, string uri = "")
        {
            Email emailPattern = new Email();
            EnvironmentVariable environmentVariable = new EnvironmentVariable();
            var url = uri;

            if (url.Contains("confirmemail"))
                await SendMailRegisterAsync(userEmail, environmentVariable, emailPattern, url);
            else
            {
                var apiKey = environmentVariable.SendgridApiKey;
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(environmentVariable.EmailLogin, "A New Level Music");
                var subject = emailPattern.ResetSubject;
                var to = new EmailAddress(userEmail, userEmail.FriendlyEmailName());
                var plainTextContent = $"Clique no link abaixo para redefinir sua senha:\n{url}";
                var htmlContent = $"<p>Clique no link abaixo para redefinir sua senha:</p><p><a href='{url}'>{url}</a></p>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
        }

        public async Task SendMailRegisterAsync(string userEmail, EnvironmentVariable environmentVariable, Email emailPattern, string url)
        {
            var apiKey = environmentVariable.SendgridApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(environmentVariable.EmailLogin, "A New Level Music");
            var subject = emailPattern.ResetSubject;
            var to = new EmailAddress(userEmail, userEmail.FriendlyEmailName ());
            var plainTextContent = $"Confirme seu email";
            var htmlContent = "<h3>Bem vindo headbanger</h3>" +
                $"<p>Por favor confirme seu email{url}</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        public async Task<string> CreateToken(User user, EnvironmentVariable environment)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var takeSecretKey = environment.JWTApiToken;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(takeSecretKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credential);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
