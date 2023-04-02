namespace dotnet_API.Models
{
    public class Email
    {
        public string BodyMessage { get; }
        public string Title { get; }

        public Email(string bodyMessage = "Segue o link abaixo para a sua redefinição de senha", string title = "Redefinição de senha")
        {
            BodyMessage = bodyMessage;
            Title = title;
        }
    }
}
