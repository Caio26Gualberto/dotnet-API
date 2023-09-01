using dotnet_API.Models;

namespace dotnet_API.Interfaces
{
    public interface IEmailService
    {
        public Task SendMailAsync(string userEmail, string uri = "");
        public Task SendMailRegisterAsync(string userEmail, EnvironmentVariable environmentVariable, Email emailPattern, string url);
        public Task<string> CreateToken(User user, EnvironmentVariable environment);
    }
}
