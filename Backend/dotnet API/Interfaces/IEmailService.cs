using dotnet_API.Models;

namespace dotnet_API.Interfaces
{
    public interface IEmailService
    {
        public Task SendMailAsync(string userEmail, string uri);
        public Task<string> CreateToken(User user, EnvironmentVariable environment);
    }
}
