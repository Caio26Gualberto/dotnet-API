using dotnet_API.Interfaces;
using Microsoft.Office.Interop.Outlook;
using System.ComponentModel.DataAnnotations.Schema;
using Exception = System.Exception;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace dotnet_API.Models
{
    [Table("ResetPasswords")]
    public class SendMail : Password, ISendMail
    {
        public int Id { get; set; }  
        public string Body { get; set; }

        private readonly ApiContext ApiContext;
        private Email _email { get; set; }

        public SendMail () {}
        public SendMail(Email email, ApiContext context)
        {
            _email = email;
            context.SendMails.Add(this);
            ApiContext = context;
        }
        public void SendEmail(string userEmail, ApiContext context)
        {        
            Email email = new Email();
            try
            {
                Application app = new Outlook.Application();
                MailItem emailToSend = app.CreateItem(OlItemType.olMailItem) as MailItem;

                emailToSend.To = userEmail;
                emailToSend.Subject = _email.Title;
                emailToSend.Body = _email.BodyMessage;
                emailToSend.Send();

                SendMail sendMail = new SendMail(email, context)
                {
                    Body = emailToSend.Body,
                };
                ApiContext.SendMails.Add(sendMail);

            }
            catch (Exception e)
            {
                throw new Exception("Algo deu errado com o envio de email, por favor tenta novamente mais tarde");
            }
        }
    }
}
