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
        public async Task SendMailAsync(string userEmail, string uri)
        {
            EnvironmentVariable environment = new();
            Email emailClass = new();
            var url = uri;

            var apiKey = environment.SendgridApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(environment.EmailLogin, "A New Level Music");
            var subject = emailClass.ResetSubject;
            var to = new EmailAddress(userEmail, userEmail.FriendlyEmailName());
            var plainTextContent = "Ola mundo";
            var htmlContent = $"<strong>{emailClass.ResetBody}</strong><br>" +
                url;
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
