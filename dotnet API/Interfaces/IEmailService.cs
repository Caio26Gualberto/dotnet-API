namespace dotnet_API.Interfaces
{
    public interface IEmailService
    {
        public Task SendMailAsync(string userEmail);
    }
}
