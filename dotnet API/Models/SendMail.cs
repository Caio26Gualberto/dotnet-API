using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace dotnet_API.Models
{
    [Table("ResetPasswords")]
    public class SendMail : Password
    {
        public int Id { get; set; }       
        private readonly Email _email;
        public SendMail(Email email)
        {
            _email = email;
        }

        public void SendEmail(string email)
        {
            MailMessage message = new MailMessage();
            var encryptedPassword = EncryptedPassword();
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 1000;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("anewlevelmusic@gmail.com", Encoding.UTF8.GetString(encryptedPassword));

                message.From = new MailAddress("anewlevelmusic@gmail.com", "Redefinição de senha");
                message.Body = _email.BodyMessage;
                message.Subject = _email.Title;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.Normal;
                message.To.Add(email);

                smtpClient.Send(message);
            }
            catch (Exception e)
            {
                throw new Exception("Algo deu errado com o envio de email, por favor tenta novamente mais tarde");
            }
        }
    }
}
