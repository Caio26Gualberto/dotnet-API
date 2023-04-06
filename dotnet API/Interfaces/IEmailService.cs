namespace dotnet_API.Interfaces
{
    public interface IEmailService
    {
        public Task SendResetPasswordEmail(string email);
    }
}
