namespace dotnet_API.Models
{
    public class Email
    {
        public string ResetBody { get; set; } = "Aqui está seu link para redefinição de senha";
        public string ResetSubject { get; set; } = "Redefinição de senha";
    }
}
