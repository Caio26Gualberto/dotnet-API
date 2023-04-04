using dotnet_API.Interfaces;
using SendGrid;
using System.ComponentModel.DataAnnotations.Schema;
using Exception = System.Exception;

namespace dotnet_API.Models
{
    [Table("ResetPasswords")]
    public class SendMail : Password, ISendMail
    {
        public int Id { get; set; }
        public string Body { get; set; }

        private readonly SendGridMail SendGridMail;

        public SendMail() { }
        public SendMail(SendGridMail email)
        {
            SendGridMail = email;
        }
        public async Task<string> SendEmail(string userEmail, ANewLevelContext context)
        {
            SendMail email = new SendMail();

            try
            {
                var a = await SendGridMail.Execute(userEmail, context);
                return a;
            }
            catch (Exception e)
            {
                throw new Exception("Algo deu errado com o envio de email, por favor tenta novamente mais tarde");
            }
        }
    }
}
