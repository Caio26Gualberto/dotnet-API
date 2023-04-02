namespace dotnet_API.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string BodyMessage { get; set; } = "Segue o link abaixo para a sua redefinição de senha";
        public string Title { get; set; } = "Redefinição de senha";
    }
}
