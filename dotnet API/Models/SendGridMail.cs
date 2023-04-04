using dotnet_API.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SendGrid
{
    public class SendGridMail
    {
        public async Task<string> Execute(string email, ANewLevelContext userContext)
        {
            Regex rx = new Regex(@"^(\w+)");
            Match match = rx.Match(email);
            string friendlyUserName = match.Groups[1].Value;

            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("anewlevelmusic@gmail.com", "A New Level Music");
            var subject = $"Aqui está seu link para redefinição de senha {friendlyUserName}";
            var to = new EmailAddress(email, friendlyUserName);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return friendlyUserName;
        }
    }
}